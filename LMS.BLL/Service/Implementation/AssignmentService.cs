using AutoMapper;
using LMS.BLL.ModelVM;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{

    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepo _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AssignmentService(IAssignmentRepo repo, IMapper mapper, IWebHostEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
        }

        // ── FR-11 Assignment Creation ────────────────

        public async Task<bool> CreateAssignmentAsync(AssignmentVM model)
        {
            // Rule: due date must be in the future
            if (model.DueDate <= DateTime.Now)
                return false;

            var assignment = _mapper.Map<Assignment>(model);
            await _repo.AddAssignmentAsync(assignment);
            return true;
        }

        public async Task<AssignmentVM> GetAssignmentForEditAsync(int id)
        {
            var assignment = await _repo.GetAssignmentByIdAsync(id);
            return _mapper.Map<AssignmentVM>(assignment);
        }

        public async Task<bool> UpdateAssignmentAsync(AssignmentVM model)
        {
            var assignment = await _repo.GetAssignmentByIdAsync(model.Id);
            if (assignment == null) return false;

            _mapper.Map(model, assignment);
            await _repo.UpdateAssignmentAsync(assignment);
            return true;
        }

        public async Task<bool> DeleteAssignmentAsync(int id)
        {
            var assignment = await _repo.GetAssignmentByIdAsync(id);
            if (assignment == null) return false;

            await _repo.DeleteAssignmentAsync(id);
            return true;
        }

        public async Task<IEnumerable<AssignmentListVM>> GetAssignmentsByInstructorAsync(int instructorId)
        {
            var assignments = await _repo.GetAssignmentsByInstructorAsync(instructorId);
            return _mapper.Map<IEnumerable<AssignmentListVM>>(assignments);
        }

        public async Task<IEnumerable<AssignmentListVM>> GetAssignmentsByCourseAsync(int courseId)
        {
            var assignments = await _repo.GetAssignmentsByCourseAsync(courseId);
            return _mapper.Map<IEnumerable<AssignmentListVM>>(assignments);
        }

        // ── FR-12 Submission ─────────────────────────

        public async Task<bool> SubmitAssignmentAsync(SubmissionVM model)
        {
            // Rule: student can only submit once
            var existing = await _repo.GetSubmissionsByStudentAsync(model.StudentId);
            if (existing.Any(s => s.AssignmentId == model.AssignmentId))
                return false;

            var submission = new AssignmentSubmission
            {
                AssignmentId = model.AssignmentId,
                StudentId = model.StudentId,
                TextContent = model.TextContent,
                SubmittedAt = DateTime.Now,
                IsGraded = false
            };

            // Handle file upload
            if (model.File != null && model.File.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "assignments");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await model.File.CopyToAsync(stream);

                submission.FilePath = $"/uploads/assignments/{fileName}";
            }

            await _repo.AddSubmissionAsync(submission);
            return true;
        }

        public async Task<IEnumerable<AssignmentListVM>> GetStudentAssignmentsAsync(int studentId)
        {
            var submissions = await _repo.GetSubmissionsByStudentAsync(studentId);
            return submissions.Select(s => new AssignmentListVM
            {
                Id = s.AssignmentId,
                Title = s.Assignment.Title,
                CourseName = s.Assignment.Course.Title,
                DueDate = s.Assignment.DueDate,
                IsSubmitted = true,
                IsGraded = s.IsGraded,
                Grade = s.Grade
            });
        }

        // ── FR-13 Grading ────────────────────────────

        public async Task<GradeVM> GetSubmissionForGradingAsync(int submissionId)
        {
            var submission = await _repo.GetSubmissionByIdAsync(submissionId);
            return _mapper.Map<GradeVM>(submission);
        }

        public async Task<bool> GradeSubmissionAsync(GradeVM model)
        {
            var submission = await _repo.GetSubmissionByIdAsync(model.SubmissionId);
            if (submission == null) return false;

            // Rule: grade must be between 0 and 100
            if (model.Grade < 0 || model.Grade > 100) return false;

            submission.Grade = model.Grade;
            submission.Feedback = model.Feedback;
            submission.IsGraded = true;

            await _repo.UpdateSubmissionAsync(submission);
            return true;
        }

        public async Task<IEnumerable<SubmissionVM>> GetSubmissionsForAssignmentAsync(int assignmentId)
        {
            var submissions = await _repo.GetSubmissionsByAssignmentAsync(assignmentId);
            return _mapper.Map<IEnumerable<SubmissionVM>>(submissions);
        }

        // ── FR-14 Grade Viewing ──────────────────────

        public async Task<IEnumerable<SubmissionVM>> GetStudentGradesAsync(int studentId)
        {
            var submissions = await _repo.GetSubmissionsByStudentAsync(studentId);
            return _mapper.Map<IEnumerable<SubmissionVM>>(
                submissions.Where(s => s.IsGraded)
            );
        }

        public async Task<SubmissionVM> GetSubmissionDetailsAsync(int submissionId)
        {
            var submission = await _repo.GetSubmissionByIdAsync(submissionId);
            return _mapper.Map<SubmissionVM>(submission);
        }
    }
}

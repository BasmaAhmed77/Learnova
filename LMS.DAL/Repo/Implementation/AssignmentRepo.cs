using LMS.DAL.Database;
using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using LMS.DAL.Repo.Abstraction;
namespace LMS.DAL.Repo.Implementation
{
    public class AssignmentRepo : IAssignmentRepo

    {
        private readonly LMSDBContext _context;

        public AssignmentRepo(LMSDBContext context)
        {
            _context = context;
        }

        // ── Assignments ──────────────────────────────

        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.Instructor)
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.Instructor)
                .Include(a => a.Submissions)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByCourseAsync(int courseId)
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByInstructorAsync(int instructorId)
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .Where(a => a.InstructorId == instructorId)
                .ToListAsync();
        }

        public async Task AddAssignmentAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssignmentAsync(int id)
        {
            var assignment = await GetAssignmentByIdAsync(id);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }

        // ── Submissions ──────────────────────────────

        public async Task<IEnumerable<AssignmentSubmission>> GetSubmissionsByAssignmentAsync(int assignmentId)
        {
            return await _context.AssignmentSubmissions
                .Include(s => s.Student)
                .Include(s => s.Assignment)
                .Where(s => s.AssignmentId == assignmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssignmentSubmission>> GetSubmissionsByStudentAsync(int studentId)
        {
            return await _context.AssignmentSubmissions
                .Include(s => s.Assignment)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<AssignmentSubmission> GetSubmissionByIdAsync(int id)
        {
            return await _context.AssignmentSubmissions
                .Include(s => s.Student)
                .Include(s => s.Assignment)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSubmissionAsync(AssignmentSubmission submission)
        {
            submission.SubmittedAt = DateTime.Now;
            await _context.AssignmentSubmissions.AddAsync(submission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubmissionAsync(AssignmentSubmission submission)
        {
            _context.AssignmentSubmissions.Update(submission);
            await _context.SaveChangesAsync();
        }
    }
}

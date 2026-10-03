using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::LMS.BLL.ModelVM;
using LMS.BLL.ModelVM;

namespace LMS.BLL.Service.Abstraction
{
       public interface IAssignmentService
        {
            // FR-11 Assignment Creation
            Task<bool> CreateAssignmentAsync(AssignmentVM model);
            Task<AssignmentVM> GetAssignmentForEditAsync(int id);
            Task<bool> UpdateAssignmentAsync(AssignmentVM model);
            Task<bool> DeleteAssignmentAsync(int id);
            Task<IEnumerable<AssignmentListVM>> GetAssignmentsByInstructorAsync(int instructorId);
            Task<IEnumerable<AssignmentListVM>> GetAssignmentsByCourseAsync(int courseId);

            // FR-12 Assignment Submission
            Task<bool> SubmitAssignmentAsync(SubmissionVM model);
            Task<IEnumerable<AssignmentListVM>> GetStudentAssignmentsAsync(int studentId);

            // FR-13 Manual Grading
            Task<GradeVM> GetSubmissionForGradingAsync(int submissionId);
            Task<bool> GradeSubmissionAsync(GradeVM model);
            Task<IEnumerable<SubmissionVM>> GetSubmissionsForAssignmentAsync(int assignmentId);

            // FR-14 Grade Viewing
            Task<IEnumerable<SubmissionVM>> GetStudentGradesAsync(int studentId);
            Task<SubmissionVM> GetSubmissionDetailsAsync(int submissionId);
        }
    
}

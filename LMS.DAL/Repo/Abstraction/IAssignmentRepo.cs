using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
    public interface IAssignmentRepo
    {
        // Assignment CRUD
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<Assignment>> GetAssignmentsByCourseAsync(int courseId);
        Task<IEnumerable<Assignment>> GetAssignmentsByInstructorAsync(int instructorId);
        Task AddAssignmentAsync(Assignment assignment);
        Task UpdateAssignmentAsync(Assignment assignment);
        Task DeleteAssignmentAsync(int id);

        // Submission
        Task<IEnumerable<AssignmentSubmission>> GetSubmissionsByAssignmentAsync(int assignmentId);
        Task<IEnumerable<AssignmentSubmission>> GetSubmissionsByStudentAsync(int studentId);
        Task<AssignmentSubmission> GetSubmissionByIdAsync(int id);
        Task AddSubmissionAsync(AssignmentSubmission submission);
        Task UpdateSubmissionAsync(AssignmentSubmission submission);
    }
}

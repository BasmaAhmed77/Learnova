using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
    public interface IStudentRepo
    {
        Task<Student?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Student?> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken ct = default);
        Task<List<int>> GetEnrolledIdsByStudentIdAsync(int studentId, CancellationToken ct);
        Task AddAsync(Student student, CancellationToken ct = default);
        Task EnrollInCourseAsync(StudentCourse studentCourse, CancellationToken ct);
        Task<bool> IsEnrolledAsync(int studentId, int courseId, CancellationToken ct);
    }
}

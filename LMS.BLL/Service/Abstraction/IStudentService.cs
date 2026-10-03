using LMS.BLL.ModelVM.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Abstraction
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentVM>> GetAllAsync(CancellationToken ct = default);
        Task<StudentVM?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<StudentVM?> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task<List<int>> GetEnrolledCourseIdsAsync(string userId, CancellationToken ct = default);
    }
}

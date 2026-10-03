using LMS.BLL.ModelVM.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Abstraction
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorVM>> GetAllAsync(CancellationToken ct = default);
        Task<InstructorVM?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<InstructorVM?> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task<bool> UpdateBioAsync(string userId, UpdateInstructorBioVM model, CancellationToken ct = default);
        Task<bool> UpdateExperienceAsync(string userId, UpdateInstructorYearsOfExperienceVM model, CancellationToken ct = default);
        Task<bool> UpdateAcademicDegreeAsync(string userId, UpdateInstructorAcademicDegreeVM model, CancellationToken ct = default);
        Task<bool> UpdateTitleAsync(string userId, UpdateInstructorTitleVM model, CancellationToken ct = default);
        Task<bool> VerifyInstructorAsync(string userId, CancellationToken ct = default);
    }
}

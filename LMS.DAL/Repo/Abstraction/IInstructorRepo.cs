using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
    public interface IInstructorRepo
    {
        Task<Instructor?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Instructor?> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task<IEnumerable<Instructor>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Instructor instructor, CancellationToken ct = default);
        Task UpdateBioAsync(int id, string newBio, CancellationToken ct = default);
        Task UpdateExperienceAsync(int id, int years, CancellationToken ct = default);
        Task UpdateAcademicDegreeAsync(int id, string degree, CancellationToken ct = default);
        Task UpdateTitleAsync(int id, string title, CancellationToken ct = default);
        Task VerifyInstructorAsync(int id, CancellationToken ct = default);
    }
}

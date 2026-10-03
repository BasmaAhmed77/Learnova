using LMS.DAL.Database;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Implementation
{
    public class InstructorRepo : IInstructorRepo
    {
        private readonly LMSDBContext _context;
        public InstructorRepo(LMSDBContext context)
        {
            _context = context;
        }
        public async Task<Instructor?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Instructors
                 .Include(i => i.UserAccount)
                 .FirstOrDefaultAsync(i => i.InstructorId == id && !i.UserAccount.IsDeleted, ct);
        }

        public async Task<Instructor?> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.UserAccount)
                .FirstOrDefaultAsync(i => i.UserId == userId && !i.UserAccount.IsDeleted, ct);
        }
        public async Task<IEnumerable<Instructor>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.UserAccount)
                .Where(i => !i.UserAccount.IsDeleted)
                .ToListAsync(ct);
        }
        public async Task AddAsync(Instructor instructor, CancellationToken ct = default)
        {
            await _context.Instructors.AddAsync(instructor,ct);
        }
        public async Task UpdateAcademicDegreeAsync(int id, string degree, CancellationToken ct = default)
        {
            var instructor = await GetByIdAsync(id, ct);
            if (instructor != null)
            {
                instructor.UpdateAcademicDegree(degree);
            }
        }

        public async Task UpdateBioAsync(int id, string newBio, CancellationToken ct = default)
        {
            var instructor = await GetByIdAsync(id, ct);
            if (instructor != null)
            {
                instructor.UpdateProfessionalBio(newBio);
            }
        }

        public async Task UpdateExperienceAsync(int id, int years, CancellationToken ct = default)
        {
            var instructor = await GetByIdAsync(id, ct);
            if (instructor != null)
            {
                instructor.UpdateExperience(years);
            }
        }
        public async Task UpdateTitleAsync(int id, string title, CancellationToken ct = default)
        {
            var instructor = await GetByIdAsync(id, ct);
            if (instructor != null)
            {
                instructor.UpdateJobTitle(title); 
            }
        }
        public async Task VerifyInstructorAsync(int id, CancellationToken ct = default)
        {
            var instructor = await GetByIdAsync(id, ct);
            if (instructor != null)
            {
                instructor.Verify();
            }
        }
    }
}

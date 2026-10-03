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
    public class UserRepo : IUserRepo
    {
        private readonly LMSDBContext _context;
        public UserRepo(LMSDBContext context) => _context = context;

        public async Task<UserAccount?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await _context.UserAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, ct);
        }
        public async Task UpdateNameAsync(string id, string fname, string lname, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null)
            {
                user.UpdateName(fname, lname); 
            }
        }
        public async Task<UserAccount?> GetDeletedUserByIdAsync(string id, CancellationToken ct = default)
        {
            return await _context.UserAccounts
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted, ct);
        }

        public async Task SetProfilePictureAsync(string id, string path, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && !user.IsDeleted)
            {
                user.SetProfilePicture(path);
            }
        }

        public async Task RemoveProfilePictureAsync(string id, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && !user.IsDeleted)
            {
                user.RemoveProfilePicture();
            }
        }

        public async Task SetPhoneNumberAsync(string id, string phoneNumber, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && !user.IsDeleted)
            {
                user.SetPhoneNumber(phoneNumber);
            }
        }
        public async Task RemovePhoneNumberAsync(string id, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && !user.IsDeleted)
            {
                user.RemovePhoneNumber();
            }
        }

        public async Task SoftDeleteAsync(string id, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && !user.IsDeleted)
            {
                user.SoftDelete();
            }
        }

        public async Task RestoreAsync(string id, CancellationToken ct = default)
        {
            var user = await _context.UserAccounts.FindAsync(new object[] { id }, ct);
            if (user != null && user.IsDeleted)
            {
                user.Restore();
            }
        }

    }
}

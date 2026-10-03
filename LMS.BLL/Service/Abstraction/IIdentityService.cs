using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Abstraction
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, string UserId, string Error)> CreateUserAsync(
            string fname, string lname, string email, string userName, string gender, DateTime dob, string password);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<bool> AddToRoleAsync(string userId, string role);
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<(bool Succeeded, string Error)> ResetPasswordAsync(string email, string token, string newPassword);
        Task<IEnumerable<string>> GetUserRolesAsync(string email);
    }
}

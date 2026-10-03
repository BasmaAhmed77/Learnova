using LMS.BLL.Service.Abstraction;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(bool Succeeded, string UserId, string Error)> CreateUserAsync(
            string fname,
            string lname,
            string email,
            string userName,
            string gender,
            DateTime dob,
            string password)
        {
            try
            {
                var user = new UserAccount(fname, lname, email, userName, gender, dob);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return (true, user.Id, "");
                }
                var firstError = result.Errors.FirstOrDefault();
                string errorMessage = "Registration failed";

                if (firstError != null)
                {
                    errorMessage = firstError.Description;
                }

                return (false, "", errorMessage);
            }
            catch (ArgumentException ex)
            {
                return (false, string.Empty, ex.Message);
            }
        }
        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.IsDeleted)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<bool> AddToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return (false, "User not found");
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return (true, "");
            }
            else
            {
                var firstError = result.Errors.FirstOrDefault();
                string errorMessage = "Failed to change password";

                if (firstError != null)
                {
                    errorMessage = firstError.Description;
                }

                return (false, errorMessage);
            }
        }
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) 
                return string.Empty;
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        public async Task<(bool Succeeded, string Error)> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) 
                return (false, "User not found");
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return (true, "");
            }
            else
            {
                var firstError = result.Errors.FirstOrDefault();
                string errorMessage = "Invalid reset process";

                if (firstError != null)
                {
                    errorMessage = firstError.Description;
                }

                return (false, errorMessage);
            }
        }
        public async Task<IEnumerable<string>> GetUserRolesAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Enumerable.Empty<string>();

            return await _userManager.GetRolesAsync(user);
        }
    }
}

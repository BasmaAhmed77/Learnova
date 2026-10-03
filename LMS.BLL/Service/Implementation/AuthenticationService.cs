using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.ModelVM.User;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identity;
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<UserAccount> _signInManager;
        public AuthenticationService(IIdentityService identity, IUnitOfWork uow, SignInManager<UserAccount> signInManager)
        {
            _identity = identity;
            _uow = uow;
            _signInManager = signInManager;
        }
        public async Task<(bool Succeeded, string Error)> RegisterStudentAsync(RegisterVM model)
        {
            try
            {
                var (idResult, userId, idError) = await _identity.CreateUserAsync(
                    model.Fname, model.Lname, model.Email, model.UserName,
                    model.Gender, model.DOB, model.Password);
                if (!idResult) 
                    return (false, idError);
                await _identity.AddToRoleAsync(userId, "Student");
                var student = new Student(userId);
                await _uow.Students.AddAsync(student);
                await _uow.CompleteAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return (false, ex.Message);
            }
        }
        public async Task<(bool Succeeded, string Error)> RegisterInstructorAsync(RegisterInstructorVM model)
        {
            try
            {
                var (idResult, userId, idError) = await _identity.CreateUserAsync(
                    model.Fname, model.Lname, model.Email, model.UserName,
                    model.Gender, model.DOB, model.Password);
                if (!idResult) 
                    return (false, idError);
                await _identity.AddToRoleAsync(userId, "Instructor");
                var instructor = new Instructor(userId, model.Bio, model.AcademicDegree, model.YearsOfExperience, model.Title);
                await _uow.Instructors.AddAsync(instructor);
                await _uow.CompleteAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return (false, ex.Message);
            }
        }
        public async Task<(bool Succeeded, string Error, string Role)> LoginAsync(LoginVM model)
        {

            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return (false, "Invalid Email or Password!","");
            }
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var roles = await _identity.GetUserRolesAsync(model.Email);

                var userRole = roles.FirstOrDefault();
                if (string.IsNullOrEmpty(userRole))
                    return (false, "User has no assigned role.", "");

                return (true, "", userRole);
            }
            if (result.IsLockedOut)
            {
                return (false, "Account locked due to multiple failed attempts.","");
            }

            return (false, "Invalid Email or Password.","");
        }

        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(ChangePasswordVM model, string userId)
        {
            if (model.CurrentPassword == model.NewPassword)
                return (false, "The new password cannot be the same as the current password.");
            return await _identity.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);
        }
        public async Task<(string Token, string Error)> GeneratePasswordResetTokenAsync(string email)
        {
            var token = await _identity.GeneratePasswordResetTokenAsync(email);
            if (string.IsNullOrEmpty(token))
                return ("", "User not found or email is invalid.");
            return (token, "");
        }
        public async Task<(bool Succeeded, string Error)> ResetPasswordAsync(ResetPasswordVM model)
        {
            return await _identity.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

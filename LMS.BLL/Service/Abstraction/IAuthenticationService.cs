using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.ModelVM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Abstraction
{
    public interface IAuthenticationService
    {
        Task<(bool Succeeded, string Error)> RegisterStudentAsync(RegisterVM model);
        Task<(bool Succeeded, string Error)> RegisterInstructorAsync(RegisterInstructorVM model);
        Task<(bool Succeeded, string Error, string Role)> LoginAsync(LoginVM model);
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(ChangePasswordVM model, string userId);
        Task<(string Token, string Error)> GeneratePasswordResetTokenAsync(string email);
        Task<(bool Succeeded, string Error)> ResetPasswordAsync(ResetPasswordVM model);
        Task LogoutAsync();
    }
}

using LMS.BLL.ModelVM.User;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Abstraction
{
    public interface IUserService
    {
        Task<UserAccountVM?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<bool> UpdateProfileNameAsync(string userId, string fname, string lname, CancellationToken ct = default);
        Task<bool> SetProfilePictureAsync(IFormFile file, string userId,CancellationToken ct = default);
        Task<bool> RemoveProfilePictureAsync(string id, CancellationToken ct = default);
        Task<bool> SetPhoneNumberAsync(string UserId,UpdatePhoneNumberVM model, CancellationToken ct = default);
        Task<bool> RemovePhoneNumberAsync(string id, CancellationToken ct = default);
        Task<bool> SoftDeleteAsync(string id, CancellationToken ct = default);
        Task<bool> RestoreAsync(string id, CancellationToken ct = default);
    }
}

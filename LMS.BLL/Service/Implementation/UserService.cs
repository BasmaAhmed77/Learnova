using AutoMapper;
using LMS.BLL.Helper;
using LMS.BLL.ModelVM.User;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly Upload _fileHelper;

        public UserService(IUnitOfWork uow, IMapper mapper, Upload fileHelper)
        {
            _uow = uow;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }
        public async Task<UserAccountVM?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, ct);
            return _mapper.Map<UserAccountVM>(user);
        }
        public async Task<bool> UpdateProfileNameAsync(string userId, string fname, string lname, CancellationToken ct = default)
        {
            await _uow.Users.UpdateNameAsync(userId, fname, lname, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> SetProfilePictureAsync(IFormFile file, string userId, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(userId, ct);
            if (user == null)
                return false;
            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                _fileHelper.DeleteFile(user.ProfilePicture, "Profiles");
            }
            string newFileName = _fileHelper.UploadFile(file, "Profiles");
            await _uow.Users.SetProfilePictureAsync(userId, newFileName, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> RemoveProfilePictureAsync(string id, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, ct);
            if (user == null) 
                return false;
            if (string.IsNullOrEmpty(user.ProfilePicture)) 
                return true;
            await _uow.Users.RemoveProfilePictureAsync(id, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> SetPhoneNumberAsync(string UserId, UpdatePhoneNumberVM model, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(UserId, ct);
            if (user == null) 
                return false;
            if (user.PhoneNumber == model.PhoneNumber) 
                return true;
            await _uow.Users.SetPhoneNumberAsync(UserId, model.PhoneNumber, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> RemovePhoneNumberAsync(string id, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, ct);
            if (user == null) 
                return false;
            if (string.IsNullOrEmpty(user.PhoneNumber)) 
                return true;
            await _uow.Users.RemovePhoneNumberAsync(id, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> SoftDeleteAsync(string id, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetByIdAsync(id, ct);
            if (user == null) 
                return false;
            if (user.IsDeleted) 
                return true;
            await _uow.Users.SoftDeleteAsync(id, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
        public async Task<bool> RestoreAsync(string id, CancellationToken ct = default)
        {
            var user = await _uow.Users.GetDeletedUserByIdAsync(id, ct);
            if (user == null)
                return false;
            await _uow.Users.RestoreAsync(id, ct);
            int rowsAffected = await _uow.CompleteAsync();
            if (rowsAffected > 0)
                return true;
            return false;
        }
    }
}

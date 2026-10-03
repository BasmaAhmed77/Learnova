using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
    public interface IUserRepo
    {
        Task<UserAccount?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<UserAccount?> GetDeletedUserByIdAsync(string id, CancellationToken ct = default);
        Task UpdateNameAsync(string id, string fname, string lname, CancellationToken ct = default);
        Task SetProfilePictureAsync(string id, string path, CancellationToken ct = default);
        Task RemoveProfilePictureAsync(string id, CancellationToken ct = default);
        Task SetPhoneNumberAsync(string id, string phoneNumber, CancellationToken ct = default);
        Task RemovePhoneNumberAsync(string id, CancellationToken ct = default);
        Task SoftDeleteAsync(string id, CancellationToken ct = default);
        Task RestoreAsync(string id, CancellationToken ct = default);
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Abstraction
{
    public interface IUnitOfWork
    {
        IUserRepo Users { get; }
        IInstructorRepo Instructors { get; }
        IStudentRepo Students { get; }
        Task<int> CompleteAsync(CancellationToken ct = default);
        Task RollbackAsync();
    }
}

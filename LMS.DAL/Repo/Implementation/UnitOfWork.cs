using LMS.DAL.Database;
using LMS.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Repo.Implementation
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LMSDBContext _context;
        public IUserRepo Users { get; private set; }
        public IInstructorRepo Instructors { get; private set; }
        public IStudentRepo Students { get; private set; }

        public UnitOfWork(LMSDBContext context)
        {
            _context = context;
            Users = new UserRepo(_context);
            Instructors = new InstructorRepo(_context);
            Students = new StudentRepo(_context);
        }
        public async Task<int> CompleteAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }
        public async Task RollbackAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

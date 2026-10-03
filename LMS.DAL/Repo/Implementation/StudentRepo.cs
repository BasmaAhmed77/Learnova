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
    public class StudentRepo : IStudentRepo
    {
        private readonly LMSDBContext _context;
        public StudentRepo(LMSDBContext context) => _context = context;

        public async Task<Student?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(s => s.UserAccount)
                .FirstOrDefaultAsync(s => s.StudentId == id&& !s.UserAccount.IsDeleted,ct);
        }

        public async Task<Student?> GetByUserIdAsync(string userId, CancellationToken ct = default)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(s => s.UserAccount)
                .FirstOrDefaultAsync(s => s.UserId == userId && !s.UserAccount.IsDeleted, ct);
        }

        public async Task AddAsync(Student student, CancellationToken ct = default)
        {
            await _context.Students.AddAsync(student,ct);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken ct = default)
        {
            return await _context.Students
                .AsNoTracking()
                .Include(s => s.UserAccount)
                .Where(s => !s.UserAccount.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<List<int>> GetEnrolledIdsByStudentIdAsync(int studentId, CancellationToken ct)
        {
            return await _context.StudentCourses
                .AsNoTracking()
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.CourseId)
                .ToListAsync(ct);
        }
        public async Task EnrollInCourseAsync(StudentCourse studentCourse, CancellationToken ct)
        {
            await _context.StudentCourses.AddAsync(studentCourse, ct);
        }

        public async Task<bool> IsEnrolledAsync(int studentId, int courseId, CancellationToken ct)
        {
            return await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId, ct);
        }
    }
}

using LMS.DAL.Database;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repo.Implementation
{
    public class CourseRepo : ICourseRepo
    {
        private readonly LMSDBContext _dbContext;

        public CourseRepo(LMSDBContext context)
        {
            _dbContext = context;
        }
        public async Task addCourseAsync(Course course, CancellationToken ct)
        {
            await _dbContext.Courses.AddAsync(course, ct);
        }

        public async Task<Course?> getCourseByIdAsync(int courseId, CancellationToken ct)
        {
            return await _dbContext.Courses.FindAsync(new object[] { courseId }, ct);
        }
        public async Task updateCourseAsync(Course course)
        {
            _dbContext.Courses.Update(course);
            await Task.CompletedTask;
        }

        public async Task<Course?> getCourseWithLessonsByIdAsync(int courseId, CancellationToken ct)
        {
            return await _dbContext.Courses
                .Include(c => c.Instructor)
                    .ThenInclude(i => i.UserAccount)
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.CourseId == courseId, ct);
        }

        public async Task deleteCourseByIDAsync(int courseId, CancellationToken ct)
        {
            var course = await getCourseByIdAsync(courseId, ct);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
            }
        }

        public async Task<IEnumerable<Course>> getCoursesByInstructorIdAsync(int instructorId, CancellationToken ct)
        {
            return await _dbContext.Courses
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Course>> getEnrolledCoursesByStudentIdAsync(int studentId, CancellationToken ct)
        {
            return await _dbContext.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Course)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Course>> getAllCoursesAsync(CancellationToken ct)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                    .ThenInclude(i => i.UserAccount)
                .ToListAsync(ct);
        }
    }
}
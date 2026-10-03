using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repo.Abstraction
{
    public interface ICourseRepo
    {
        Task<IEnumerable<Course>> getAllCoursesAsync(CancellationToken ct);
        Task addCourseAsync(Course course, CancellationToken ct);
        Task<Course?> getCourseByIdAsync(int courseId, CancellationToken ct);
        Task updateCourseAsync(Course course);
        Task<Course?> getCourseWithLessonsByIdAsync(int courseId, CancellationToken ct);
        Task deleteCourseByIDAsync(int courseId, CancellationToken ct);
        Task<IEnumerable<Course>> getCoursesByInstructorIdAsync(int instructorId, CancellationToken ct);
        Task<IEnumerable<Course>> getEnrolledCoursesByStudentIdAsync(int studentId, CancellationToken ct);
    }
}

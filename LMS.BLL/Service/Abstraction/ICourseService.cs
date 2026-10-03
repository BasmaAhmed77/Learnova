using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Database;
//using LMS.DAL.Entities;
using LMS.BLL.ModelVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BLL.Service.Abstraction
{
    public interface ICourseService
    {

        public string uploadFileToCloudinary(IFormFile file, string folderName);

        public List<string> getLevels();
        public List<string> getCategories();
        public List<string> getLanguages();
        public bool isPriceValid(CourseVM course);
        public bool isImageValid(CourseVM course, IFormFile imageFile);
        Task<IEnumerable<CourseVM>> getAllCourses(CancellationToken ct = default);
        Task addCourse(CourseVM course, string userId, CancellationToken ct = default);
        Task<CourseVM?> getCourseById(int courseId, CancellationToken ct = default);
        Task<CourseVM> updateCourse(CourseVM course, CancellationToken ct = default);
        Task<CourseVM?> getCourseWithLessonsById(int courseId, CancellationToken ct = default);
        Task deleteCourseByID(int courseId, CancellationToken ct = default);

        Task<IEnumerable<CourseVM>> getMyCoursesAsync(string userId, string role, CancellationToken ct = default);
        Task<List<int>> GetEnrolledCourseIdsAsync(string userId, CancellationToken ct = default);
        Task<bool> EnrollStudentAsync(string userId, int courseId, CancellationToken ct);
        Task<bool> IsCourseOwner(int courseId, string userId, CancellationToken ct);
    }
}


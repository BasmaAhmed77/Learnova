using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LMS.BLL.ModelVM;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Database;
using LMS.DAL.Entities;
using LMS.DAL.Repo.Abstraction;
using LMS.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LMS.BLL.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CloudinarySettings _cloudinarySettings;

        public CourseService(
            ICourseRepo repo,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinarySettings = cloudinaryOptions?.Value ?? throw new ArgumentNullException(nameof(cloudinaryOptions));
        }
        public async Task<IEnumerable<CourseVM>> getAllCourses(CancellationToken ct)
        {
            var courses = await _repo.getAllCoursesAsync(ct);
            return _mapper.Map<IEnumerable<CourseVM>>(courses);
        }

        public async Task addCourse(CourseVM courseVM, string userId, CancellationToken ct)
        {
            var instructor = await _unitOfWork.Instructors.GetByUserIdAsync(userId, ct);
            var course = _mapper.Map<Course>(courseVM);
            course.InstructorId = instructor.InstructorId;
            await _repo.addCourseAsync(course, ct);
            await _unitOfWork.CompleteAsync(ct);
            courseVM.CourseId = course.CourseId;
        }

        public async Task<CourseVM?> getCourseById(int courseId, CancellationToken ct)
        {
            var course = await _repo.getCourseByIdAsync(courseId, ct);
            return _mapper.Map<CourseVM>(course);
        }

        public async Task<CourseVM> updateCourse(CourseVM courseVM, CancellationToken ct)
        {
            var course = _mapper.Map<Course>(courseVM);
            await _repo.updateCourseAsync(course);
            await _unitOfWork.CompleteAsync(ct);
            return _mapper.Map<CourseVM>(course);
        }

        public async Task deleteCourseByID(int courseId, CancellationToken ct)
        {
            await _repo.deleteCourseByIDAsync(courseId, ct);
            await _unitOfWork.CompleteAsync(ct);
        }

        public async Task<IEnumerable<CourseVM>> getMyCoursesAsync(string userId, string role, CancellationToken ct)
        {
            if (role == "Instructor")
            {
                var instructor = await _unitOfWork.Instructors.GetByUserIdAsync(userId, ct);
                var courses = await _repo.getCoursesByInstructorIdAsync(instructor.InstructorId, ct);
                return _mapper.Map<IEnumerable<CourseVM>>(courses);
            }
            else
            {
                var student = await _unitOfWork.Students.GetByUserIdAsync(userId, ct);
                var courses = await _repo.getEnrolledCoursesByStudentIdAsync(student.StudentId, ct);
                return _mapper.Map<IEnumerable<CourseVM>>(courses);
            }
        }

        public async Task<List<int>> GetEnrolledCourseIdsAsync(string userId, CancellationToken ct)
        {
            var student = await _unitOfWork.Students.GetByUserIdAsync(userId, ct);
            if (student == null)
                return new List<int>();
            return await _unitOfWork.Students.GetEnrolledIdsByStudentIdAsync(student.StudentId, ct);
        }

        public async Task<CourseVM?> getCourseWithLessonsById(int courseId, CancellationToken ct)
        {
            var course = await _repo.getCourseWithLessonsByIdAsync(courseId, ct);
            return _mapper.Map<CourseVM>(course);
        }
        public List<string> getLevels()
        {
            List<string> levels;
            return levels = new List<string> { "Beginner", "Intermediate", "Advanced" };
        }
        public List<string> getCategories()
        {
            List<string> categories;
            return categories = new List<string> { "Frontend", "Backend", "Software Testing", "Embedded Systems", "Artificial Intelligence", "Robotechs" };
        }
        public List<string> getLanguages()
        {
            List<string> languages;
            return languages = new List<string> { "English", "Arabic", "Indian", "French", "Spanish", "Chinese" };
        }
        public bool isPriceValid(CourseVM course)
        {
            if (course.isFree == "Free")
            {
                course.Price = 0;
            }
            else if ((course.isFree == "Paid") && (course.Price < 100 || course.Price > 10000))
            {
                return false;
            }
            return true;
        }

        public bool isImageValid(CourseVM course, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return false;
            }
            else
            {
                var imageExtentions = new[] { ".png", ".jpg", ".jpeg" };
                var extention = Path.GetExtension(imageFile.FileName).ToLower();

                if (!imageExtentions.Contains(extention))
                {
                    return false;
                }
                else
                {
                    course.imageURL = uploadFileToCloudinary(imageFile, "Images");
                }
            }
            return true;
        }

        //public string uploadFileToCloudinary(IFormFile file, string folderName)
        //{
        //    var account = new Account
        //    (
        //        "dybsseasq",
        //        "175651579712685",
        //        "LWAXfnfE3573q1no8K-is68eVZU"
        //    );
        //    var cloudinary = new Cloudinary(account);

        //    using (var stream = file.OpenReadStream())
        //    {
        //        var uploadParams = new ImageUploadParams()
        //        {
        //            Folder = folderName,
        //            File = new FileDescription(file.FileName, stream)
        //        };

        //        var url = cloudinary.Upload(uploadParams);
        //        return url.SecureUrl.ToString();
        //    }
        //}
        public string uploadFileToCloudinary(IFormFile file, string folderName)
        {
            var account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            var cloudinary = new Cloudinary(account);

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    Folder = folderName,
                    File = new FileDescription(file.FileName, stream)
                };

                var result = cloudinary.Upload(uploadParams);

                if (result == null)
                {
                    throw new Exception("Cloudinary returned a null result.");
                }

                if (result.Error != null)
                {
                    throw new Exception(
                        $"Cloudinary upload failed: {result.Error.Message}"
                    );
                }

                if (result.SecureUrl == null)
                {
                    throw new Exception(
                        "Cloudinary upload succeeded but no SecureUrl was returned."
                    );
                }

                return result.SecureUrl.ToString();
            }
        }

        public async Task<bool> EnrollStudentAsync(string userId, int courseId, CancellationToken ct)
        {
            var student = await _unitOfWork.Students.GetByUserIdAsync(userId, ct);
            if (student == null) return false;

            bool alreadyEnrolled = await _unitOfWork.Students.IsEnrolledAsync(student.StudentId, courseId, ct);
            if (alreadyEnrolled)
                return true;

            var enrollment = new StudentCourse
            {
                StudentId = student.StudentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now
            };
            await _unitOfWork.Students.EnrollInCourseAsync(enrollment, ct);
            return await _unitOfWork.CompleteAsync(ct) > 0;
        }
        public async Task<bool> IsCourseOwner(int courseId, string userId, CancellationToken ct)
        {
            var course = await _repo.getCourseByIdAsync(courseId, ct);
            var instructor = await _unitOfWork.Instructors.GetByUserIdAsync(userId, ct);

            return course != null && instructor != null && course.InstructorId == instructor.InstructorId;
        }
    }
}

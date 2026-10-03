using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LMS.BLL.ModelVM;
using LMS.BLL.Service.Abstraction;
using LMS.DAL.Database;
using LMS.DAL.Repo.Abstraction;
using Microsoft.AspNetCore.Authorization;
//using LMS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;


namespace LMS.PL.Controllers
{
    public class CourseController : Controller
    {

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        private void ViewBagComponents()
        {
            ViewBag.categories = _courseService.getCategories();
            ViewBag.languages = _courseService.getLanguages();
            ViewBag.levels = _courseService.getLevels();
        }

        [HttpGet]
        public async Task<IActionResult> showAllCourses(CancellationToken ct)
        {
            var courses = await _courseService.getAllCourses(ct);
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Student"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.EnrolledIds = await _courseService.GetEnrolledCourseIdsAsync(userId, ct);
            }
            return View("ShowAllCourses", courses);
        }

        [Authorize(Roles = "Instructor")]
        public IActionResult createCourse()
        {
            ViewBagComponents();
            return View("CreateCourse", new CourseVM());
        }
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> addCourse(CourseVM course, IFormFile imageFile, CancellationToken ct)
        {
            bool isValid = validation(course, imageFile);
            if (!isValid)
            {
                ViewBagComponents();
                return View("CreateCourse", course);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _courseService.addCourse(course, userId, ct);
            if (course.CourseId > 0)
            {
                TempData["SuccessMessage"] = "Course created successfully! You can now add lessons.";
                return RedirectToAction("createLesson", "Lesson", new { CourseId = course.CourseId });
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong while saving the course. Please try again.");
                ViewBagComponents();
                return View("CreateCourse", course);
            }
        }
        public bool validation(CourseVM course, IFormFile imageFile)
        {
            bool isPriceValid = _courseService.isPriceValid(course);
            if(!isPriceValid)
            {
                ModelState.AddModelError("Price", "Price must be in [100$ , 10000$]");
            }

            bool isImageValid = _courseService.isImageValid(course, imageFile);
            if (!isImageValid)
            {
                ModelState.AddModelError("imageURL", "Image is Not Valid");
            }

            if (!ModelState.IsValid)
            {
                return false;
            }
            return true;
        }

        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> editCourse(int CourseId, CancellationToken ct)
        {
            ViewBagComponents();
            CourseVM course = await _courseService.getCourseById(CourseId,ct);
            return View("EditCourse", course);
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost] 
        public async Task<IActionResult> saveEditedCourse(CourseVM course, IFormFile imageFile, CancellationToken ct)
        {
            bool isValid = validation(course, imageFile);
            if (!isValid)
            {
                ViewBagComponents();
                return View("EditCourse", course);
            }
            CourseVM editedCourse = await _courseService.updateCourse(course, ct);

            TempData["SuccessMessage"] = "Course updated successfully!";
            return RedirectToAction("Details", new { CourseId = course.CourseId });
        }

       
        [Authorize]
        public async Task<IActionResult> Details(int CourseId, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isOwner = await _courseService.IsCourseOwner(CourseId, userId, ct);
            ViewBag.IsOwner = isOwner;

            var course = await _courseService.getCourseWithLessonsById(CourseId, ct);
            if (isOwner)
            {
                return View("ShowCourseDetailsToEdit", course);
            }
            return View("ShowCourseDetailsToEnroll", course);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> deleteCourse(int CourseId, CancellationToken ct)
        {
            await _courseService.deleteCourseByID(CourseId,ct);
            TempData["SuccessMessage"] = "Course deleted successfully";
            return RedirectToAction("MyCourses");
        }
        [Authorize]
        public async Task<IActionResult> MyCourses(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.IsInRole("Instructor") ? "Instructor" : "Student";
            var courses = await _courseService.getMyCoursesAsync(userId, userRole, ct);
            return View("MyCourses", courses);
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> enrollCourse(int CourseId, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = await _courseService.EnrollStudentAsync(userId, CourseId,ct);

            if (result)
            {
                TempData["SuccessMessage"] = "Successfully enrolled in the course!";
                return RedirectToAction("Details", new { CourseId = CourseId });
            }

            TempData["ErrorMessage"] = "Something went wrong. Please try again.";
            return RedirectToAction("Details", new { CourseId = CourseId });
            
        }
    }
}
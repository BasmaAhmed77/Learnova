using LMS.BLL.ModelVM.User;
using LMS.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IInstructorService _instructorService;
        private readonly IStudentService _studentService;

        public UserController(IUserService userService,
                              IInstructorService instructorService,
                              IStudentService studentService)
        {
            _userService = userService;
            _instructorService = instructorService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Instructor"))
            {
                var instructor = await _instructorService.GetByUserIdAsync(userId);
                return View(instructor);
            }

            var student = await _studentService.GetByUserIdAsync(userId);
            return View(student);
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateName(string fname, string lname)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.UpdateProfileNameAsync(userId, fname, lname);

            if (result)
            {
                TempData["SuccessMessage"] = "Name updated successfully!";
            }
            else
                TempData["ErrorMessage"] = "Failed to update Name.";
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoneNumber(UpdatePhoneNumberVM model)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                TempData["ErrorMessage"] = firstError ?? "Invalid data";
                return RedirectToAction(nameof(Profile));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.SetPhoneNumberAsync(userId,model);
            if (result)
            {
                TempData["SuccessMessage"] = "Phone number updated successfully!";
            }
            else
                TempData["ErrorMessage"] = "Failed to update phone number.";

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.RemovePhoneNumberAsync(userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Phone number removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove phone number.";
            }
            return RedirectToAction(nameof(Profile));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(UploadProfilePictureVM model)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction(nameof(Profile));
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.SetProfilePictureAsync(model.Picture, userId);
            if (result)
                TempData["SuccessMessage"] = "Profile picture updated successfully!";
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProfilePicture()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.RemoveProfilePictureAsync(userId);

            if (result)
                TempData["SuccessMessage"] = "Profile picture removed.";

            return RedirectToAction(nameof(Profile));
        }
    }
}


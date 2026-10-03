using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.ModelVM.Student;   
using LMS.BLL.ModelVM.User;    
using LMS.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
namespace LMS.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, error) = await _authService.RegisterStudentAsync(model);

                if (succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError("", error);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterInstructor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInstructor(RegisterInstructorVM model)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, error) = await _authService.RegisterInstructorAsync(model);

                if (succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, error, role) = await _authService.LoginAsync(model);

                if (succeeded)
                {
                    if (role == "Admin") 
                        return RedirectToAction("Index", "Admin");
                    return RedirectToAction("showAllCourses", "Course");
                }
                ModelState.AddModelError("", error);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var (token, error) = await _authService.GeneratePasswordResetTokenAsync(email);

            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("", error);
                return View();
            }

            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { email = email, token = token }, Request.Scheme);

            ViewBag.ResetLink = callbackUrl;
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return RedirectToAction(nameof(Login));

            var model = new ResetPasswordVM { Email = email, Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, error) = await _authService.ResetPasswordAsync(model);
                if (succeeded) 
                    return RedirectToAction(nameof(Login));
                ModelState.AddModelError("", error);
            }
            return View(model);
        }


        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) 
                    return RedirectToAction(nameof(Login));

                var (succeeded, error) = await _authService.ChangePasswordAsync(model, userId);

                if (succeeded)
                {
                    TempData["SuccessMessage"] = "Password reset successfully! You can now login.";
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError("", error);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

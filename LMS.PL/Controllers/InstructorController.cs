using LMS.BLL.ModelVM.Instructor;
using LMS.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTitle(UpdateInstructorTitleVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                TempData["ErrorMessage"] = firstError ?? "Invalid title data";
                return RedirectToAction("Profile", "User");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _instructorService.UpdateTitleAsync(userId, model,ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Job title updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update job title.";
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAcademicDegree(UpdateInstructorAcademicDegreeVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                TempData["ErrorMessage"] = firstError ?? "Invalid degree data";
                return RedirectToAction("Profile", "User");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _instructorService.UpdateAcademicDegreeAsync(userId,model,ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Academic degree updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update academic degree.";
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBio(UpdateInstructorBioVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                TempData["ErrorMessage"] = firstError ?? "Invalid bio data";
                return RedirectToAction("Profile", "User");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _instructorService.UpdateBioAsync(userId,model,ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Biography updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update biography.";
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExperience(UpdateInstructorYearsOfExperienceVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                TempData["ErrorMessage"] = firstError ?? "Invalid experience data";
                return RedirectToAction("Profile", "User");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _instructorService.UpdateExperienceAsync(userId,model,ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Years of experience updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update experience.";
            }

            return RedirectToAction("Profile", "User");
        }

    }
}

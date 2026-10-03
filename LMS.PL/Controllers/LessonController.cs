using System.Threading.Tasks;
using LMS.DAL.Database;
//using LMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using LMS.BLL.Service.Abstraction;
using LMS.BLL.ModelVM;



namespace LMS.PL.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonService _LessonService;

        public LessonController(ILessonService _service)
        {
            _LessonService = _service;
        }
        public async Task<IActionResult> createLesson(int CourseId) //create new lesson
        {
            var lesson = await _LessonService.createLessonByCourseID(CourseId);
            return View("CreateLesson", lesson);
        }


        [HttpPost]
        public async Task<IActionResult> editLesson(int LessonID)
        {
            var lesson = await _LessonService.getLessonById(LessonID);
            return View("EditLesson", lesson);
        }

        [HttpPost]
        public async Task<IActionResult> saveEditedLesson(LessonVM lesson)
        {
            string id = await _LessonService.getVideoID(lesson.url);
            if (id == null)
            {
                return View("EditLesson", lesson);
            }
            await _LessonService.updateLesson(lesson , id);
            TempData["SuccessMessage"] = "Lesson updated successfully!";
            return RedirectToAction("Details", "Course", new { CourseId = lesson.CourseId });
        }

        [HttpPost]
        public async Task<IActionResult> deleteLesson(int LessonId, int CourseId)
        {
            await _LessonService.deleteLessonById(LessonId, CourseId);
            TempData["SuccessMessage"] = "Lesson deleted successfully!";
            return RedirectToAction("Details", "Course", new { CourseId = CourseId });
        }

        [HttpPost]
        public async Task<IActionResult> addLesson(LessonVM lesson) //add lesson to db
        {
            string id = await _LessonService.getVideoID(lesson.url);
            if (id == null)
            {
                return View("CreateLesson", lesson);
            }
            await _LessonService.addLesson(lesson, id);
            return View("ShowLessonDetails", lesson);
        }

    }
}

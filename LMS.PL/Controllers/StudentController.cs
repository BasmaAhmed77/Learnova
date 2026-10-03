using Microsoft.AspNetCore.Mvc;

namespace LMS.PL.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

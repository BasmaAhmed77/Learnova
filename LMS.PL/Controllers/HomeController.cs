using System.Diagnostics;
using System.Threading.Tasks;
using LMS.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LMS.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService service)
        {
            _logger = logger;
            _courseService = service;
        }

        public async Task<IActionResult> Index()
        {
            var courses= await _courseService.getAllCourses();
            ViewBag.Categories = _courseService.getCategories();
            return View("Index",courses);
        }

       
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

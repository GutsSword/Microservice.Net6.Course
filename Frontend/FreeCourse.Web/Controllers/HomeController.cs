using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FreeCourse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatologService catologService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICatologService catologService)
        {
            _logger = logger;
            this.catologService = catologService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await catologService.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var values = await catologService.GetByCourseId(id);
            return View(values);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if(errorFeature!= null && errorFeature.Error is UnAuthorizeException)
            {
                return RedirectToAction("Logout", "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

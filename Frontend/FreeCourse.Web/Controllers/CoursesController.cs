using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatologService catologService;
        private readonly ISharedIdentityService sharedIdentityService;

        public CoursesController(ICatologService catologService, ISharedIdentityService sharedIdentityService)
        {
            this.catologService = catologService;
            this.sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var user = sharedIdentityService.GetUserId;
            var courses = await catologService.GetAllCourseByUserIdAsync(user);

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await catologService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel createCourseViewModel)
        {
            var user = sharedIdentityService.GetUserId;
            var categories = await catologService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "CategoryId", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            createCourseViewModel.UserId = user;
            createCourseViewModel.Picture = "Herhangi bir resim eklenmedi.";

            await catologService.CreateCourseAsync(createCourseViewModel);
            
            return RedirectToAction("Index","Courses");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var course = await catologService.GetByCourseId(id);
            var categories = await catologService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "CategoryId", "Name");

            UpdateCourseViewModel updateCourseViewModel = new()
            {
                CourseId = course.CourseId,
                CategoryId = course.CategoryId,
                Description = course.Description,
                Feature = course.Feature,
                Name = course.Name,
                Picture = course.Picture,
                Price = course.Price,
                UserId = course.UserId,
                
            };        

            return View(updateCourseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseViewModel updateCourseViewModel)
        {
            var categories = await catologService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "CategoryId", "Name",updateCourseViewModel.CourseId);

            await catologService.UpdateCourseAsync(updateCourseViewModel);

            return RedirectToAction("Index", "Courses");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {      
            await catologService.DeleteCourseAsync(id);

            return RedirectToAction("Index", "Courses");
        }
    }
}

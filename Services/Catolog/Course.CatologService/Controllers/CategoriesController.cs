using FreeCourse.CatologService.Dtos.CategoryDtos;
using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.CatologService.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.CatologService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {

        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var courseList = await categoryService.GetAllAsync();
            return CreateActionResultInstance(courseList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategories(string id)
        {
            var response = await categoryService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var response = await categoryService.CreateAsync(createCategoryDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateCourse(string id)
        {
            var response = await categoryService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}

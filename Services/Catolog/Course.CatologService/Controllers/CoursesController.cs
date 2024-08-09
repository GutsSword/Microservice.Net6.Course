using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.CatologService.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.CatologService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courseList = await courseService.GetAllAsync();
            return CreateActionResultInstance(courseList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCourse(string id)
        {
            var response = await courseService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpGet("GetByUserIdCourse/{id}")]
        public async Task<IActionResult> GetByUserIdCourse(string id)
        {
            var response = await courseService.GetAllByUserIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            var response = await courseService.CreateAsync(createCourseDto);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            var response = await courseService.UpdateAsync(updateCourseDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var response = await courseService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}

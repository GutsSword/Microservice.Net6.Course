using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.CatologService.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(CreateCourseDto createCourseDto);
        Task<Response<NoContent>> DeleteAsync(string id);
        Task<Response<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto);
    }
}

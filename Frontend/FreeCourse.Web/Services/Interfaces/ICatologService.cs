using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatologService
    {
        Task<List<CourseViewModel>> GetAllCoursesAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetByCourseId(string courseId);
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<bool> DeleteCourseAsync(string courseId);
        Task<bool> CreateCourseAsync(CreateCourseViewModel createCourseViewModel);
        Task<bool> UpdateCourseAsync(UpdateCourseViewModel updateCourseViewModel);

    }
}

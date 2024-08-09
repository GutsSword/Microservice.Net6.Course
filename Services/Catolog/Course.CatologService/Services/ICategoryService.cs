using FreeCourse.CatologService.Dtos.CategoryDtos;
using FreeCourse.CatologService.Entities;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.CatologService.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> DeleteAsync(string id);
    }
}

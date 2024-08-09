using AutoMapper;
using FreeCourse.CatologService.Dtos;
using FreeCourse.CatologService.Dtos.CategoryDtos;
using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.CatologService.Entities;

namespace FreeCourse.CatologService.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<CategoryDto,UpdateCategoryDto>().ReverseMap();
            CreateMap<CategoryDto, CreateCategoryDto>().ReverseMap();

            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
            CreateMap<CourseDto, CreateCourseDto>().ReverseMap();
            CreateMap<CourseDto, UpdateCourseDto>().ReverseMap();

            CreateMap<Feature, FeatureDto>().ReverseMap();
            
        }
    }
}

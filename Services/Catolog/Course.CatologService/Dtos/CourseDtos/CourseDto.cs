using FreeCourse.CatologService.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FreeCourse.CatologService.Dtos.CategoryDtos;

namespace FreeCourse.CatologService.Dtos.CourseDtos
{
    public class CourseDto
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}

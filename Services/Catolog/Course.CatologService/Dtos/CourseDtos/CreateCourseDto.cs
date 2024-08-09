using FreeCourse.CatologService.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.CatologService.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate{ get; set; }

        public decimal Price { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

    }
}

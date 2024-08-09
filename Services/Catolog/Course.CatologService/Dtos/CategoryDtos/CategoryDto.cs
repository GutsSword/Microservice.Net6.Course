using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.CatologService.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
    }
}

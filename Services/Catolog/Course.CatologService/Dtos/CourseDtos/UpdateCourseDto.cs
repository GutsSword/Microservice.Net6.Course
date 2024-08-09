using FreeCourse.CatologService.Entities;

namespace FreeCourse.CatologService.Dtos.CourseDtos
{
    public class UpdateCourseDto
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }

        public decimal Price { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }
    }
}

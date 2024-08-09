using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.CatologService.Entities
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }      

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public Feature Feature { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }

    }
}

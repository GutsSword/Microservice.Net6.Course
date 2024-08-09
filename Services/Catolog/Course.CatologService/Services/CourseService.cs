using AutoMapper;
using FreeCourse.CatologService.Dtos.CourseDtos;
using FreeCourse.CatologService.Entities;
using FreeCourse.CatologService.Settings;
using FreeCourse.Shared.Dtos;
using Mass=MassTransit;
using MongoDB.Driver;
using FreeCourse.Shared.Messages;
using FreeCourse.Shared.Services;

namespace FreeCourse.CatologService.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> courseCollection;
        private readonly IMongoCollection<Category> categoryCollection;
        private readonly IMapper mapper;
        private readonly Mass.IPublishEndpoint publishEndpoint;

        public CourseService(IDatabaseSettings databaseSettings, IMapper mapper, Mass.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Response<CourseDto>> CreateAsync(CreateCourseDto createCourseDto)
        {
            var map = mapper.Map<Course>(createCourseDto);
            map.CreatedDate = DateTime.Now;

            await courseCollection.InsertOneAsync(map);

            var resultMap = mapper.Map<CourseDto>(map);
            return Response<CourseDto>.Success(resultMap, 201);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await courseCollection.DeleteOneAsync(x=>x.CourseId==id);
            if(result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Course Not Found.", 404);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await courseCollection.Find(x => true).ToListAsync();

            if (courses.Any())
            {
                foreach(var item in courses)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryId == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
         
            var map = mapper.Map<List<CourseDto>>(courses);

            return Response<List<CourseDto>>.Success(map, 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var courseById = await courseCollection.Find(x => x.CourseId == id).FirstOrDefaultAsync();
         
            if (courseById is null)
            {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }

            courseById.Category = await categoryCollection.Find(x => x.CategoryId == courseById.CategoryId).FirstAsync();
            var map = mapper.Map<CourseDto>(courseById);

            return Response<CourseDto>.Success(map, 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string id)
        {
            var courses = await courseCollection.Find(x => x.UserId == id).ToListAsync();

            if (courses.Any())
            {
                foreach(var item in courses)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryId == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            var map = mapper.Map<List<CourseDto>>(courses);

            return Response<List<CourseDto>>.Success(map, 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto)
        {
            var map = mapper.Map<Course>(updateCourseDto);
            var result = await courseCollection.FindOneAndReplaceAsync(x=>x.CourseId == updateCourseDto.CourseId, map);
            if(result is null)
            {
                return Response<NoContent>.Fail("Course Not Found.", 404);
            }

            await publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent
            {
                CourseId = updateCourseDto.CourseId,
                UpdatedName = updateCourseDto.Name,
                UserId = updateCourseDto.UserId,
            });

            return Response<NoContent>.Success(204);
        }
    }
}

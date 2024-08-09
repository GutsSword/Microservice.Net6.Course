using AutoMapper;
using FreeCourse.CatologService.Dtos.CategoryDtos;
using FreeCourse.CatologService.Entities;
using FreeCourse.CatologService.Settings;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FreeCourse.CatologService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> categoryCollection;
        private readonly IMapper mapper;

        public CategoryService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            this.mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await categoryCollection.Find(x => true).ToListAsync();
            var map = mapper.Map<List<CategoryDto>>(categories);

            return Response<List<CategoryDto>>.Success(map, 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var map = mapper.Map<Category>(createCategoryDto);
            await categoryCollection.InsertOneAsync(map);
            var resultMap = mapper.Map<CategoryDto>(map);

            return Response<CategoryDto>.Success(resultMap, 201);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var categoryById = await categoryCollection.Find(x=>x.CategoryId == id).FirstOrDefaultAsync();
            if (categoryById is null)
                return Response<CategoryDto>.Fail("Category Not Found", 404);

            var resultMap = mapper.Map<CategoryDto>(categoryById);

            return Response<CategoryDto>.Success(resultMap, 200);
        }

        public async Task<Response<CategoryDto>> DeleteAsync(string id)
        {           
            await categoryCollection.DeleteOneAsync(x=>x.CategoryId == id);

            return Response<CategoryDto>.Success(200);
        }

    }
}


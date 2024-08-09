using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Models.PhotoStock;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace FreeCourse.Web.Services.Concrete
{
    public class CatologService : ICatologService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper photoHelper;

        public CatologService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            this.photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseViewModel createCourseViewModel)
        {
            var photo = await _photoStockService.UploadPhoto(createCourseViewModel.PhotoFormFile);

            if (photo is not null)
                createCourseViewModel.Picture = photo.Url;

            var response = await _httpClient.PostAsJsonAsync<CreateCourseViewModel>("courses", createCourseViewModel);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return data.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"Courses/GetByUserIdCourse/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            data.Data.ForEach(x =>
            {
                x.StockPictureUrl = photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return data.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            // BaseUrl program.cs ' te tanımlandı.
            // http://localhost:5000/services/catolog/

            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            data.Data.ForEach(x =>
            {
                x.StockPictureUrl = photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return data.Data;

        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            data.Data.StockPictureUrl = photoHelper.GetPhotoStockUrl(data.Data.Picture);

            return data.Data;
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseViewModel updateCourseViewModel)
        {
            var photo = await _photoStockService.UploadPhoto(updateCourseViewModel.PhotoFormFile);

            if (photo is not null)
            {
                await _photoStockService.DeletePhoto(updateCourseViewModel.Picture);
                updateCourseViewModel.Picture = photo.Url;
            }               

            var response = await _httpClient.PutAsJsonAsync<UpdateCourseViewModel>("courses", updateCourseViewModel);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}

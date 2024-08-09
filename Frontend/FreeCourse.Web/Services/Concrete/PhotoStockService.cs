using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.PhotoStock;
using FreeCourse.Web.Services.Interfaces;
using System.IO;

namespace FreeCourse.Web.Services.Concrete
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }             

            var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";  //Guid.jpg

            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);

            var multiPartContent = new MultipartFormDataContent();
            multiPartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "photo", randomFileName);

            var response = await httpClient.PostAsync("photos", multiPartContent);

            if (!response.IsSuccessStatusCode)
                return null;

            var viewModel = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

            return viewModel.Data;
        }
    }
}

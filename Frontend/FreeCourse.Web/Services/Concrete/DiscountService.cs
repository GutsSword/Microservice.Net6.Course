using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Discount;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly ISharedIdentityService identityService;

        public DiscountService(HttpClient httpClient, ISharedIdentityService identityService)
        {
            _httpClient = httpClient;
            this.identityService = identityService;
        }

        public async Task<DiscountViewModel> GetDiscountCode(string discountCode)
        {
            var user = identityService.GetUserId;
            var response = await _httpClient.GetAsync($"discount/GetByCode/{discountCode}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var values = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

            return values.Data;
            
        }
    }
}

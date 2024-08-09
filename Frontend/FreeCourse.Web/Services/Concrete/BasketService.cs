using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;

namespace FreeCourse.Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient httpClient;
        private readonly IDiscountService discountService;
        private readonly ISharedIdentityService identityService;

        public BasketService(HttpClient httpClient, IDiscountService discountService, ISharedIdentityService identityService)
        {
            this.httpClient = httpClient;
            this.discountService = discountService;
            this.identityService = identityService;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetBasket();

            if (basket != null)
            {
                if (!basket.BasketItem.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItem.Add(basketItemViewModel);
                }
            }
            else
            {
                var userId = identityService.GetUserId;
                basket = new BasketViewModel();
                basket.UserId = userId;
                basket.BasketItem.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            var basket = await GetBasket();
            if(basket == null)
            {
                return false;
            }

            var discount = await discountService.GetDiscountCode(discountCode);

            if(discount == null)
            {
                return false;
            }

            basket.ApplyDiscount(discount.Code,discount.Rate);

            await SaveOrUpdate(basket);

            return true;

        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await GetBasket();

            if(basket is null || basket.DiscountCode == null)
            {
                return false;
            }

            basket.CancelDiscount();
          
            await SaveOrUpdate(basket);

            return true;

        }

        public async Task<bool> DeleteBasket()
        {
            var result = await httpClient.DeleteAsync("basket");
            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetBasket()
        {
            var response = await httpClient.GetAsync("basket");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await GetBasket();

            if(basket is null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItem.Remove(basket.BasketItem.First(x=>x.CourseId == courseId));

            if (!deleteBasketItem)
            {
                return false;
            }

            if (!basket.BasketItem.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);


        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<BasketViewModel>("basket",basketViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}

using FreeCourse.BasketService.Dtos;
using FreeCourse.BasketService.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreeCourse.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService basketService;
        private readonly ISharedIdentityService identityService;

        public BasketController(IBasketService basketService, ISharedIdentityService identityService)
        {
            this.basketService = basketService;
            this.identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var userId = identityService.GetUserId;
            var values = await basketService.GetBasket(userId);
            if(values == null)
            {
                return NotFound();
            }

            return CreateActionResultInstance(values);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            string UserId = identityService.GetUserId;
            var response = await basketService.SaveOrUpdate(basketDto);
          
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            var userId = identityService.GetUserId;
            var values = await basketService.Delete(userId);

            return CreateActionResultInstance(values);
        }
    }
}

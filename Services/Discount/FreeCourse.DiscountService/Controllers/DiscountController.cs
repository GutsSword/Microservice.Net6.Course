using FreeCourse.DiscountService.Entities;
using FreeCourse.DiscountService.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.DiscountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService service;
        private readonly ISharedIdentityService identityService;

        public DiscountController(IDiscountService service, ISharedIdentityService identityService)
        {
            this.service = service;
            this.identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountCodes()
        {
            var values = await service.GetAllDiscounts();
            return CreateActionResultInstance(values);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCodeById(int id)
        {
            var values = await service.GetByIdDiscount(id);
            return CreateActionResultInstance(values);
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = identityService.GetUserId;
            var values = await service.GetByCodeByUserId(code, userId);

            return CreateActionResultInstance(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCode(Discount discount )
        {
            var values = await service.Save(discount);

            return CreateActionResultInstance(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCode(Discount discount)
        {
            var values = await service.Update(discount);

            return CreateActionResultInstance(values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCode(int id)
        {
            var values = await service.Delete(id);

            return CreateActionResultInstance(values);
        }
    }
}

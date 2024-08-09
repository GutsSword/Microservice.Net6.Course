using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;
        private readonly ICatologService catologService;

        public BasketController(IBasketService basketService, ICatologService catologService)
        {
            this.basketService = basketService;
            this.catologService = catologService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await basketService.GetBasket();
            return View(response);
        }

        public async Task<IActionResult> AddToBasket(string id)
        {
            var courseItem = await catologService.GetByCourseId(id);
            var basketItem = new BasketItemViewModel
            {
                CourseId = courseItem.CourseId,
                Price = courseItem.Price,
                CourseName = courseItem.Name,
            };

            await basketService.AddBasketItem(basketItem);

            return RedirectToAction("Index", "Basket");
        }

        public async Task<IActionResult> DeleteBasketItem(string courseId)
        {       
            await basketService.RemoveBasketItem(courseId);
            return RedirectToAction("Index","Basket");
        }

        public async Task<IActionResult> ApplyDiscountCode(string discountCode)
        {
            if(!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).First();
                return RedirectToAction("Index", "Basket");
            }
            var discountStatus = await basketService.ApplyDiscount(discountCode);
            TempData["discountStatus"] = discountStatus;

            return RedirectToAction("Index","Basket");
        }

        public async Task<IActionResult> CancelAppliedDiscountCode()
        {
            await basketService.CancelApplyDiscount();

            return RedirectToAction("Index","Basket");
        }
    }
}

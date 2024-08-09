using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Concrete;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IBasketService basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            this.orderService = orderService;
            this.basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await basketService.GetBasket();
            ViewBag.basket = basket;

            return View(new CheckoutInfoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoViewModel checkoutInfoViewModel)
        {
            /* Senkron İletişim
            var orderStatus = await orderService.CreateOrder(checkoutInfoViewModel);
            */
            // Asenkron İletişim
            var orderSuspend = await orderService.SuspendOrder(checkoutInfoViewModel);

            var basket = await basketService.GetBasket();
            ViewBag.basket = basket;

            if (!orderSuspend.IsSuccessful)
            {
                ViewBag.Error = orderSuspend.Error;
                return View();
            }

            return RedirectToAction("SuccessfulCheckout", "Order", new { orderId = orderSuspend.OrderId });
        }

        public async Task<IActionResult> SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId; 
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            var getOrders = await orderService.GetAllOrders();
            return View(getOrders);
        }
    }
}

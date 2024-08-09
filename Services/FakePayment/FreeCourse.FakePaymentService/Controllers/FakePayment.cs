using FreeCourse.FakePaymentService.Dto;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.FakePaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePayment : ControllerBase
    {
        private readonly ISendEndpointProvider sendEndpointProvider;

        public FakePayment(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = paymentDto.Order.BuyerId,
                Province = paymentDto.Order.Address.Province,
                District = paymentDto.Order.Address.District,
                Line = paymentDto.Order.Address.Line,
                Street = paymentDto.Order.Address.Street,
                ZipCode = paymentDto.Order.Address.ZipCode,
             
            };

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                });
            });

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            return Ok("Payment process successfull.");
        }
    }
}

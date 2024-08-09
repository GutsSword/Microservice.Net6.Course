using FreeCourse.Services.OrderService.Application.CQRS.Commands;
using FreeCourse.Services.OrderService.Application.CQRS.Queries;
using FreeCourse.Services.OrderService.Application.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IMediator mediator;

        public OrdersController(ISharedIdentityService identityService, IMediator mediator)
        {
            _identityService = identityService;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = _identityService.GetUserId;
            var response = await mediator.Send(new GetOrdersByUserIdQuery { UserId = userId});

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await mediator.Send(createOrderCommand);

            return Ok(response);
        }
    }
}

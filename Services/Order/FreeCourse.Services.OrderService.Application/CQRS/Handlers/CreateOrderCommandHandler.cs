using FreeCourse.Services.OrderService.Application.CQRS.Commands;
using FreeCourse.Services.OrderService.Application.Dtos;
using FreeCourse.Services.OrderService.Domain.OrderAggregate;
using FreeCourse.Services.OrderService.Infrastructure.AppContext;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Application.CQRS.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
    {
        private readonly OrderDbContext context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            this.context = context;
        }

        public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Adresi ekle.
            var newAddress = new Address(
                request.Address.Province,
                request.Address.District,
                request.Address.Street,
                request.Address.ZipCode,
                request.Address.Line
                );

            // Order Ekle
            Order newOrder = new Order(request.BuyerId, newAddress);

            // Order Items Ekle
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Price);
            });
            
            await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();

            return new CreatedOrderDto { OrderId = newOrder.Id };
        }
    }
}

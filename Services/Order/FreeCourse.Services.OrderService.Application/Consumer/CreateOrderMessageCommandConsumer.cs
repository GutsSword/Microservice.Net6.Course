using FreeCourse.Services.OrderService.Domain.OrderAggregate;
using FreeCourse.Services.OrderService.Infrastructure.AppContext;
using FreeCourse.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Application.Consumer
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext dbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Address(context.Message.Province, context.Message.District, context.Message.Street, context.Message.ZipCode, context.Message.Line);

            Order order = new Order(context.Message.BuyerId, newAddress);

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Price);
            });

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }
    }
}

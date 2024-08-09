using FreeCourse.Services.OrderService.Infrastructure.AppContext;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Application.Consumer
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext dbContext;

        public CourseNameChangedEventConsumer(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await dbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

            orderItems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.UpdatedName, x.PictureUrl, x.Price);
            });

            await dbContext.SaveChangesAsync();
        }
    }
}

using FreeCourse.BasketService.Dtos;
using FreeCourse.BasketService.Redis;
using FreeCourse.BasketService.Services;
using FreeCourse.Shared.Messages;
using FreeCourse.Shared.Services;
using MassTransit;
using System.Text.Json;

namespace FreeCourse.BasketService.Consumer
{
    public class CourseNameChangedBasketConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly IBasketService basketService;

        public CourseNameChangedBasketConsumer(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var basketDto = await basketService.GetBasket(context.Message.UserId);

            if (basketDto != null )
            {
                basketDto.Data.basketItem.Where(x=>x.CourseId == context.Message.CourseId).ToList().ForEach(x =>
                {
                    x.CourseName = context.Message.UpdatedName;
                });

                await basketService.SaveOrUpdate(basketDto.Data);
            }


        }
    }
}

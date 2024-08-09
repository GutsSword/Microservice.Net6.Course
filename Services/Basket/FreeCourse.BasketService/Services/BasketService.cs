using FreeCourse.BasketService.Dtos;
using FreeCourse.BasketService.Redis;
using FreeCourse.Shared.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace FreeCourse.BasketService.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService redisService;

        public BasketService(RedisService redisService)
        {
            this.redisService = redisService;
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await redisService.GetDb().StringGetAsync(userId);
            if (existBasket.IsNullOrEmpty)
            {
                return Response<BasketDto>.Fail("Basket not found.",404);
            }

            var values = JsonSerializer.Deserialize<BasketDto>(existBasket);

            return Response<BasketDto>.Success(values, 200);
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await redisService.GetDb().KeyDeleteAsync(userId);
            if (!status)
                return Response<bool>.Fail("Basket not found.", 404);

            return Response<bool>.Success(204);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var values = JsonSerializer.Serialize(basketDto);
            var status = await redisService.GetDb().StringSetAsync(basketDto.UserId,values);
            if (!status)
                return Response<bool>.Fail("Something went wrong",500);

            return Response<bool>.Success(204);
        }
    }
}

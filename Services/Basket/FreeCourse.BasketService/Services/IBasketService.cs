using FreeCourse.BasketService.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.BasketService.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);

    }
}

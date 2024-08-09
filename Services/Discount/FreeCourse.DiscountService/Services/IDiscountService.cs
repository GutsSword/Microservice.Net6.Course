using FreeCourse.DiscountService.Entities;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.DiscountService.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAllDiscounts();
        Task<Response<Discount>> GetByIdDiscount(int id);
        Task<Response<NoContent>> Save(Discount discount);
        Task<Response<NoContent>> Update(Discount discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<Discount>> GetByCodeByUserId(string code, string userId);
    }
}

using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscountCode(string discountCode);
    }
}

using FluentValidation;
using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Validators.Discount
{
    public class DiscountApplyViewModelValidator : AbstractValidator<DiscountApplyViewModel>
    {
        public DiscountApplyViewModelValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Kupon Kodu Boş Geçilemez.");

            RuleFor(x => x.Rate)
                .NotEmpty()
                .InclusiveBetween(0,100)
                .WithMessage("Kupon Kodu Boş Geçilemez.");
        }
    }
}

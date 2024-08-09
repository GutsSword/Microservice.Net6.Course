using FluentValidation;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Validators.Catolog
{
    public class CreateCourseViewModelValidator : AbstractValidator<CreateCourseViewModel>
    {
        private const int MAX_DURATION = 1000;
        
        public CreateCourseViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("İsim alanı boş olamaz.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Açıklama alanı boş olamaz.");

            RuleFor(x => x.Feature.Duration)
                .InclusiveBetween(1, MAX_DURATION)
                .WithMessage("Kurs süresi boş olamaz.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Kurs Fiyatı boş olamaz.")
                .ScalePrecision(2,6)
                .WithMessage("Fiyat küsüratı en fazla 2 karakter içermelidir. (Örnek 249.99 TL)");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Kurs Kategorisi boş olamaz.");
                
        }
    }
}

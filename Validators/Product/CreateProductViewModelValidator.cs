using FluentValidation;
using ItiFinalProject.View_Model.Product;

namespace ItiFinalProject.Validators.Product
{
    public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("The title does not exceed 50 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is Required")
                .GreaterThan(0).WithMessage("The price must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("The quantity must be 0 or greater");

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Please select the category")
                .GreaterThan(0).WithMessage("Please select the category");
        }
    }
}

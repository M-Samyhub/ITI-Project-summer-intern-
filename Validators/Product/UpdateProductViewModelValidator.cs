using FluentValidation;
using ItiFinalProject.View_Model.Product;

namespace ItiFinalProject.Validators.Product
{
    public class UpdateProductViewModelValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductViewModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid product ID");

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


            When(x => x.ImgePath != null, () =>
            {
                RuleFor(x => x.ImgePath.Length)
                    .LessThanOrEqualTo(2 * 1024 * 1024).WithMessage("Image size must be less than 2MB");

                RuleFor(x => x.ImgePath.ContentType)
                    .Must(type => type.Equals("image/jpeg") || type.Equals("image/png") || type.Equals("image/jpg"))
                    .WithMessage("Only JPEG and PNG images are allowed");
            });
        }
    }
}

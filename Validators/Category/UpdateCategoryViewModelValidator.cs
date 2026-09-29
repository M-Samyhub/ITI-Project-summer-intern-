using FluentValidation;
using ItiFinalProject.View_Model.Category;

namespace ItiFinalProject.Validators.Category
{
    public class UpdateCategoryViewModelValidator : AbstractValidator<UpdateCategoryViewModel>
    {
        public UpdateCategoryViewModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The category ID is invalid.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(100).WithMessage("The category name does not exceed 100 characters.");

            RuleFor(x => x.Description)
                .Empty()
                .MaximumLength(2500).WithMessage("The category name does not exceed 2500 characters");
        }
    }
}

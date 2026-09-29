using FluentValidation;
using ItiFinalProject.View_Model.Category;

namespace ItiFinalProject.Validators.Category
{
    public class CreateCategoryViewModelValidator : AbstractValidator<CreateCategoryViewModel>
    {
        public CreateCategoryViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم القسم مطلوب.")
                .MaximumLength(100).WithMessage("The category name does not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2500).WithMessage("The category name does not exceed 2500 characters");
        }
    }
}

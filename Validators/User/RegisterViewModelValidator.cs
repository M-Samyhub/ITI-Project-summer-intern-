using FluentValidation;
using ItiFinalProject.View_Model.User;
using System.Collections.Generic;

namespace ItiFinalProject.Validators.User
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("The first name must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("The surname must not exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Emai is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("The password must be at least 6 characters long")
                .Matches(@"[A-Z]").WithMessage("The password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("The password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("The password must contain at least one number");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password confirmation is required")
                .Equal(x => x.Password).WithMessage("The passwords do not match");
        }
    }
}

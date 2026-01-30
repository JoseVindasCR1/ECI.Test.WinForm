using ECI.Test.BL.Validation;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Validators
{
    public class UserValidator : AbstractValidator<User>, IUserValidator
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MaximumLength(50)
                .WithMessage("Username cannot exceed 50 characters.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MaximumLength(255)
                .WithMessage("Password cannot exceed 255 characters.");
        }
    }
}
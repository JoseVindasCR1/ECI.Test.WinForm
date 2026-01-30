using ECI.Test.BL.Validation;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.DTOs;

namespace ECI.Test.BL.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>, ILoginDtoValidator
    {
        public LoginDtoValidator()
        {
            RuleFor(login => login.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(login => login.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
using ECI.Test.BL.Validation;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Validators
{
    public class WalkValidator : AbstractValidator<Walk>, IWalkValidator
    {
        public WalkValidator()
        {
            RuleFor(walk => walk.ClientId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Client ID is required.");

            RuleFor(walk => walk.DogId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Dog ID is required.");

            RuleFor(walk => walk.Date)
                .NotEmpty()
                .WithMessage("Walk date is required.");

            RuleFor(walk => walk.Duration)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Walk duration must be at least 1 minute.");
        }
    }
}
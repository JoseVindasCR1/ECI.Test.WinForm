using ECI.Test.BL.Validation;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Validators
{
    public class DogValidator : AbstractValidator<Dog>, IDogValidator
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name)
                .NotEmpty()
                .WithMessage("Dog name is required.")
                .MaximumLength(100)
                .WithMessage("Dog name cannot exceed 100 characters.");

            RuleFor(dog => dog.Breed)
                .MaximumLength(50)
                .WithMessage("Dog breed cannot exceed 50 characters.")
                .When(dog => !string.IsNullOrEmpty(dog.Breed));

            RuleFor(dog => dog.Age)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Dog age must be a positive number.");
        }
    }
}
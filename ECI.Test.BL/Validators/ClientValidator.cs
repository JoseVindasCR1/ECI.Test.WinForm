using ECI.Test.BL.Validation;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Validators
{
    public class ClientValidator : AbstractValidator<Client>, IClientValidator
    {
        public ClientValidator()
        {
            RuleFor(client => client.Name)
                .NotEmpty()
                .WithMessage("Client name is required.")
                .MaximumLength(100)
                .WithMessage("Client name cannot exceed 100 characters.");

            RuleFor(client => client.Phone)
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.")
                .When(client => !string.IsNullOrEmpty(client.Phone));
        }
    }
}
using Application.Features.Command.GenericCommands;
using FluentValidation;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CustomerHandler
{
    public class CreateCategoryCommandValidator : AbstractValidator<GenericCreateCommand<CustomerDto, CustomerDto>>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Entity.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(3).MaximumLength(20).WithMessage("First name must be between 3 and 20 characters.");

            RuleFor(x => x.Entity.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Entity.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Entity.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(10, 12).WithMessage("Phone number must be between 10 and 12 digits.");

            RuleFor(x => x.Entity.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(10).MaximumLength(15).WithMessage("Password must be between 10 and 15 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");
        }
    }
}

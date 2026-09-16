using Application.Features.Command.GenericCommands;
using FluentValidation;
using static Application.DTO.Auth;

public class RegisterValidator : AbstractValidator<GenericCreateCommand<CustomerDto, GenericResponse<string>>>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Entity.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");

        RuleFor(x => x.Entity.FirstName)
            .NotEmpty().WithMessage("First Name is required.");

        RuleFor(x => x.Entity.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Entity.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9][0-9]{7,14}$").WithMessage("Invalid phone number format.");

        // Uncomment and adjust password validation
        RuleFor(x => x.Entity.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}

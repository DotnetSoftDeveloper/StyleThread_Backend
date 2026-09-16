using Application.DTO;
using Application.Features.Command.GenericCommands;
using FluentValidation;

namespace Application.Features.Handlers.CategoryHandler
{
    public class CreateCategoryValidator : AbstractValidator<GenericCreateCommand<CategoryDto, CategoryDto>>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Entity.Name).NotEmpty().NotNull().WithMessage("CategoryName is required.");
        }
    }
}

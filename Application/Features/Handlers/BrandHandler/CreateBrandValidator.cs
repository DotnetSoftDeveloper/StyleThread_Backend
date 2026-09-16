using Application.DTO;
using Application.Features.Command.GenericCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.BrandHandler
{
    public class CreateCategoryCommandValidator : AbstractValidator<GenericCreateCommand<BrandDto, BrandDto>>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Entity.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}

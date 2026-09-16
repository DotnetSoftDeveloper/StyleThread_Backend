using Application.DTO;
using Application.Features.Command.GenericCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.CartHandler
{
    public class CreateCategoryCommandValidator : AbstractValidator<GenericCreateCommand<CartDto, CartDto>>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Entity.CartId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Entity.ProductId).NotEmpty().NotNull().WithMessage("{PropertyName} is required."); 
            RuleFor(x => x.Entity.CustomerId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Entity.Quantity).NotEmpty().NotNull().LessThan(5).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}

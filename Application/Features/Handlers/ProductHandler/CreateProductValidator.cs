using Application.DTO;
using Application.Features.Command.GenericCommands;
using FluentValidation;

namespace Application.Features.Handlers.ProductHandler
{
    public class RegisterValidator : AbstractValidator<GenericCreateCommand<ProductDto, ProductDto>>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Entity.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Entity.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Entity.BrandId)
                .NotEmpty().WithMessage("Brand ID is required.");

            //RuleFor(x => x.Entity.UploadImages)
            //    .NotEmpty().WithMessage("Image is required.");

            RuleFor(x => x.Entity.ListedBy)
                .NotEmpty().WithMessage("Listed By is required.")
                .MaximumLength(50).WithMessage("Listed By must not exceed 50 characters.");

            //RuleFor(x => x.Entity.Price)
            //    .NotEmpty().WithMessage("Price is required.")
            //    .GreaterThan(0).WithMessage("Price must be greater than 0.");

            //RuleFor(x => x.Entity.SalePrice)
            //    .NotEmpty().WithMessage("Sale Price is required.")
            //    .GreaterThan(0).WithMessage("Sale Price must be greater than 0.");

            //RuleFor(x => x.Entity.SizeId)
            //    .NotEmpty().WithMessage("Size is required.")
            //    .MaximumLength(20).WithMessage("Size must not exceed 20 characters.");

            RuleFor(x => x.Entity.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Entity.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .MaximumLength(20).WithMessage("SKU must not exceed 20 characters.");
        }
    }
}

//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.BrandCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.WishlistHandler
//{
//    public class CreateWishlistCommandValidator : AbstractValidator<CreateWishlistCommand>
//    {
//        public CreateWishlistCommandValidator()
//        {
//            RuleFor(x => x.Wishlist.CustomerId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
//        }
//    }
//}

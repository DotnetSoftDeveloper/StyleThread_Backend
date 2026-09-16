//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.BrandCommand;
//using static Application.Features.Command.ReviewCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.ReviewHandler
//{
//    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
//    {
//        public CreateReviewCommandValidator()
//        {
//            RuleFor(x => x.Review.CustomerId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
//        }
//    }
//}

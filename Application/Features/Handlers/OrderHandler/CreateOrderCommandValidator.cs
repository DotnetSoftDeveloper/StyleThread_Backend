//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.BrandCommand;
//using static Application.Features.Command.OrderCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.OrderHandler
//{
//    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
//    {
//        public CreateOrderCommandValidator()
//        {
//            RuleFor(x => x.Order.CustomerId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
//        }
//    }
//}

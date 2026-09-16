//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.BrandCommand;
//using static Application.Features.Command.OrderItemCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.OrderItemHandler
//{
//    public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
//    {
//        public CreateOrderItemCommandValidator()
//        {
//            RuleFor(x => x.OrderItem.OrderId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
//        }
//    }
//}

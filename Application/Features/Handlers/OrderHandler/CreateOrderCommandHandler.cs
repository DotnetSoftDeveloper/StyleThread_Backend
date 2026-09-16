//using Application.Interfaces;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.OrderCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.OrderHandler
//{
//    internal class CreateCartCommandHandler(IApplicationDbcontext dbcontext, IMapper mapper) : IRequestHandler<CreateOrderCommand, int>
//    {

//        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
//        {
//            var order = mapper.Map<Domain.Entites.Order>(request);

//            await dbcontext.Order.AddAsync(order);
//            await dbcontext.SaveChangesAsync();
//            return order.OrderId;
//        }
//    }
//}

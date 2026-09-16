//using Application.Interfaces;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.OrderItemCommand;

//namespace Application.Features.Handlers.OrderItemHandler
//{
//    internal class CreateOrderItemCommandHandler(IApplicationDbcontext dbcontext, IMapper mapper) : IRequestHandler<CreateOrderItemCommand, int>
//    {

//        public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
//        {
//            var orderItem = mapper.Map<Domain.Entites.OrderItem>(request);

//            await dbcontext.OrderItem.AddAsync(orderItem);
//            await dbcontext.SaveChangesAsync();
//            return orderItem.OrderItemId;
//        }
//    }
//}

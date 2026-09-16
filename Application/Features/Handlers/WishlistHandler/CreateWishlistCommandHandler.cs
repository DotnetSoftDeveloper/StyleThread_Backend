//using Application.Interfaces;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.WishlistHandler
//{
//    internal class CreateWishlistCommandHandler(IApplicationDbcontext dbcontext, IMapper mapper) : IRequestHandler<CreateWishlistCommand, int>
//    {

//        public async Task<int> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
//        {
//            var wishlist = mapper.Map<Domain.Entites.Wishlist>(request);

//            await dbcontext.Wishlist.AddAsync(wishlist);
//            await dbcontext.SaveChangesAsync();
//            return wishlist.WishlistId;
//        }
//    }
//}

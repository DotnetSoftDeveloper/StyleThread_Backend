//using Application.Interfaces;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.ReviewCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.ReviewHandler
//{
//    internal class CreateReviewCommandHandler(IApplicationDbcontext dbcontext, IMapper mapper) : IRequestHandler<CreateReviewCommand, int>
//    {

//        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
//        {
//            var review = mapper.Map<Domain.Entites.Review>(request);

//            await dbcontext.Review.AddAsync(review);
//            await dbcontext.SaveChangesAsync();
//            return review.ReviewId;
//        }
//    }
//}

//using Application.Interfaces.IRepository;
//using Domain.Entities;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Features.Handlers.AuthHandler
//{
//    public record UpdateUserCommand(Customer UpdatedUser) : IRequest<string>;

//    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, string>
//    {
//        private readonly IUserService _userService;

//        public UpdateUserHandler(IUserService userService)
//        {
//            _userService = userService;
//        }

//        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
//        {
//            return await _userService.UpdateUser(request.UpdatedUser);
//        }
//    }

//}

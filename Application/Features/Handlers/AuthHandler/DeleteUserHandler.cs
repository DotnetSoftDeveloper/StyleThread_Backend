using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.AuthHandler
{
    public record DeleteUserCommand(string UserId) : IRequest<string>;

    internal class DeleteUserHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUser(request.UserId);
        }
    }
    public record UpdateUserCommand(Customer UpdatedUser) : IRequest<string>;

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request.UpdatedUser);
        }
    }
}

using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public class LoginHandler(IUserService _userService) : IRequestHandler<GenericCreateCommand<LoginRequestDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericCreateCommand<LoginRequestDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
                return new GenericResponse<string> { Success = false, Message = "Login request cannot be null." };
            try
            {
                var data = await _userService.Login(request.Entity);
                return data;
            }
            catch (Exception ex)
            {
                return new GenericResponse<string> { Success = false, Message = "Something went wrong while processing your request. Please try again later." };
            }
        }
    }
}

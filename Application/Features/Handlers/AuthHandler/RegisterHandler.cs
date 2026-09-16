using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    internal class RegisterHandler(IUserService _userManager, IMapper _mapper) : IRequestHandler<GenericCreateCommand<CustomerDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericCreateCommand<CustomerDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                Customer registerRequest = _mapper.Map<Customer>(request.Entity);
                IdentityResult result = await _userManager.Register(registerRequest, request.Entity.Password);

                if (result.Succeeded)
                {
                    return new GenericResponse<string>
                    {
                        Content = null,
                        Success = true,
                        Message = "User registered successfully."
                    };
                }

                // Extract error messages from IdentityResult
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));

                return new GenericResponse<string>
                {
                    Content = "Error",
                    Success = false,
                    Message = errors
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
                {
                    Content = "Error",
                    Success = false,
                    Message = "Something went wrong while processing your request. Please try again later."
                };
            }
        }
    }
}

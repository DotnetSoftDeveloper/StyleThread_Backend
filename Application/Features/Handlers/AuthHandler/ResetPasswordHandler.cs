using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<GenericResponse<string>>;

    internal class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;

        public ResetPasswordHandler(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GenericResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            return result.Succeeded
                ? new GenericResponse<string> { Success = true, Message = "Password reset successful." }
                : new GenericResponse<string> { Success = false, Message = "Password reset failed." };
        }
    }

}

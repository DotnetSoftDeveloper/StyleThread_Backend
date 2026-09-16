using Application.Interfaces;
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
    public record ConfirmEmailCommand(string Email, string Otp) : IRequest<GenericResponse<string>>;

    internal class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, GenericResponse<string>>
    {
        private readonly IUserService _userService;

        public ConfirmEmailHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GenericResponse<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ConfirmOtpAsync(request, cancellationToken);
        }
    }

}

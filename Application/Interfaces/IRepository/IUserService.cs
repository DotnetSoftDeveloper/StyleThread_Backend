using Application.DTO;
using Application.Features.Handlers.AuthHandler;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Interfaces.IRepository
{
    public interface IUserService
    {
        Task<IdentityResult> Register(Customer registerRequest, string password);
        Task<GenericResponse<string>> Login(LoginRequestDto loginRequest);
        Task<string> DeleteUser(string userId);
        Task<string> UpdateUser(Customer updateUserRequest);
        Task<GenericResponse<string>> SendConfirmationEmailAsync(Customer user, string appBaseUrl);
        Task<GenericResponse<string>> ConfirmEmailAsync(int userId, string encodedToken);

        Task<GenericResponse<string>> SendPasswordResetEmailAsync(string email, string appBaseUrl);
        Task<GenericResponse<string>> ResetPasswordAsync(int userId, string encodedToken, string newPassword);
        Task<GenericResponse<string>> ConfirmOtpAsync(ConfirmEmailCommand request, CancellationToken cancellationToken);

    }
}

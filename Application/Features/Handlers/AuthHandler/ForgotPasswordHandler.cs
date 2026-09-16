using Application.Interfaces.IEmailService;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public record ForgotPasswordCommand(string Email) : IRequest<GenericResponse<string>>;

    internal class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(UserManager<Customer> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<GenericResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            // Generate reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Encode the token so it’s safe for URLs
            var encodedToken = System.Web.HttpUtility.UrlEncode(token);

            // Build the reset link (frontend URL)
            var resetLink = $"http://localhost:5176/reset-password?email={user.Email}&token={encodedToken}";

            // Send email
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Message = "User does not have a valid email."
                };
            }
            await _emailService.SendResetPasswordEmail(user.Email , resetLink);

            return new GenericResponse<string>
            {
                Success = true,
                Message = "Password reset email sent successfully."
            };
        }
    }
}

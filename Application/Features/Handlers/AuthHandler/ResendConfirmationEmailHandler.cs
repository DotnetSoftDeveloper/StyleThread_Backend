using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public record ResendConfirmationEmailCommand(string Email) : IRequest<GenericResponse<string>>;

    internal class ResendConfirmationEmailHandler : IRequestHandler<ResendConfirmationEmailCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ResendConfirmationEmailHandler(
            UserManager<Customer> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            if (user.EmailConfirmed)
                return new GenericResponse<string> { Success = false, Message = "Email already confirmed." };

            // Generate new OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Save OTP (valid for 5 mins)
            await _unitOfWork.emailOtpRepository.SaveOtpAsync(user.Id, otp);

            // Send email
            await _unitOfWork.emailOtpRepository.SendEmailAsync(user.Email, "Your OTP Code", $"Your new OTP is: {otp}");

            // Send SMS (optional)
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                await _unitOfWork.emailOtpRepository.SendSmsAsync(user.PhoneNumber, $"Your OTP is: {otp}");
            }

            return new GenericResponse<string> { Success = true, Message = "OTP resent to your email and phone number." };
        }
    }
}

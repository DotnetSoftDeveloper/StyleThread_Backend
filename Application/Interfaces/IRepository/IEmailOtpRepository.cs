using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IEmailOtpRepository
    {
        Task SaveOtpAsync(int userId, string otp);
        Task<EmailOtp?> GetLatestValidOtpAsync(int userId, string otp, CancellationToken cancellationToken);
        Task SendSmsAsync(string phoneNumber, string message);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}

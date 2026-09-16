using Application.Interfaces.IRepository;
using Domain.Entities;
using MimeKit;
using InfraStructure.Context;
using MailKit.Net.Smtp;

namespace InfraStructure.Repository
{
    internal class EmailOtpRepository : IEmailOtpRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailOtpRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EmailOtp?> GetLatestValidOtpAsync(int userId, string otp, CancellationToken cancellationToken)
        {
            var expiryTime = DateTime.UtcNow.AddMinutes(-5);

            return _context.EmailOtps
                        .Where(e => e.UserId == userId && e.Otp == otp && e.CreatedAt >= expiryTime)
                        .OrderByDescending(e => e.CreatedAt)
                        .FirstOrDefault();
        }

        public async Task SaveOtpAsync(int userId, string otp)
        {
            var emailOtp = new EmailOtp
            {
                UserId = userId,
                Otp = otp,
                CreatedAt = DateTime.UtcNow
            };

            _context.EmailOtps.Add(emailOtp);
            await _context.SaveChangesAsync();
        }
        public async Task SendEmailAsync(string toEmail, string subject, string otp)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("groupcs69@gmail.com"));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                var htmlBody = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f9f9f9;
                                padding: 20px;
                                color: #333;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: #fff;
                                border-radius: 8px;
                                padding: 20px;
                                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                            }}
                            .otp {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #2c3e50;
                                margin: 20px 0;
                            }}
                            .footer {{
                                margin-top: 30px;
                                font-size: 12px;
                                color: #888;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Email Verification Code</h2>
                            <p>Dear User,</p>
                            <p>Thank you for registering. Please use the OTP below to verify your email address. This OTP is valid for <strong>5 minutes</strong>.</p>
                            <div class='otp'>{otp}</div>
                            <p>If you did not request this, please ignore this email.</p>
                            <div class='footer'>© {DateTime.UtcNow.Year} StyleThread. All rights reserved.</div>
                        </div>
                    </body>
                </html>";

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlBody,
                    TextBody = $"Your OTP code is: {otp}. It is valid for 5 minutes."
                };

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("groupcs69@gmail.com", "qxnn xkzt hvnc igrr"); // Use your Gmail App Password
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Optional: log the error or rethrow with context
                throw new Exception("Failed to send OTP email", ex);
            }
        }
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            Console.WriteLine($"SMS sent to {phoneNumber}: {message}");
            return Task.CompletedTask;
        }
    }
}

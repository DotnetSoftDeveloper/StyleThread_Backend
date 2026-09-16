using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.IEmailService;

namespace Application.Features.Service
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendResetPasswordEmail(string toEmail, string resetLink)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"] ?? string.Empty);
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];
                var fromEmail = _configuration["EmailSettings:From"];

                var body = $@"
        <html>
        <head>
          <style>
            .container {{
              font-family: Arial, sans-serif;
              max-width: 600px;
              margin: auto;
              border: 1px solid #e0e0e0;
              padding: 20px;
              background-color: #fafafa;
              border-radius: 8px;
            }}
            .header {{
              text-align: center;
              padding-bottom: 10px;
            }}
            .btn {{
              display: inline-block;
              padding: 12px 20px;
              margin-top: 20px;
              font-size: 16px;
              font-weight: bold;
              color: #ffffff;
              background-color: #007bff;
              text-decoration: none;
              border-radius: 5px;
            }}
            .footer {{
              font-size: 12px;
              color: #777;
              margin-top: 30px;
              text-align: center;
            }}
          </style>
        </head>
        <body>
          <div class='container'>
            <div class='header'>
              <h2>Password Reset Request</h2>
            </div>
            <p>Hi there,</p>
            <p>We received a request to reset the password for your account. 
               Click the button below to set a new password:</p>
            
            <div style='text-align:center;'>
              <a href='{resetLink}' class='btn'>Reset Password</a>
            </div>

            <div class='footer'>
              <p>© {DateTime.UtcNow.Year} Style Thread. All rights reserved.</p>
            </div>
          </div>
        </body>
        </html>";

                var mail = new MailMessage
                {
                    From = new MailAddress(!string.IsNullOrEmpty(fromEmail) ? fromEmail : "stylethread@gmail.com", "stylethread@gmail.com"),
                    Subject = "Reset Your Password",
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(toEmail);

                using (var smtp = new SmtpClient(smtpServer, port))
                {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

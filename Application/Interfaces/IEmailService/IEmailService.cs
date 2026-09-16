using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IEmailService
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string toEmail, string resetLink);
    }

}

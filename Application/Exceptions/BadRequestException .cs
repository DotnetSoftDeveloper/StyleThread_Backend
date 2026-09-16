using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message, string? errorCode = "BAD_REQUEST", Exception? innerException = null)
            : base(message, StatusCodes.Status400BadRequest, errorCode, innerException)
        {
        }
    }
}
    
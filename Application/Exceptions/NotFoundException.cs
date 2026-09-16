using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string? errorCode = "NOT_FOUND", Exception? innerException = null)
            : base(message, StatusCodes.Status404NotFound, errorCode, innerException)
        {
        }
    }
}

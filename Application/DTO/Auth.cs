using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class Auth
    {
        [Index(nameof(Email), IsUnique = true)]
        public class LoginRequestDto
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
        public class CustomerDto
        {
            
            public int CustomerId { get; set; }
            public required string UserName { get; set; }
            public required string FirstName { get; set; }
            public string? LastName { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public required string Address { get; set; }
            public required string PhoneNumber { get; set; }
        }

        public class CustomerAddressDto
        {
            public int CustomerAddressId { get; set; }
            public int CustomerId { get; set; }
            public required string FullName { get; set; }
            public required string Phone { get; set; }
            public required string Pincode { get; set; }
            public required string Locality { get; set; }
            public required string AddressLine1 { get; set; }
            public required string City { get; set; }
            public required string State { get; set; }
            public string? Landmark { get; set; }
            public string? AlternatePhone { get; set; }
            public required string LocationTypeTag { get; set; }
            public bool IsDefault { get; set; }
        }

        public class GenericResponse<TResponse>
        {
            public string? Message { get; set; }
            public string? Error { get; set; }
            public int? ErrorCode { get; set; }
            public TResponse? Content { get; set; } 
            public bool Success { get; set; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmailOtp
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Otp { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Customer User { get; set; }
    }
}

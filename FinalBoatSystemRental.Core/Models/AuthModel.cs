using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Core.Models
{
    public class AuthModel
    {
        public string Message { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime ExpiresOn { get; set; }
     
    }
}

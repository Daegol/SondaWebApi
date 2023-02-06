using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Authentication
{
    public class AuthenticationDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}

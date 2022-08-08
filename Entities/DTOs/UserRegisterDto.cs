using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserRegisterDto:IDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Username { get; set; }
        public string? Country { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}

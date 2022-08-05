using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public int UserRole { get; set; } 

    }
}

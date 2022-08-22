using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserLikesDto:IDto
    {
        public Product Product { get; set; }
        public Like Like { get; set; }

    }
}

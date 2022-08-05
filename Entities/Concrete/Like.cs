using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Like:IEntity
    {
        public int LikeId { get; set; }
        public int LikeCount { get; set; }
    }
}

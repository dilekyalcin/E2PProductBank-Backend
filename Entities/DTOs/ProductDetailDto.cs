using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductVendor { get; set; }  
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public Like Like { get; set; }

    }
}

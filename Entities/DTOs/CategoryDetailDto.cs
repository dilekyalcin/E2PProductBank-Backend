using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CategoryDetailDto
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }  
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductVendor { get; set; }
        //public byte[] ProductImage { get; set; }
        public string ProductDescription { get; set; }
    }
}

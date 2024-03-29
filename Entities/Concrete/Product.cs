﻿using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductVendor { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        [NotMapped]
        public IFormFile ProductImageFile { get; set; }

        public int LikeCount { get; set; } = 0;
       
        //public virtual Category Category { get; set; }


    }
}

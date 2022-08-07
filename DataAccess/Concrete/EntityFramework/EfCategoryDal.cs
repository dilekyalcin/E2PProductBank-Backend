using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, E2PContext>, ICategoryDal
    {
        public List<CategoryDetailDto> GetCategoryDetail(int categoryId)
        {
            using (E2PContext context = new E2PContext())
            {
                 var result = from c in context.Categories
                              join p in context.Products on categoryId equals p.CategoryId
                              where c.Id == categoryId
                              select new CategoryDetailDto { CategoryId = categoryId, ProductId = p.Id, ProductName = p.ProductName, ProductVendor = p.ProductVendor, ProductDescription = p.ProductDescription, CategoryName = c.CategoryName};
                return result.ToList();
                
            }
        }
    }
}

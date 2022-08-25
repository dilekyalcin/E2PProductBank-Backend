using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetail(int productId);

        List<Product> GetProductsFromCategoryId(int categoryId);

        List<Product> GetProducts();

        bool DeleteProduct(int productId);

        bool UpdateProduct(Product request);

        Product GetProduct(int productId);

    }
}

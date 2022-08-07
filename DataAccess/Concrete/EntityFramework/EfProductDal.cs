using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, E2PContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetail(int productId)
        {
            using (E2PContext context = new E2PContext())
            {
                var result = from p in context.Products
                             join d in context.Comments on productId equals d.ProductId
                             join u in context.Users on d.UserId equals u.Id
                             select new ProductDetailDto { ProductId = productId, ProductName=p.ProductName, CommentText= d.CommentText, Username = u.Username };

                return result.ToList();
            }
        }
    }
}

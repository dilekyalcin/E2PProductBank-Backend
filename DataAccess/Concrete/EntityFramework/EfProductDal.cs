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
            //ProductDetailDto pDetail = new ProductDetailDto();

            using (E2PContext context = new E2PContext())
            {
                IQueryable<ProductDetailDto> pDetail = from p in context.Products
                              join d in context.Comments on productId equals d.ProductId
                              join l in context.Likes on productId equals l.ProductId
                              where p.Id == productId
                              select new ProductDetailDto { 
                                  ProductId = productId, 
                                  ProductName = p.ProductName, 
                                  ProductVendor = p.ProductVendor, 
                                  ProductDescription = p.ProductDescription, 
                                  CommentText = d.CommentText,
                                  UserId = d.UserId,
                                  Like = l
                              };


                return pDetail.ToList();
            }
        }

        

        public List<Product> GetProductsFromCategoryId(int categoryId)
        {
            using (E2PContext context = new E2PContext())
            {
                var result = context.Products.Where(p => p.Id == categoryId).ToList();
                return result;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (E2PContext context = new E2PContext())
            {
                var product = context.Products.Where(p => p.Id == productId).FirstOrDefault();
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public List<Product> GetProducts()
        {
            using (E2PContext context = new E2PContext())
            {
                var result = context.Products.Select(p => new Product()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductVendor = p.ProductVendor,
                    ProductDescription = p.ProductDescription,
                    CategoryId = p.CategoryId,
                    ProductImage = p.ProductImage,
                });
                return result.ToList();
            }
        }

        public bool UpdateProduct(Product request)
        {
            using (E2PContext context = new E2PContext())
            {
                try
                {
                    var getProduct = context.Products.Where(p => p.Id == request.Id).SingleOrDefault();

                    getProduct.ProductName = request.ProductName;
                    getProduct.ProductVendor = request.ProductVendor;
                    getProduct.ProductDescription = request.ProductDescription;
                    getProduct.ProductImage = request.ProductImageFile.FileName;
                    getProduct.CategoryId = request.CategoryId;
                    context.Products.Update(getProduct);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Product GetProduct(int productId)
        {
            using (E2PContext context = new E2PContext())
            {
                return context.Products.Find(productId);
            }
        }
    }
}

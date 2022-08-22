using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLikeDal : EfEntityRepositoryBase<Like, E2PContext>, ILikeDal
    {
        public List<Like> LikeProduct(Like like)
        {
            using (E2PContext context = new E2PContext())
            {
                Product product = context.Products.Where(p => p.Id == like.ProductId).SingleOrDefault();
                
                product.LikeCount = product.LikeCount + 1;
                var res = new Like
                {
                    ProductId = like.ProductId,
                    UserId = like.UserId,
                };
                context.Likes.Add(res);
                context.SaveChanges();
                return context.Likes.ToList();
            }
        }
    }
}

using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
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
        public IDataResult<List<Like>> LikeProduct(Like like)
        {
            using (E2PContext context = new E2PContext())
            {
                try
                {
                    Product product = context.Products.Where(p => p.Id == like.ProductId).SingleOrDefault();
                    
                    product.LikeCount = product.LikeCount + 1;
                    var res = new Like
                    {
                        ProductId = like.ProductId,
                        UserId = like.UserId,
                        Status = true,
                    };
                    context.Likes.Add(res);
                    context.SaveChanges();
                    return new SuccessDataResult<List<Like>>(context.Likes.ToList());
                }
                catch (Exception)
                {
                    return new ErrorDataResult<List<Like>>("Aynı ürünü birden fazla beğenemezsin!");
                }
            }
        }

        public IDataResult<List<Like>> UnlikeProduct(Like like)
        {
            using (E2PContext context = new E2PContext())
            {
                try
                {
                    Product product = context.Products.Where(p => p.Id == like.ProductId).SingleOrDefault();
                    Like getLike = context.Likes.Where(l => l.ProductId == like.ProductId).SingleOrDefault();
                    User getUser = context.Users.Where(u => u.Id == like.UserId).SingleOrDefault();
                    
                    product.LikeCount = product.LikeCount - 1;
                    getLike.ProductId = like.ProductId;
                    getLike.UserId  = like.UserId;
                    getLike.Status = false;
                    context.Likes.Update(getLike);
                    context.SaveChanges();
                    return new SuccessDataResult<List<Like>>(context.Likes.ToList());
                }
                catch (Exception)
                {
                    return new ErrorDataResult<List<Like>>("Ürün beğeni geri alma da hata var!");
                }
            }
        }
    }
}

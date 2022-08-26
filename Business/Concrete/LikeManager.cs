using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LikeManager : ILikeService
    {
        ILikeDal _likeDal;

        public LikeManager(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public IDataResult<List<Like>> GetLikes()
        {
            return new SuccessDataResult<List<Like>>(_likeDal.GetAll(), "Beğeniler getirildi.");
        }

        public IDataResult<List<Like>> GetLikesProduct(int productId)
        {
            return new SuccessDataResult<List<Like>>(_likeDal.GetAll(l => l.ProductId == productId),"Ürünün beğenileri getirildi.");
        }

        public IDataResult<List<Like>> LikeProduct(Like like)
        {
            return _likeDal.LikeProduct(like);
            //return new SuccessDataResult<List<Like>>(_likeDal.LikeProduct(like),"Ürün beğenildi");
        }

        public IDataResult<List<Like>> UnlikeProduct(Like like)
        {
            return _likeDal.UnlikeProduct(like);
        }
    }
}
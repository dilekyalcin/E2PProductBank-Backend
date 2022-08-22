using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILikeService
    {
        IDataResult<List<Like>> GetLikes();

        IDataResult<List<Like>> GetLikesProduct(int productId);

        IDataResult<List<Like>> LikeProduct(Like like);
    }
}

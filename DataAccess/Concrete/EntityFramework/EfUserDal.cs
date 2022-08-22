using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, E2PContext>, IUserDal
    {
        public List<UserCommentsDto> GetCommentsUser(int userId)
        {
            using (E2PContext context = new E2PContext())
            {
                var result = from u in context.Users
                             join c in context.Comments on u.Id equals c.UserId
                             join p in context.Products on c.ProductId equals p.Id
                             where u.Id == userId
                             select new UserCommentsDto { Product = c.Product, Comment = c.CommentText };
                return result.ToList();
                             
            }
        }

        public List<UserLikesDto> GetLikesUser(int userId)
        {
            using (E2PContext context = new E2PContext())
            {
                var result = from u in context.Users
                             join l in context.Likes on u.Id equals l.UserId
                             join p in context.Products on l.ProductId equals p.Id
                             where u.Id == userId
                             select new UserLikesDto { Product = p, Like = l };
                return result.ToList();
            }
        }
    }
}

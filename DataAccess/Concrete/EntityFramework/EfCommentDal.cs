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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, E2PContext>, ICommentDal
    {

        public void AddComment(int productId, int userId, Comment request)
        {
            using (E2PContext context = new E2PContext())
            {
                var comment = new Comment
                {
                    ProductId = productId,
                    UserId = userId,
                    CommentText = request.CommentText
                };
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public List<Comment> GetComments()
        {
            using (E2PContext context = new E2PContext())
            {
                var result = from c in context.Comments
                             join p in context.Products on c.ProductId equals p.Id
                             select c;

                return result.ToList();
                             
            }
        }
    }
}

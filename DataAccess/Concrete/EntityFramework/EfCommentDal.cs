using Core.DataAccess.EntityFramework;
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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, E2PContext>, ICommentDal
    {

        public IResult AddComment(Comment request)
        {
            using (E2PContext context = new E2PContext())
            {
                try
                {


                    var comment = new Comment
                    {
                        ProductId = request.ProductId,
                        UserId = request.UserId,
                        CommentText = request.CommentText
                    };
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    return new SuccessResult("Yorum Eklendi.");
                }
                catch (Exception)
                {
                    return new ErrorResult("Aynı ürüne birden fazla yorum ekleyemezsiniz!");
                }
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

using Business.Abstract;
using Business.Constants;
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
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IResult Add(Comment comment)
        {
                _commentDal.Add(comment);
                return new SuccessResult(Messages.CommentAdded);
         
            return new ErrorResult();
        }

        public IResult AddComment(int productId, int userId, Comment comment)
        {
            _commentDal.AddComment(productId, userId, comment);
            return new SuccessResult( Messages.CommentAdded);
        }

        public IDataResult<List<Comment>> GetAll()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetComments(), Messages.CommentsListed);
        }

        public IDataResult<List<Comment>> GetByProductId(int productId)
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(p => p.ProductId == productId), Messages.CommentsListed);
        }
    }
}

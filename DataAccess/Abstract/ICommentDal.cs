using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICommentDal: IEntityRepository<Comment>
    {
        IResult AddComment(Comment comment);
        List<Comment> GetComments();
    }
}

﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICommentDal
    {
        void AddComment(int productId, int userId, Comment comment);
        List<Comment> GetComments();
    }
}

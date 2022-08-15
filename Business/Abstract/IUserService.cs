using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<User> Get(int id);
        User GetByMail(string email);
        User GetById(int userId);
        
        IDataResult<List<UserCommentsDto>> GetCommentsUser(int userId);
    }
}

using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User user)
        {
            ValidationTool.Validate(new UserValidator(), user);

            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IDataResult<User> Get(int id)
        {
            _userDal.GetAll(u => u.Id == id);
            return new SuccessDataResult<User>(Messages.UserGetById);
        }

        public User GetById(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return result;
        }

        public User GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            return result;
        }

        public IDataResult<List<UserCommentsDto>> GetCommentsUser(int userId)
        {
            return new SuccessDataResult<List<UserCommentsDto>>(_userDal.GetCommentsUser(userId), "Yorumlar getirildi.");

        }

        public IDataResult<List<UserLikesDto>> GetLikesUser(int userId)
        {
            return new SuccessDataResult<List<UserLikesDto>>(_userDal.GetLikesUser(userId), "Yorumlar getirildi.");
        }
    }
}

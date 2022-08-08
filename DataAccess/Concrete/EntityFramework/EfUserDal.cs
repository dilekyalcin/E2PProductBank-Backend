using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, E2PContext>, IUserDal
    {
        public User GetUserByMail(string email)
        {
            using (E2PContext context = new E2PContext())
            {
                var result = context.Users.FirstOrDefault(x => x.Email == email);
                return result;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Model;
using TuyenDung.Service.Repository.Interface;

namespace TuyenDung.Service.Repository.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly MyDb _dbContext;
        public UserRepository(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public AccessToken GetValidTokenByUserId(int userId)
        {
            return _dbContext.AccessTokens.FirstOrDefault(x => x.Id == userId);
        }

        public User GetId(int id)
        {
            throw new NotImplementedException();
        }
    }
}

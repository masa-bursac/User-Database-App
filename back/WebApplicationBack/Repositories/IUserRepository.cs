using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.Model;

namespace WebApplicationBack.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAll();
        User FindByUsernameAndPassword(string email, string password);
        User FindById(int userId);
        bool Update(User user);
    }
}

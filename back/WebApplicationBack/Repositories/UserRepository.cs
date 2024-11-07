using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.Model;

namespace WebApplicationBack.Repositories
{
    public class UserRepository : IUserRepository
    {
        public MyDbContext dbContext { get; set; }

        public UserRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserRepository() { }

        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }
    }
}


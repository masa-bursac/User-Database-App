using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.Repositories;

namespace WebApplicationBack.Services
{
    public class UserService
    {
        private IUserRepository UserRepository { get; }
        private UserRepository UserSqlRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
             UserRepository = userRepository;
        }
        public UserService()
        {
            UserRepository = new UserRepository();
        }
        public UserService(UserRepository userSqlRepository)
        {
            UserSqlRepository = userSqlRepository;
        }
       
    }

}


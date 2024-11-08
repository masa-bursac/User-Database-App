using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.Model;
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

        public void SaveUser(User user, MyDbContext dbContext)
        {
            user.UserType = UserType.user;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            user.DateOfBirth = TimeZoneInfo.ConvertTime(user.DateOfBirth, timeZone);
            user.Password = new Microsoft.AspNetCore.Identity.PasswordHasher<object>().HashPassword(null, user.Password);
            UserSqlRepository.saveUser(user);
        }
    }

}


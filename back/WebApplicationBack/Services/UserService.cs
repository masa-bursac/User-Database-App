using Microsoft.AspNetCore.Identity;
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
            Console.WriteLine(user.Password);
            user.Password = new PasswordHasher<object>().HashPassword(null, user.Password);
            Console.WriteLine(user.Password);
            UserSqlRepository.saveUser(user);
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            return UserSqlRepository.FindByUsernameAndPassword(email, password);
        }
    }

}


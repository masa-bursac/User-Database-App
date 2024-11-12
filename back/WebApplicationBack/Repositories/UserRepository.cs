using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.DTO;
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
            return dbContext.Users.Where(user => !user.IsDeleted).ToList();
        }

            public void saveUser(User user)
        {
            int id = dbContext.Users.ToList().Count(); //iako je stavljeno da se sam inkrementira to ne radi, pa je dodato manuelno
            user.Id = id+1;
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public User FindByUsernameAndPassword(string email, string password)
        {
            var passwordHasher = new PasswordHasher<object>();

            User user = dbContext.Users.FirstOrDefault(n => n.Email == email);

            if (user != null)
            {
                var result = passwordHasher.VerifyHashedPassword(null, user.Password, password);

                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public User FindById(int userId)
        {
            User u = dbContext.Users.Find(userId);
            return u;
        }

        public bool Update(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            return true;
        }

        public List<User> SearchUsers(SearchDto searchDto)
        {
            List<User> users = GetAll();
            List<User> returnUsers = new List<User>();

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            searchDto.StartDate = TimeZoneInfo.ConvertTime(searchDto.StartDate, timeZone);
            searchDto.EndDate = TimeZoneInfo.ConvertTime(searchDto.EndDate, timeZone);

            if (searchDto.Email == null)
                searchDto.Email = "";

            foreach (User u in users){
                if (u.Email.Contains(searchDto.Email) && u.DateOfBirth >= searchDto.StartDate && u.DateOfBirth <= searchDto.EndDate)
                {
                    returnUsers.Add(u);
                }
            }
            return returnUsers;
        }
    }
}


using Microsoft.AspNetCore.Identity;
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
                Console.WriteLine(password);
                Console.WriteLine(user.Password);
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
    }
}


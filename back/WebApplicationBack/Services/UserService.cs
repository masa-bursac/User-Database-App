using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationBack.Model;
using WebApplicationBack.Repositories;

namespace WebApplicationBack.Services
{
    public class UserService
    {
        private IUserRepository UserRepository { get; }
        private UserRepository UserSqlRepository { get; set; }
        private string Role { get; set; }

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

        public String GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            if(user.UserType == UserType.user)
            {
                Role = "user";
            }
            else
            {
                Role = "admin";
            }

            IdentityOptions options = new IdentityOptions();
            SecurityTokenDescriptor tokenDeskriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim("isDeleted", user.IsDeleted.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QKcOa8xPopVOliV6tpvuWmoKn4MOydSeIzUt4W4r1UlU2De7dTUYMlrgv3rU")), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDeskriptor);
            return tokenHandler.WriteToken(token);

        }

        public Boolean UpdateUser(User user, DTO.UserDto userDto)
        {
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.DateOfBirth = userDto.DateOfBirth;
            user.Image = Encoding.ASCII.GetBytes(userDto.Image);

            Boolean check = ComparePasswordInDatabase(user.Password, userDto.CheckPassword);

            if (check)
            {
                user.Password = new PasswordHasher<object>().HashPassword(null, userDto.NewPassword);
                
                return UserSqlRepository.Update(user);
            }

            return false;
        }

        public Boolean ComparePasswordInDatabase(String databasePassword, string checkPassword)
        {
            var passwordHasher = new PasswordHasher<object>();

  
            var result = passwordHasher.VerifyHashedPassword(null, databasePassword, checkPassword);

                if (result == PasswordVerificationResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public User FindUserById(int id)
        {
            return UserSqlRepository.FindById(id);
        }

        public Boolean DeleteUser(int userId)
        {
            User user = UserSqlRepository.FindById(userId);
            user.IsDeleted = true;
            Boolean done = UserSqlRepository.Update(user);
            return done;
        }
    }

}


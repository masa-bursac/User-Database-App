using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBack.Model;

namespace WebApplicationBack.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserType UserType { get; set; }
        public String Image { get; set; }
        public Boolean IsDeleted { get; set; }

        public UserDto() { }
        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
            Name = user.Name;
            Surname = user.Surname;
            DateOfBirth = user.DateOfBirth;
            UserType = user.UserType;
            Image = Convert.ToBase64String(user.Image);
            IsDeleted = user.IsDeleted;
        }
    }
}

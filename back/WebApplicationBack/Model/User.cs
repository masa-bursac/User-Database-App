using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationBack.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserType UserType { get; set; }
        public String Token { get; set; }
        public byte[] Image { get; set; }

        public User(int id, string email, string password, string name, string surname, DateTime dateOfBirth, UserType userType, string token, string image)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            UserType = userType;
            Token = token;
            Image = Encoding.ASCII.GetBytes(image);
        }

        public User(int id, string email, string password, string name, string surname, DateTime dateOfBirth, UserType userType)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            UserType = userType;
            Image = new byte[0]; 
        }
    }
}

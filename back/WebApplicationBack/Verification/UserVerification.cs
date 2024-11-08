using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplicationBack.Model;

namespace WebApplicationBack.Verification
{
    public class UserVerification
    {
        private User user;

        public UserVerification() { }

        private bool VerifyName()
        {
            Regex regex = new Regex("[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*");
            if (user.Name.Equals(""))
                return false;
            if (!regex.IsMatch(user.Name))
                return false;
            return true;
        }

        private bool VerifySurname()
        {
            Regex regex = new Regex("[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*");
            if (user.Surname.Equals(""))
                return false;
            if (!regex.IsMatch(user.Surname))
                return false;
            return true;
        }

        private bool VerifyPassword()
        {
            Console.WriteLine(user.Password);
            if (user.Password.Equals(""))
                return false;
            return true;
        }

        private bool VerifyEmail()
        {
            Regex regex = new Regex("^(.+)@(.+)$");
            if (user.Email.Equals(""))
                return false;
            if (!regex.IsMatch(user.Email))
                return false;
            return true;
        }

        private bool VerifyDate()
        {
            if (user.DateOfBirth == null)
                return false;
            return true;
        }

        public bool Verify(User user)
        {
            this.user = user;
            if (user == null)
                return false;
            else if (!VerifyName())
                return false;
            else if (!VerifySurname())
                return false;
            else if (!VerifyEmail())
                return false;
            else if (!VerifyDate())
                return false;
            else if (!VerifyPassword())
                return false;
            return true;
        }
    }
}

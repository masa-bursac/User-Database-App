using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplicationBack.DTO;

namespace WebApplicationBack.Verification
{
    public class UpdateVerification
    {
        private UserDto userDto; 

        public UpdateVerification() { }

        private bool VerifyName()
        {
            Regex regex = new Regex("[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*");
            if (userDto.Name.Equals(""))
                return false;
            if (!regex.IsMatch(userDto.Name))
                return false;
            return true;
        }

        private bool VerifySurname()
        {
            Regex regex = new Regex("[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*");
            if (userDto.Surname.Equals(""))
                return false;
            if (!regex.IsMatch(userDto.Surname))
                return false;
            return true;
        }

        private bool VerifyDate()
        {
            if (userDto.DateOfBirth == null)
                return false;
            return true;
        }

        public bool VerifyUpdate(UserDto userDto)
        {
            this.userDto = userDto;
            if (userDto == null)
                return false;
            else if (!VerifyName())
                return false;
            else if (!VerifySurname())
                return false;
            else if (!VerifyDate())
                return false;
            return true;
        }
    }
}

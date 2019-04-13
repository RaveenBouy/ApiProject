using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataLibrary.BusinessLogic
{
    public static class Validator
    {
        public static int ValidateUser(string username, string password) // 7 = Validated 2 = invalid username 3 = invalid password
        {
            return ValidateUsername(username) ? ValidatePassword(password) ? 7 : 3 : 2;
        }

        public static int ValidateUserRegistration(UserModel userModel)
        {
            return ValidateUsername(userModel.UserName) ? ValidatePassword(userModel.Pass) ? ValidateEmailAddress(userModel.Email) ? ValidateUserType(userModel.UserType) ? 2 : 7 : 5 : 6 : 4;
        }

        private static bool ValidateUserType(int userType)
        {
            var regFormat = @"[1-2]$";

            try
            {
                return Regex.IsMatch(userType.ToString(), regFormat) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidateEmailAddress(string email)
        {
            var regFormat = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                            + "@"
                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            try
            {
                return Regex.IsMatch(email, regFormat) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidateUsername(string username)
        {
            var regFormat = @"^[a-zA-Z0-9-_%£$@\\/]*$";

            try
            {
                return Regex.IsMatch(username, regFormat) ? true : false; ///^[a-zA-Z0-9]+([_-]?[a-zA-Z0-9])*$/
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidatePassword(string password)
        {
            var regFormat = @"^[a-zA-Z0-9-_%£$@/\\]*$";

            try
            {
                return Regex.IsMatch(password, regFormat) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

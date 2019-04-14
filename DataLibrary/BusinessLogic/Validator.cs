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

        public static int ValidateSetLibraryItem(ItemModel itemModel)
        {
            return !string.IsNullOrEmpty(itemModel.AuthToken) ? !string.IsNullOrEmpty(itemModel.Title) ? !string.IsNullOrEmpty(itemModel.Description) ? !string.IsNullOrEmpty(itemModel.Author) ? ValidatePublishYear(itemModel.PublishYear) ? ValidateAccess(itemModel.Access) ? 2 : 9 : 8 : 7 : 6 : 5 : 4;
        }

        public static int ValidateUpdateLibraryItem(string token, int id, string type, string value)
        {
            return !string.IsNullOrEmpty(token) ? id >= 0 ? ValidateType(type) ? ValidateValue(type, value) ? 2 : 7 : 6 : 5 : 4;
        }

        public static int ValidateDeleteLibraryItem(string token, int id)
        {
            return !string.IsNullOrEmpty(token) ? id >= 0 ? 2 : 5 : 4;
        }

        private static bool ValidateType(string type)
        {
            try
            {
                if (type.ToLower().Contains("title") || type.ToLower().Contains("description") || type.ToLower().Contains("author") || type.ToLower().Contains("publishyear") || type.ToLower().Contains("category") || type.ToLower().Contains("access"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidateValue(string type, string value)
        {
            var regFormat = @"^\d{4}$";

            try
            {
                if (type.ToLower().Contains("PublishYear"))
                {
                    try
                    {
                        return Regex.IsMatch(value.ToString(), regFormat) ? true : false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidatePublishYear(int publishYear)
        {
            var regFormat = @"^\d{4}$";

            try
            {
                return Regex.IsMatch(publishYear.ToString(), regFormat) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidateAccess(string access)
        {
            try
            {
                if (access.ToLower().Contains("rare") || access.ToLower().Contains("public"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
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

using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.UserProcessor;

namespace DataLibrary.BusinessLogic
{
    public static class Verifier
    {
        public static (int, UserModel) VerifyeUser(string username, string password)
        {
            var userModel = new UserModel();
            bool isError = false;
            bool isUserFound = false;
            var user = LoadUser(username);

            if (user != null)
            {
                if (user.Count == 1)
                {
                    userModel = user[0];
                    isUserFound = true;
                }
            }
            else
            {
                isError = true;
            }

            return ((!isError ? isUserFound ? Hashing.ValidatePassword(password, userModel.Pass) ? 6 : 3 : 2 : 4), userModel);
        }

        public static bool VerifyToken(string token)
        {
            return TokenProcessor.VerifyToken(token) ? true : false;
        }

        public static int VerifyRegistration(UserModel userModel)
        {
            return RegisterUser(userModel);
        }
    }
}

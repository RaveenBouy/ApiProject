using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static List<UserModel> LoadUser(string username)
        {
            var sql = $@"SELECT * FROM user WHERE UserName = '{username}'";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static int RegisterUser(UserModel userModel)
        {
            bool isVerified;
            if (userModel.UserType == 1)
            {
                isVerified = true;
            }
            else
            {
                isVerified = false;
            }

            var sql = @"INSERT INTO user(UserName, Email, Pass, UserType, IsVerified) "+
                      $"VALUES(@Username, @Email, @Pass, @UserType, {isVerified})";

            var model = new UserModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Pass = Hashing.HashPassword(userModel.Pass),
                UserType = userModel.UserType
            };

            SetUserRole(model.UserName);
            return SqlDataAccess.SaveData(sql, model);               
        }

        public static int SetUserRole(string userName)
        {
            var sql = @"INSERT INTO user_roles(UserName,role) "+
                       "VALUES(@Username, @Role)";

            var model = new UserRoleModel
            {
                UserName = userName,
                Role = 1
            };

            return SqlDataAccess.SaveData(sql, model);
        }

        public static int GetUserRole(string token)
        {
            var sql = "SELECT user_roles.Role " +
                      "FROM user_roles " +
                      "INNER JOIN user ON user.UserName = user_roles.UserName " +
                      "INNER JOIN authentication_token ON authentication_token.UserId = user.Id " +
                     $"WHERE authentication_token.Token = '{token}' ";

            var userRole = SqlDataAccess.LoadData<int>(sql);

            try
            {
                return userRole[0];

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int GetUserType(string token)
        {
            if (TokenProcessor.VerifyToken(token))
            {
                var sql = "SELECT user.UserType " +
                          "FROM user " +
                          "INNER JOIN authentication_token " +
                          "ON user.Id = authentication_token.UserId " +
                         $"WHERE Token = '{token}' ";

                var user = SqlDataAccess.LoadData<UserModel>(sql);

                if (user != null)
                {
                    return user[0].UserType;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}

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
            var sql = $@"SELECT Id, UserName, Email, Pass, UserType FROM user WHERE UserName = '{username}'";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static int RegisterUser(UserModel userModel)
        {
            var sql = $@"INSERT INTO user(UserName, Email, Pass, UserType)
                         VALUES(@Username, @Email, @Pass, @UserType)";

            var model = new UserModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Pass = Hashing.HashPassword(userModel.Pass),
                UserType = userModel.UserType
            };

            return SqlDataAccess.SaveData(sql, model);               
        }

        public static int SetUserRole(string userName)
        {
            var sql = $@"INSERT INTO user_roles(UserName,role)
                         VALUES(@Username, @Role)";

            var model = new UserRoleModel
            {
                UserName = userName,
                Role = 1
            };

            return SqlDataAccess.SaveData(sql, model);
        }

        public static List<UserRoleModel> GetUserRole(string username)
        {
            var sql = $@"SELECT UserName, Role FROM user_roles WHERE UserName = '{username}'";
            return SqlDataAccess.LoadData<UserRoleModel>(sql);
        }

        public static int GetUserType(string token)
        {
            if (TokenProcessor.VerifyToken(token))
            {
                var sql = $"SELECT user.UserType " +
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

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

        public static int UpdateUser(int id, string type, string value)
        {
            var sql = @"UPDATE user " +
                      $"SET {type} = @Value " +
                       "WHERE id = @Id";

            var data = new DynamicUpdateModel
            {
                Id = id,
                Type = type,
                Value = value
            };

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteUser(int id)
        {
            var sql = @"DELETE FROM user " +
                      $"WHERE id = {id} ";

            DeleteUserAuthTokens(id);
            DeleteUserRole(id);
            return SqlDataAccess.SaveData(sql, id);
        }

        public static int DeleteUserAuthTokens(int id)
        {
            var sql = @"DELETE FROM authentication_token " +
                      $"WHERE UserId = {id} ";

            DeleteUserRole(id);
            return SqlDataAccess.SaveData(sql, id);
        }

        public static int DeleteUserRole(int id)
        {
            var sql = "SELECT UserName FROM user " +
                     $"WHERE Id = {id}";

            var user = SqlDataAccess.LoadData<UserModel>(sql);
            string username = null;

            try
            {
                username = user[0].UserName;
            }
            catch (Exception)
            {
                return 3;
            }

            var sql2 = @"DELETE FROM user_roles " +
                      $"WHERE username = '{username}' ";

            return SqlDataAccess.SaveData(sql2, id);
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

        public static IEnumerable<UserModel> GetAllUsers(string token)
        {
            var sql = "SELECT * FROM user";

            var userRole = GetUserRole(token);

            if (userRole != 0)
            {
                return null;
            }

            return SqlDataAccess.LoadData<UserModel>(sql.ToString());
        }
    }
}

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
            var sql = $@"SELECT Id, UserName, Email, Pass FROM user WHERE UserName = '{username}'";
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
                Pass = userModel.Pass,
                UserType = userModel.UserType
            };

            return SqlDataAccess.SaveData(sql, model);               
        }
    }
}

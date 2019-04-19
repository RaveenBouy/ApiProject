using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class TokenProcessor
    {
        public static string GenerateToken(int size = 64)
        {
            const string charSet = "abcdefghiIlo1jkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                var result = new StringBuilder(size);

                foreach (var b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }

                return result.ToString();
            }
        }

        public static bool VerifyToken(string token)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            int value;
            var sql = @"SELECT COUNT(token) FROM authentication_token "+
                      $"WHERE Token='{token}' "+
                      $"AND ExpireDateTime > '{date}'";

            try
            {
                value = SqlDataAccess.LoadData<int>(sql)[0];
            }
            catch (Exception)
            {

                return false;
            }

            return value == 1 ? true : false;
        }

        public static int WriteAuthenticationToken(int userId, string token)
        {
            var date = DateTime.Now.AddHours(24);
            const string sql = @"INSERT INTO authentication_token(UserId, Token, ExpireDateTime) VALUES(@UserId, @Token, @ExpireDateTime);";

            var data = new TokenModel
            {
                Token = token,
                UserId = userId,
                ExpireDateTime = date.ToString("yyyy-MM-dd hh:mm:ss")
            };

            SqlDataAccess.SaveData(sql, data);

            return UserProcessor.GetUserRole(token);
        }
    }
}

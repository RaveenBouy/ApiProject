﻿using System;
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
            var sql = $@"SELECT COUNT(token) FROM authenticationToken WHERE token='{token}'";
            var value = SqlDataAccess.LoadData<int>(sql)[0];
            return value == 0 ? true : false;
        }

        public static int WriteAuthenticationToken(int userId, string token)
        {
            var date = DateTime.Now.AddHours(12);
            const string sql = @"INSERT INTO authenticationToken(userId, token, expireDateTime) VALUES(@Token, @UserId, @Additional, @ExpireDateTime);";

            var data = new TokenModel
            {
                Token = token,
                UserId = userId,
                ExpireDateTime = date.ToString("yyyy-MM-dd hh:mm:ss")
            };

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}

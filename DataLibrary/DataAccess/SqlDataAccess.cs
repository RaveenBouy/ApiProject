using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        private static string GetConnectionString()
        {
            return "Server=127.0.0.1; Port=3306; Database=APIProject; Uid=root";
        }

        public static List<T> LoadData<T>(string sql)
        {
            try
            {
                using (IDbConnection con = new  MySqlConnection(GetConnectionString()))
                {
                    return con.Query<T>(sql).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            try
            {
                using (IDbConnection con = new MySqlConnection(GetConnectionString()))
                {
                    return con.Execute(sql, data);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

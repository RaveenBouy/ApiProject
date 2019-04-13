using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class BookProcessor
    {
        public static IEnumerable<ItemModel> GetAllBooks()
        {
            var sql = "SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book'" +
                      "AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static IEnumerable<ItemModel> GetBookByType(string type, string value)
        {
            var sql = "SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book'" +
                      $"AND {type} LIKE '%{value}%'" +
                      $"AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static List<ItemModel> GetBookById(int id)
        {
            var sql = "SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book' " +
                      $"AND Id = {id} " +
                      $"AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static IEnumerable<ItemModel> GetAllBooks(string token)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book' ");

            switch (UserProcessor.GetUserType(token))
            {
                case -1:
                    return null;
                case 2:
                    sql.Append("AND Access = 'public' ");
                    break;
            }
            
            return SqlDataAccess.LoadData<ItemModel>(sql.ToString());
        }

        public static IEnumerable<ItemModel> GetBookByType(string token, string type, string value)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book'" +
                      $"AND {type} LIKE '%{value}%' ");

            switch (UserProcessor.GetUserType(token))
            {
                case -1:
                    return null;
                case 2:
                    sql.Append("AND Access = 'public' ");
                    break;
            }

            return SqlDataAccess.LoadData<ItemModel>(sql.ToString());
        }

        public static List<ItemModel> GetBookById(string token, int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Id, Title, Description, Author, PublishYear FROM library_item " +
                      "WHERE Category = 'book' " +
                      $"AND Id = {id} " );

            switch (UserProcessor.GetUserType(token))
            {
                case -1:
                    return null;
                case 2:
                    sql.Append("AND Access = 'public' ");
                    break;
            }

            return SqlDataAccess.LoadData<ItemModel>(sql.ToString());
        }
    }
}

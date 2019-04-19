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
            var sql = "SELECT * FROM library_item " +
                      "WHERE Category = 'book'" +
                      "AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static IEnumerable<ItemModel> GetBookByType(string type, string value)
        {
            var sql = "SELECT * FROM library_item " +
                      "WHERE Category = 'book'" +
                     $"AND {type} LIKE '%{value}%'" +
                     $"AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static List<ItemModel> GetBookById(int id)
        {
            var sql = "SELECT * FROM library_item " +
                      "WHERE Category = 'book' " +
                     $"AND Id = {id} " +
                     $"AND Access = 'public'";

            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static IEnumerable<ItemModel> GetAllBooks(string token)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
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

        public static List<ItemModel> GetBookByType(string token, string type, string value)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'book' " +
                      $"AND {type} ");

            if (type.Equals("Id"))
            {
                sql.Append($" = {value} ");
            }
            else
            {
                sql.Append($" LIKE '%{value}%' ");
            }

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
            sql.Append("SELECT * FROM library_item " +
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

        public static int SetBook(ItemModel itemModel)
        {
            var sql = $@"INSERT INTO library_item(Title, Description, Author, PublishYear, Category, Access) "+
                        "VALUES(@Title, @Description, @Author, @PublishYear, @Category, @Access)";

            var model = new ItemModel
            {
                Title = itemModel.Title,
                Description = itemModel.Description,
                Author = itemModel.Author,
                PublishYear = itemModel.PublishYear,
                Category = itemModel.Category,
                Access = itemModel.Access
            };

            return SqlDataAccess.SaveData(sql, model);
        }

        public static int UpdateBook(int id, string type, string value)
        {
            var sql = @"UPDATE library_item "+
                      $"SET {type} = @Value " +
                       "WHERE id = @Id " +
                       "AND Category = 'book'";

            var data = new DynamicUpdateModel
            {
                Id = id,
                Type = type,
                Value = value
            };

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteBook(int id)
        {
            var sql = @"DELETE FROM library_item " +
                      $"WHERE id = {id} " +
                       "AND Category = 'book'";

            return SqlDataAccess.SaveData(sql, id);
        }
    }
}

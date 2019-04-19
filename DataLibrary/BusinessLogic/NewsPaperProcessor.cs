using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public class NewspaperProcessor
    {
        public static IEnumerable<ItemModel> GetAllNewspapers(string token)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'Newspaper' ");

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

        public static IEnumerable<ItemModel> GetNewspapersByType(string token, string type, string value)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'Newspaper' " +
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

        public static List<ItemModel> GetNewspaperById(string token, int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'Newspaper' " +
                      $"AND Id = {id} ");

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

        public static int SetNewspaper(ItemModel itemModel)
        {
            var sql = $@"INSERT INTO library_item(Title, Description, Author, PublishYear, Category, Access) " +
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

        public static int UpdateNewspaper(int id, string type, string value)
        {
            var sql = @"UPDATE library_item " +
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

        public static int DeleteNewspaper(int id)
        {
            var sql = @"DELETE FROM library_item " +
                      $"WHERE id = {id}";

            return SqlDataAccess.SaveData(sql, id);
        }
    }
}

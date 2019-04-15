using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public class JournalProcessor
    {
        public static IEnumerable<ItemModel> GetAllJournals(string token)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'journal' ");

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

        public static IEnumerable<ItemModel> GetJournalsByType(string token, string type, string value)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'journal'" +
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

        public static List<ItemModel> GetJournalById(string token, int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM library_item " +
                       "WHERE Category = 'journal' " +
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

        public static int SetJournal(ItemModel itemModel)
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

        public static int UpdateJournal(int id, string type, string value)
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

        public static int DeleteJournal(int id, string category)
        {
            var sql = @"DELETE FROM library_item " +
                      $"WHERE id = {id} " +
                      $"AND Category = '{category}'";

            return SqlDataAccess.SaveData(sql, id);
        }
    }
}

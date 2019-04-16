using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.UserProcessor;

namespace DataLibrary.BusinessLogic
{
    public static class Verifier
    {
        public static (int, UserModel) VerifyeUser(string username, string password)
        {
            var userModel = new UserModel();
            bool isError = false;
            bool isUserFound = false;
            bool isVerified = false;
            var user = LoadUser(username);

            if (user != null)
            {
                if (user.Count == 1)
                {
                    userModel = user[0];
                    isUserFound = true;

                    if (userModel.IsVerified)
                    {
                        isVerified = true;
                    }
                }
            }
            else
            {
                isError = true;
            }

            return ((!isError ? isUserFound ? Hashing.ValidatePassword(password, userModel.Pass) ? isVerified ? 6 : 0 : 3 : 2 : 4), userModel);
        }

        public static bool VerifyToken(string token)
        {
            return TokenProcessor.VerifyToken(token) ? true : false;
        }

        public static int VerifyRegistration(UserModel userModel)
        {
            return RegisterUser(userModel);
        }

        public static int VerifyInsertLibraryItem(ItemModel itemModel, string category)
        {
            int itemInsertStatus = 0;
            var userRole = GetUserRole(itemModel.AuthToken);

            if (userRole != 0)
            {
                return -1;
            }

            switch (category)
            {
                case "book":
                    itemModel.Category = category;
                    itemInsertStatus = BookProcessor.SetBook(itemModel);
                    break;
                case "newspaper":
                    itemModel.Category = category;
                    itemInsertStatus = NewspaperProcessor.SetNewspaper(itemModel);
                    break;
                case "magazine":
                    itemModel.Category = category;
                    itemInsertStatus = MagazineProcessor.SetMagazine(itemModel);
                    break;
                case "journal":
                    itemModel.Category = category;
                    itemInsertStatus = JournalProcessor.SetJournal(itemModel);
                    break;
                case "manuscript":
                    itemModel.Category = category;
                    itemInsertStatus = ManuscriptProcessor.SetManuscript(itemModel);
                    break;
            }

            return itemInsertStatus;
        }

        public static int VerifyUpdateLibraryItem(string token, int id, string type, string value, string category)
        {
            int itemUpdateStatus = 0;
            var userRole = GetUserRole(token);

            if (userRole != 0)
            {
                return -1;
            }

            switch (category)
            {
                case "book":
                    itemUpdateStatus = BookProcessor.UpdateBook(id, type, value);
                    break;
                case "newspaper":
                    itemUpdateStatus = NewspaperProcessor.UpdateNewspaper(id, type, value);
                    break;
                case "magazine":
                    itemUpdateStatus = MagazineProcessor.UpdateMagazine(id, type, value);
                    break;
                case "journal":
                    itemUpdateStatus = JournalProcessor.UpdateJournal(id, type, value);
                    break;
                case "manuscript":
                    itemUpdateStatus = ManuscriptProcessor.UpdateManuscript(id, type, value);
                    break;
            }

            return itemUpdateStatus;
        }

        public static int VerifyDeleteLibraryItem(string token, int id, string category)
        {
            var userRole = GetUserRole(token);

            if (userRole != 0)
            {
                return -1;
            }

            return BookProcessor.DeleteBook(id, category);
        }

        public static int VerifyUpdateUser(string token, int id, string type, string value)
        {
            var userRole = GetUserRole(token);

            if (userRole != 0)
            {
                return -1;
            }

            return UserProcessor.UpdateUser(id, type, value);
        }

        public static int VerifyDeleteUser(string token, int id)
        {
            var userRole = GetUserRole(token);

            if (userRole != 0)
            {
                return -1;
            }

            return UserProcessor.DeleteUser(id);
        }
    }
}

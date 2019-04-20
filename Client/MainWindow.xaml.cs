using Client.Classes;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ItemModel> Item { get; set; } = new List<ItemModel>();
        public List<ItemModel> VisitorItem { get; set; } = new List<ItemModel>();
        public List<UserModel> User { get; set; } = new List<UserModel>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Search()
        {
            LBItemList.DataContext = null;
            GridItemDetails.DataContext = null;
            ComboBoxItem typeItem = (ComboBoxItem)CBCategory.SelectedItem;
            string type = typeItem.Content.ToString();

            ComboBoxItem typeItem2 = (ComboBoxItem)CBSearchType.SelectedItem;
            string value = typeItem2.Content.ToString();

            var searchvalue = TBSearchValue.Text.Equals("") ? "0" : TBSearchValue.Text;

            if (type == T3.Content.ToString()) //Books
            {
                BookProcessor book = new BookProcessor();
                Item = book.GetBooks(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;

            }
            else
            if (type == T4.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                Item = newspaper.GetNewspapers(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T5.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                Item = journal.GetJournals(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T6.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                Item = magazine.GetMagazines(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T7.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                Item = manuscript.GetManuscripts(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
        }

        private void SearchAll()
        {
            LBItemList.DataContext = null;
            GridItemDetails.DataContext = null;
            ComboBoxItem typeItem = (ComboBoxItem)CBCategory.SelectedItem;
            string type = typeItem.Content.ToString();

            if (type == T3.Content.ToString()) //Books
            {
                BookProcessor book = new BookProcessor();
                Item = book.GetAllBooks(ReferenceList.Token);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T4.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                Item = newspaper.GetAllNewspapers(ReferenceList.Token);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T5.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                Item = journal.GetAllJournals(ReferenceList.Token);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T6.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                Item = magazine.GetAllMagazines(ReferenceList.Token);
                LBItemList.DataContext = Item;
            }
            else
            if (type == T7.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                Item = manuscript.GetAllManuscripts(ReferenceList.Token);
                LBItemList.DataContext = Item;
            }
        }

        private void UserLogin()
        {
            UserProcessor processor = new UserProcessor();
            var login = processor.Login(Username.Text, Password.Text);

            if (login.Response.Equals(200))
            {
                if (login.Value.Equals(1))
                {
                    VisibityForRoles(login.Value);
                }
                else
                {
                    VisibityForRoles(login.Value);
                }
            }
            else
            {
                MessageBox.Show(login.Info);
            }
        }

        private void VisibityForRoles(int x)
        {
            switch (x)
            {
                case 0:
                    GridLogin.Visibility = Visibility.Hidden;
                    GridStaffHome.Visibility = Visibility.Visible;
                    GridStaffMenu.Visibility = Visibility.Visible;
                    GridHomeNav.Visibility = Visibility.Visible;
                    GridHomeLogout.Visibility = Visibility.Visible;
                    GridViewBooksBtn.Visibility = Visibility.Hidden;
                    GridBackground.Visibility = Visibility.Visible;

                    break;
                case 1:
                    GridLogin.Visibility = Visibility.Hidden;
                    GridMemberHome.Visibility = Visibility.Visible;
                    GridHomeNav.Visibility = Visibility.Visible;
                    GridHomeLogout.Visibility = Visibility.Visible;
                    GridViewBooksBtn.Visibility = Visibility.Hidden;
                    GridBackground.Visibility = Visibility.Visible;
                    break;
            }
        }

        private async void UserRegisterAsync()
        {
            ComboBoxItem typeItem = (ComboBoxItem)CBUserType.SelectedItem;
            string type = typeItem.Content.ToString();
            int userType;

            if (type == T1.Content.ToString()) //Local
            {
                userType = 1;
            }
            else //foreign
            {
                userType = 2;
            }

            UserProcessor processor = new UserProcessor();
            AuthResponseModel response = new AuthResponseModel();
            response = await processor.RegisterAsync(Username1.Text, Password1.Text, userType);


            if (response.Response.Equals(200))
            {
                GridLogin.Visibility = Visibility.Visible;
                GridRegister.Visibility = Visibility.Hidden;
                MessageBox.Show("Account created successfully! \n If registed as foreign student, you'll have to wait for Librarian's approval.");
            }
            else
            {
                MessageBox.Show(response.Info);
            }
        }


        private async void InsertItemAsync()
        {
            var title = TBAddItemTitle.Text;
            var description = TBAddItemDescription.Text;
            var author = TBAddItemAuthor.Text;
            var response = new AuthResponseModel();
            int publishYear;

            if (!Regex.IsMatch(TBAddItemPublishYear.Text, @"^\d{4}$"))
            {
                MessageBox.Show("Year should consist of 4 Numbers!");
                return;
            }
            else
            {
                publishYear = Convert.ToInt32(TBAddItemPublishYear.Text);
            }

            ComboBoxItem typeItem = (ComboBoxItem)CBAddItemCategory.SelectedItem;
            string category = typeItem.Content.ToString();

            ComboBoxItem typeItem2 = (ComboBoxItem)CBAddItemAccess.SelectedItem;
            string access = typeItem2.Content.ToString();

            if (category == T13.Content.ToString()) //Books
            {
                BookProcessor book = new BookProcessor();
                response = await book.InsertBook(title, description, author, publishYear, access);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T14.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                response = await newspaper.InsertNewspaper(title, description, author, publishYear, access);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T15.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                response = await journal.InsertJournal(title, description, author, publishYear, access);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T16.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                response = await magazine.InsertMagazine(title, description, author, publishYear, access);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T17.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                response = await manuscript.InsertManuscript(title, description, author, publishYear, access);
                MessageBox.Show(response.Info);
            }
        }

        private async void UpdateItem()
        {
            var idText = TBUpdateItemId.Text;
            var value = TBUpdateItemValue.Text;
            var response = new AuthResponseModel();
            int id;

            if (!Regex.IsMatch(idText, @"^\d+$"))
            {
                MessageBox.Show("ID should be a number!");
                return;
            }
            else
            {
                id = Convert.ToInt32(idText);
            }

            ComboBoxItem typeItem = (ComboBoxItem)CBUpdateItemCategory.SelectedItem;
            string category = typeItem.Content.ToString();

            ComboBoxItem typeItem2 = (ComboBoxItem)CBUpdateItemType.SelectedItem;
            string type = typeItem2.Content.ToString();

            if (category == T26.Content.ToString()) //Books
            {
                BookProcessor book = new BookProcessor();
                response = await book.UpdateBook(id, type, value);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T27.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                response = await newspaper.UpdateNewspaper(id, type, value);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T28.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                response = await journal.UpdateJournal(id, type, value);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T29.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                response = await magazine.UpdateMagazine(id, type, value);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T30.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                response = await manuscript.UpdateManuscript(id, type, value);
                MessageBox.Show(response.Info);
            }
        }

        private async void DeleteItem()
        {
            var idText = TBDeleteItemId.Text;
            var response = new AuthResponseModel();
            int id;

            if (!Regex.IsMatch(idText, @"^\d+$"))
            {
                MessageBox.Show("ID should be a number!");
                return;
            }
            else
            {
                id = Convert.ToInt32(idText);
            }

            ComboBoxItem typeItem = (ComboBoxItem)CBDeleteItemCategory.SelectedItem;
            string category = typeItem.Content.ToString();

            if (category == T31.Content.ToString()) //Books
            {
                BookProcessor book = new BookProcessor();
                response = await book.DeleteBook(id);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T32.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                response = await newspaper.DeleteNewspaper(id);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T33.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                response = await journal.DeleteJournal(id);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T34.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                response = await magazine.DeleteMagazine(id);
                MessageBox.Show(response.Info);
            }
            else
            if (category == T35.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                response = await manuscript.DeleteManuscript(id);
                MessageBox.Show(response.Info);
            }
        }

        private async void UpdateUser()
        {
            var idText = TBUpdateUserId.Text;
            var value = TBUpdateUserValue.Text;
            var response = new AuthResponseModel();
            int id;

            if (!Regex.IsMatch(idText, @"^\d+$"))
            {
                MessageBox.Show("ID should be a number!");
                return;
            }
            else
            {
                id = Convert.ToInt32(idText);
            }

            ComboBoxItem typeItem = (ComboBoxItem)CBUpdateUserType.SelectedItem;
            string type = typeItem.Content.ToString();

            if (type == T38.Content.ToString()) //UserType
            {
                if (!Regex.IsMatch(value, @"^\d+$"))
                {
                    MessageBox.Show("UserType should either be 1 = Local / 2 = Foreigner");
                    return;
                }
            }
            else
            if (type == T39.Content.ToString()) //IsVerified
            {
                if (!Regex.IsMatch(value, @"^\d+$"))
                {
                    MessageBox.Show("IsVerified should either be 0/1");
                    return;
                }
            }

            var processor = new UserProcessor();
            response = await processor.UpdateUser(id, type, value);
            MessageBox.Show(response.Info);
        }

        private async void DeleteUser()
        {
            var idText = TBDeleteUserId.Text;
            var response = new AuthResponseModel();
            int id;

            if (!Regex.IsMatch(idText, @"^\d+$"))
            {
                MessageBox.Show("ID should be a number!");
                return;
            }
            else
            {
                id = Convert.ToInt32(idText);
            }

            var processor = new UserProcessor();
            response = await processor.DeleteUser(id);
            MessageBox.Show(response.Info);
        }

        private async void GetUsers()
        {
            UserProcessor user = new UserProcessor();
            User = await user.GetAllUsers();
            LBUserList.DataContext = User;
        }

        private async void SearchAllVisitor()
        {
            LBVisitorItemList.DataContext = null;
            GridVisitorItemDetails.DataContext = null;
            BookProcessor book = new BookProcessor();
            VisitorItem = await book.GetVisitorAllBooks();
            LBVisitorItemList.DataContext = VisitorItem;
        }

        private async void SearchVisitor()
        {
            LBVisitorItemList.DataContext = null;
            GridVisitorItemDetails.DataContext = null;

            ComboBoxItem typeItem2 = (ComboBoxItem)CBVisitorSearchType.SelectedItem;
            string value = typeItem2.Content.ToString();

            var searchvalue = TBVisitorSearchValue.Text.Equals("") ? "0" : TBVisitorSearchValue.Text;

            BookProcessor book = new BookProcessor();
            VisitorItem = await book.GetVisitorBooks(value, searchvalue);
            LBVisitorItemList.DataContext = VisitorItem;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(Username.Text, @"^[a-zA-Z0-9-_%£$@\\/]*$"))
            {
                if (Regex.IsMatch(Password.Text, @"^[a-zA-Z0-9-_%£$@/\\]*$"))
                {
                    UserLogin();
                }
                else
                {
                    MessageBox.Show("Invalid format for password!");
                }
            }
            else
            {
                MessageBox.Show("Invalid format for Username!");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(Username1.Text, @"^[a-zA-Z0-9-_%£$@\\/]*$"))
            {
                if (Regex.IsMatch(Password1.Text, @"^[a-zA-Z0-9-_%£$@/\\]*$"))
                {
                    if (CBUserType.SelectedIndex.Equals(-1))
                    {
                        MessageBox.Show("User Type is a required Field!");
                    }
                    else
                    {
                        UserRegisterAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid format for password!");
                }
            }
            else
            {
                MessageBox.Show("Invalid format for Username!");
            }
        }

        private void CreateNewAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLogin.Visibility = Visibility.Hidden;
            GridRegister.Visibility = Visibility.Visible;
        }

        private void GotoLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLogin.Visibility = Visibility.Visible;
            GridRegister.Visibility = Visibility.Hidden;
        }

        private void BtnViewAllBooks_Click(object sender, RoutedEventArgs e)
        {
            GridVisitor.Visibility = Visibility.Visible;
            GridHomeNav.Visibility = Visibility.Visible;
            GridHomeLogout.Visibility = Visibility.Visible;
            GridRegister.Visibility = Visibility.Hidden;
            GridLogin.Visibility = Visibility.Hidden;
            GridViewBooksBtn.Visibility = Visibility.Hidden;
        }

        private void LBItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBItemList.SelectedIndex != -1)
            {
                GridItemDetails.Visibility = Visibility.Visible;
                GridItemDetails.DataContext = Item[LBItemList.SelectedIndex];
            }
            else
            {
                GridItemDetails.Visibility = Visibility.Hidden;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void BtnSearchAll_Click(object sender, RoutedEventArgs e)
        {
            SearchAll();
        }

        private void BtnStaffViewItems_Click(object sender, RoutedEventArgs e)
        {
            GridMemberHome.Visibility = Visibility.Visible;
            GridStaffHome.Visibility = Visibility.Hidden;
            GridHomeHome.Visibility = Visibility.Visible;
        }

        private void BtnStaffUpdateItems_Click(object sender, RoutedEventArgs e)
        {
            GridItemManage.Visibility = Visibility.Visible;
            GridStaffMenu.Visibility = Visibility.Hidden;
            GridHomeHome.Visibility = Visibility.Visible;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            GridMemberHome.Visibility = Visibility.Hidden;
            GridStaffHome.Visibility = Visibility.Hidden;
            GridLogin.Visibility = Visibility.Visible;
            GridHomeLogout.Visibility = Visibility.Collapsed;
            GridHomeHome.Visibility = Visibility.Collapsed;
            GridHomeNav.Visibility = Visibility.Collapsed;
            GridItemManage.Visibility = Visibility.Hidden;
            GridViewUsers.Visibility = Visibility.Hidden;
            GridUpdateUsers.Visibility = Visibility.Hidden;
            GridVisitor.Visibility = Visibility.Hidden;
            GridViewBooksBtn.Visibility = Visibility.Visible;
            GridBackground.Visibility = Visibility.Hidden;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            GridStaffHome.Visibility = Visibility.Visible;
            GridStaffMenu.Visibility = Visibility.Visible;
            GridHomeHome.Visibility = Visibility.Collapsed;
            GridItemManage.Visibility = Visibility.Hidden;
            GridMemberHome.Visibility = Visibility.Hidden;
            GridViewUsers.Visibility = Visibility.Hidden;
            GridUpdateUsers.Visibility = Visibility.Hidden;
            GridVisitor.Visibility = Visibility.Hidden;
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            InsertItemAsync();
        }

        private void BtnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            UpdateItem();
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem();
        }

        private void BtnViewUsers_Click(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }

        private void BtnStaffViewUsers_Click(object sender, RoutedEventArgs e)
        {
            GridStaffMenu.Visibility = Visibility.Hidden;
            GridViewUsers.Visibility = Visibility.Visible;
            GridHomeHome.Visibility = Visibility.Visible;
        }

        private void BtnStaffUpdateUsers_Click(object sender, RoutedEventArgs e)
        {
            GridUpdateUsers.Visibility = Visibility.Visible;
            GridHomeHome.Visibility = Visibility.Visible;
            GridStaffMenu.Visibility = Visibility.Hidden;
        }

        private void LBUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBUserList.SelectedIndex != -1)
            {
                GridUserDetails.Visibility = Visibility.Visible;
                GridUserDetails.DataContext = User[LBUserList.SelectedIndex];
            }
            else
            {
                GridUserDetails.Visibility = Visibility.Hidden;
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser();
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            UpdateUser();
        }

        private void LBVisitorItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBVisitorItemList.SelectedIndex != -1)
            {
                GridVisitorItemDetails.Visibility = Visibility.Visible;
                GridVisitorItemDetails.DataContext = VisitorItem[LBVisitorItemList.SelectedIndex];
            }
            else
            {
                GridVisitorItemDetails.Visibility = Visibility.Hidden;
            }
        }

        private void BtnVisitorSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchVisitor();
        }

        private void BtnVisitorSearchAll_Click(object sender, RoutedEventArgs e)
        {
            SearchAllVisitor();
        }
    }
}

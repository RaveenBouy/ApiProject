using Client.Classes;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ItemModel> Item { get; set; } = new List<ItemModel>();
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
            if(type == T4.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                Item = newspaper.GetNewspapers(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if(type == T5.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                Item = journal.GetJournals(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if(type == T6.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                Item = magazine.GetMagazines(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if(type == T7.Content.ToString()) //Manuscripts
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

                    break;
                case 1:
                    GridLogin.Visibility = Visibility.Hidden;
                    GridMemberHome.Visibility = Visibility.Visible;
                    GridHomeNav.Visibility = Visibility.Visible;
                    GridHomeLogout.Visibility = Visibility.Visible;
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if( Regex.IsMatch(Username.Text, @"^[a-zA-Z0-9-_%£$@\\/]*$"))
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

        private void BtnStaffViewUsers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStaffUpdateUsers_Click(object sender, RoutedEventArgs e)
        {

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
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            GridStaffHome.Visibility = Visibility.Visible;
            GridStaffMenu.Visibility = Visibility.Visible;
            GridHomeHome.Visibility = Visibility.Collapsed;
            GridItemManage.Visibility = Visibility.Hidden;
            GridMemberHome.Visibility = Visibility.Hidden;
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            InsertItemAsync();
        }

        private async Task InsertItemAsync()
        {
            var title = TBAddItemTitle.Text;
            var description = TBAddItemDescription.Text;
            var author = TBAddItemAuthor.Text;
            var response = new AuthResponseModel();
            int publishYear;

            if (!Regex.IsMatch(TBAddItemPublishYear.Text, "[^0-9.-]+"))
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
                response = await book.InsertBook(title,description,author,publishYear, access);
                MessageBox.Show(response.Info);
            }
            /*else
            if (category == T14.Content.ToString()) //Newspapers
            {
                NewspaperProcessor newspaper = new NewspaperProcessor();
                Item = newspaper.GetNewspapers(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (category == T15.Content.ToString()) //Journals
            {
                JournalProcessor journal = new JournalProcessor();
                Item = journal.GetJournals(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (category == T16.Content.ToString()) //Magazines
            {
                MagazineProcessor magazine = new MagazineProcessor();
                Item = magazine.GetMagazines(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }
            else
            if (category == T17.Content.ToString()) //Manuscripts
            {
                ManuscriptProcessor manuscript = new ManuscriptProcessor();
                Item = manuscript.GetManuscripts(ReferenceList.Token, value, searchvalue);
                LBItemList.DataContext = Item;
            }*/
        }
    }
}

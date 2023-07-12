using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using myLibrary.myLibraryClasses;

namespace myLibrary.Windows
{
    /// <summary>
    /// Interaction logic for DeleteReaderWindow.xaml
    /// </summary>
    public partial class DeleteReaderWindow : Window
    {
        accountsClass account = new accountsClass();
       
        public DeleteReaderWindow()
        {
            InitializeComponent();
            var accounts = account.GetAllUsers();
            usersDropdown.ItemsSource = accounts;
            usersDropdown.DisplayMemberPath = "Name";
            usersDropdown.SelectedValuePath = "IdAccount";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(backButton2))
            {
                AdministratorWindow administratorWindow = new AdministratorWindow();
                Visibility = Visibility.Hidden;
                administratorWindow.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            accountsClass userToDelete = (accountsClass)usersDropdown.SelectedItem;
            userToDelete.DeleteUser();
        }

    }
}

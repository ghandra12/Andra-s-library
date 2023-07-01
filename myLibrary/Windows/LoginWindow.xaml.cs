using myLibrary.myLibraryClasses;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        accountsClass account = new accountsClass();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = emailBox.Text;
            string password = passwordBox.Password;

            accountsClass user = account.GetUserByEmailAndPassword(email, password);

            if(user == null)
            {
                emailBox.Text = "";
                passwordBox.Password = "";
            } else
            {
                if (user.IsAdministrator)
                {
                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    Visibility = Visibility.Hidden;
                    administratorWindow.Show();
                } else
                {
                    ReaderWindow readerWindow = new ReaderWindow();
                    Visibility = Visibility.Hidden;
                    readerWindow.Show();
                }
            }
        }
    }
}

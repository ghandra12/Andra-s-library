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

namespace myLibrary.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        accountsClass account = new accountsClass();
        public AddReaderWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = addEmail.Text;
            string password = addPass.Password;
            string telephone = addTel.Text;
            string name = addName.Text;
            bool admin = check.IsChecked.Value;


            account.AddNewReaderData(email, password,telephone,name,admin);
        }
    }
}

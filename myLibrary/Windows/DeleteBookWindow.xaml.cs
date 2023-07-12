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
using System.Windows.Shapes;

namespace myLibrary.Windows
{
    /// <summary>
    /// Interaction logic for DeleteBookWindow.xaml
    /// </summary>
    public partial class DeleteBookWindow : Window
    {
        booksClass book = new booksClass();
        public DeleteBookWindow()
        {
            InitializeComponent();
            var books = book.GetBooks();
            books = books.Where(b => !b.IdAccount.HasValue).ToList();
            booksDropdown.ItemsSource = books;
            booksDropdown.DisplayMemberPath = "Title";
            booksDropdown.SelectedValuePath = "IdBook";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(backButton2))
            {
                AddBooksWindow wnd = new AddBooksWindow();
                Visibility = Visibility.Hidden;
                wnd.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            booksClass bookToChange = (booksClass)booksDropdown.SelectedItem;
            bookToChange.DeleteBook();
        }
    }
}

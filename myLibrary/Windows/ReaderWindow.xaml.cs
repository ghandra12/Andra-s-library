using myLibrary.myLibraryClasses;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace myLibrary
{
    /// <summary>
    /// Interaction logic for ReaderWindow.xaml
    /// </summary>`
    public partial class ReaderWindow : Window
    {
        public int idReader;
        private booksClass book = new booksClass();
        private List<booksClass> books = new List<booksClass>();
       
       

        public ReaderWindow()
        {
            InitializeComponent();
            books = book.GetBooks();
            this.booksList.ItemsSource = books;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(LogOutBtn))
            {
                LoginWindow loginwindow = new LoginWindow();
                Visibility = Visibility.Hidden;
                loginwindow.Show();
            }
            else
                if (sender.Equals(mybooks))
            {
               books= book.GetMyBooks(this.idReader);
               this.booksList.ItemsSource = books;
            }
            else
                if (sender.Equals(allbooks))
            {
               books= book.GetBooks();
               this.booksList.ItemsSource = books;
            }
        }
        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
        
            if (book.CountBooks(this.idReader))
            {
                Button button = sender as Button;
                booksClass book = button.DataContext as booksClass;
                book.UpdateBook(this.idReader, book.IdBook);
                books = book.GetBooks();
                this.booksList.ItemsSource = books;
            }
          
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            booksClass book = button.DataContext as booksClass;
            book.UpdateBookReturn( book.IdBook);
          
            books = book.GetMyBooks(this.idReader);
            this.booksList.ItemsSource = books;
        }
    }
}

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
using myLibrary.myLibraryClasses;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.IO;

namespace myLibrary.Windows
{
    /// <summary>
    /// Interaction logic for AddBooksWindow.xaml
    /// </summary>
    public partial class AddBooksWindow : Window
    {
        booksClass book = new booksClass();

        public AddBooksWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] buffer = null;
            if (sender.Equals(uploadPhotoBtn))
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
                openDialog.FilterIndex = 1;
                if (openDialog.ShowDialog() == true)
                {
                    imagePicture.Source = new BitmapImage(new Uri(openDialog.FileName));
                }
            }

            if (sender.Equals(addBookBtn))
            {
                var bitmap = imagePicture.Source as BitmapSource;
                var encoder = new PngBitmapEncoder(); // or one of the other encoders
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    buffer = stream.ToArray();
                }

                string title = titleBox.Text;
                string author = authorBox.Text;
                string type = typeBox.Text;
                string description = descriptionBox.Text;
                //string photo = imagePicture.Source;
                book.AddNewBook(title, author, type, description, buffer);
            }
           
            if (sender.Equals(backButton3))
            {
                AdministratorWindow administratorWindow = new AdministratorWindow();
                Visibility = Visibility.Hidden;
                administratorWindow.Show();
            }
            if (sender.Equals(deleteBook))
            {
                DeleteBookWindow wnd = new DeleteBookWindow();
                Visibility = Visibility.Hidden;
                wnd.Show();
            }
        }

    }
  

}

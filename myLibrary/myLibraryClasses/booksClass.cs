using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace myLibrary.myLibraryClasses
{
    class booksClass
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public BitmapImage Photo { get; set; }

        public void AddNewBook(string title, string author, string type, string description, byte[] photo)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"INSERT INTO dbo.books ( [Title], [Author], [Type],[Description],[Photo]) VALUES ('{title}', '{author}', '{type}','{description}', (@photo)); ";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                    cmd.Parameters.AddWithValue("@photo", photo);
                    cmd.ExecuteReader();

                }

                mConnection.Close();
            }
        }

        public List<booksClass> GetBooks()
        {
            List<booksClass> books = new List<booksClass>();
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"SELECT * FROM dbo.books  ORDER BY [Type]";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var book = new booksClass();
                            book.IdBook = (int)reader[0];
                            book.Title = (string)reader[1];
                            book.Author = (string)reader[2];
                            book.Type = (string)reader[3];
                            book.Description = (string)reader[4];
                            book.Photo = LoadImage((byte[])reader[5]);
                            books.Add(book);
                        }
                    }
                }

                mConnection.Close();
            }

            return books;
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}

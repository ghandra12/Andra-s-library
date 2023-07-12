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
        public int? IdAccount { get; set; }
        public bool Indisponibility { get; set; }
        public DateTime CreationDate { get; set; }

        //Butoanele apar doar daca cartea este disponibila adica Disponibility e marcat ca NULL:
        public string BorrowButtonVisiblity => IdAccount.HasValue || Indisponibility ? "Hidden" : "Visible";
        public string ReturnButtonVisiblity => !IdAccount.HasValue || Indisponibility ? "Hidden" : "Visible";
        public string IsDisponible => !Indisponibility ? "Hidden" : "Visible";
        public void AddNewBook(string title, string author, string type, string description, byte[] photo)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"INSERT INTO dbo.books ( [Title], [Author], [Type],[Description],[Photo], [CreationDate], [Indisponibility])" +
                $" VALUES ('{title}', '{author}', '{type}','{description}', (@photo), '{DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day}', '0'); ";
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

            string Command = $"SELECT * FROM dbo.books WHERE AccountId is NULL ORDER BY [Indisponibility], [Type]";
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
                            book.IdAccount = Convert.IsDBNull(reader[6]) ? null : (int?)reader[6];
                            book.Indisponibility = (bool)reader[7];
                            book.CreationDate = (DateTime)reader[8];
                            books.Add(book);
                        }
                    }
                }

                mConnection.Close();
            }

            return books;
        }
        public void UpdateBook(int id,int idBook)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"UPDATE dbo.books SET AccountId={id} WHERE idBook={idBook};";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                   
                    cmd.ExecuteReader();

                }

                mConnection.Close();
            }
        }
        public void UpdateBookReturn(int idBook)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"UPDATE dbo.books SET AccountId=NULL WHERE idBook={idBook};";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {

                    cmd.ExecuteReader();

                }

                mConnection.Close();
            }
        }
        public List<booksClass> GetMyBooks(int id)
        {
            List<booksClass> books = new List<booksClass>();
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"SELECT * FROM dbo.books WHERE AccountId={id}  ORDER BY [Type]";
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
                            book.IdAccount = Convert.IsDBNull(reader[6]) ? null : (int?)reader[6];
                            book.Indisponibility = (bool)reader[7];
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
        public void ChangeBookDisponibility()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"UPDATE dbo.books SET Indisponibility='1' WHERE idBook={this.IdBook}";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {

                    cmd.ExecuteReader();

                }

                mConnection.Close();
            }
        }
        public bool  CountBooks(int  id)
        {
            bool found;
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"SELECT COUNT (idBook) FROM dbo.books  WHERE AccountId={id}";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {

                   //cmd.ExecuteReader();
                   Int32 count = (Int32)cmd.ExecuteScalar();
                    if (count < 3)
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                    }

                }

                mConnection.Close();
            }
            return found;
           
        }
        public void DeleteBook()
        {
            var x = this.IdBook;

            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"DELETE FROM dbo.books WHERE idBook='{x}';";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {

                    cmd.ExecuteReader();

                }

                mConnection.Close();
            }

        }
    }
}

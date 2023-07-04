using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myLibrary.myLibraryClasses
{
    class accountsClass
    {
        public int IdAccount { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public bool IsAdministrator { get; set; }

        public accountsClass GetUserByEmailAndPassword(string email, string password)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            accountsClass account = null;

            string Command = $"SELECT TOP 1 [idAccount], [Email], [Password], [IsAdministrator] FROM dbo.accounts where Email = '{email}' and Password = '{password}'";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new accountsClass();
                            account.IdAccount = (int)reader[0];
                            account.Email = (string)reader[1];
                            account.Password = (string)reader[2];
                            account.IsAdministrator = (bool)reader[3];
                        }
                    }
                }

                mConnection.Close();
            }

            return account;
        }
        public void AddNewReaderData(string email, string password, string telephone, string name, bool admin)
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"INSERT INTO dbo.accounts ( [Email], [Password], [IsAdministrator],[Name],[Telephone])VALUES('{email}', '{password}', '{admin}','{name}','{telephone}'); ";
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
        public List<accountsClass> GetAllUsers()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            List<accountsClass> accounts = new List<accountsClass>();

            string Command = "SELECT [idAccount], [Name], [Email] FROM dbo.accounts where Email <> 'admin'";
            using (SqlConnection mConnection = new SqlConnection(myconnstring))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new accountsClass();
                            account.IdAccount = (int)reader[0];
                            account.Name = (string)reader[1];
                            account.Email = (string)reader[2];
                           
                            accounts.Add(account);
                        }
                    }
                }

                mConnection.Close();

            }
            return accounts;
        }
        public void DeleteUser()
        {
            var x = this.IdAccount;

            string myconnstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            string Command = $"DELETE FROM dbo.accounts WHERE idAccount='{x}';" ;
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
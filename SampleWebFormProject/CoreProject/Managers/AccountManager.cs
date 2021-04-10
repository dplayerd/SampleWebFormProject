using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Managers
{
    public class AccountManager
    {
        public void CreateAccount()
        { 
        }

        public void UpdateAccount()
        { 
        }

        public void DeleteAccount()
        { 
        }

        public List<AccountModel> GetAccounts()
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" SELECT * FROM Accounts";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<AccountModel> list = new List<AccountModel>();

                    while (reader.Read())
                    {
                        AccountModel model = new AccountModel();
                        model.ID = (Guid)reader["ID"];
                        model.Name = (string)reader["Name"];
                        model.Email = (string)reader["Email"];
                        model.Level = (int)reader["Level"];
                        model.PWD = (string)reader["PWD"];

                        list.Add(model);
                    }

                    reader.Close();

                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public AccountModel GetAccount(string name)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" SELECT * FROM Accounts
                    WHERE Name = @name
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", name);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    AccountModel model = null;

                    while (reader.Read())
                    {
                        model = new AccountModel();
                        model.ID = (Guid)reader["ID"];
                        model.Name = (string)reader["Name"];
                        model.Email = (string)reader["Email"];
                        model.Level = (int)reader["UserLevel"];
                        model.PWD = (string)reader["PWD"];
                    }

                    reader.Close();

                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}

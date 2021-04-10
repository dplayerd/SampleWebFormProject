using CoreProject.Models;
using CoreProject.ViewModels;
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
                    throw;
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
                    throw;
                }
            }
        }


        #region ViewModel
        public List<AccountViewModel> GetAccountViewModels(out int totalSize, int currentPage = 1, int pageSize = 10)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" 
                    SELECT TOP {10} * FROM
                    (
                        SELECT 
                            ROW_NUMBER() OVER(ORDER BY Accounts.ID) AS RowNumber,
                            Accounts.ID,
                            Accounts.Name AS Account,
                            Accounts.UserLevel,
                            AccountInfos.Name,
                            AccountInfos.Title
                        FROM Accounts
                        JOIN AccountInfos
                        ON Accounts.ID = AccountInfos.ID
                    ) AS TempT
                    WHERE RowNumber > {pageSize * (currentPage -1)}
                    ORDER BY ID
                ";

            string countQuery =
                $@" SELECT 
                        COUNT(Accounts.ID)
                    FROM Accounts
                    JOIN AccountInfos
                    ON Accounts.ID = AccountInfos.ID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<AccountViewModel> list = new List<AccountViewModel>();

                    while (reader.Read())
                    {
                        AccountViewModel model = new AccountViewModel();
                        model.ID = (Guid)reader["ID"];
                        model.Name = (string)reader["Name"];
                        model.Title = (string)reader["Title"];
                        model.Account = (string)reader["Account"];
                        model.UserLevel = (int)reader["UserLevel"];

                        list.Add(model);
                    }

                    reader.Close();

                    // 算總數並回傳
                    command.CommandText = countQuery;
                    int? totalSize2 = command.ExecuteScalar() as int?;
                    totalSize = (totalSize2.HasValue) ? totalSize2.Value : 0;

                    return list;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        public AccountViewModel GetAccountViewModel(Guid id)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" SELECT 
                        Accounts.ID,
                        Accounts.Name AS Account,
                        Accounts.UserLevel,
                        Accounts.PWD,
                        Accounts.Email,
                        AccountInfos.Name,
                        AccountInfos.Title,
                        AccountInfos.Phone
                    FROM Accounts
                    JOIN AccountInfos
                        ON Accounts.ID = AccountInfos.ID
                    WHERE Accounts.ID = @id
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    AccountViewModel model = null;

                    while (reader.Read())
                    {
                        model = new AccountViewModel();
                        model.ID = (Guid)reader["ID"];
                        model.Name = (string)reader["Name"];
                        model.Title = (string)reader["Title"];
                        model.Account = (string)reader["Account"];
                        model.UserLevel = (int)reader["UserLevel"];
                        model.PWD = (string)reader["PWD"];
                        model.Email = (string)reader["Email"];
                        model.Phone = (string)reader["Phone"];
                    }

                    reader.Close();

                    return model;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void CreateAccountViewModel(AccountViewModel model)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" INSERT INTO Accounts ( ID, NAME, PWD,UserLevel,Email) VALUES ( @id, @account, @PWD, @UserLevel, @Email);
                    INSERT INTO AccountInfos (ID, NAME, PHONE, TITLE) VALUES (@id, @name, @PHONE, @Title);
                ";

            // Check account is repeated.
            if (this.GetAccount(model.Account) != null)
            {
                throw new Exception($"Account [{model.Account}] has been created.");
            }



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", Guid.NewGuid());

                command.Parameters.AddWithValue("@account", model.Account);
                command.Parameters.AddWithValue("@PWD", model.PWD);
                command.Parameters.AddWithValue("@UserLevel", model.UserLevel);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@PHONE", model.Phone);
                command.Parameters.AddWithValue("@Title", model.Title);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        public void UpdateAccountViewModel(AccountViewModel model)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" UPDATE Accounts SET PWD = @PWD, UserLevel = @UserLevel, Email = @Email  WHERE ID = @id;
                    UPDATE AccountInfos SET NAME = @name, PHONE = @PHONE, TITLE = @Title WHERE ID = @id;
                ";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", model.ID);

                command.Parameters.AddWithValue("@PWD", model.PWD);
                command.Parameters.AddWithValue("@UserLevel", model.UserLevel);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@PHONE", model.Phone);
                command.Parameters.AddWithValue("@Title", model.Title);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void DeleteAccountViewModel(Guid id)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";
            string queryString =
                $@" DELETE AccountInfos WHERE ID = @id;
                    DELETE Accounts WHERE ID = @id;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        #endregion
    }
}

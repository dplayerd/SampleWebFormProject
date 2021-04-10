using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Helpers
{
    public class DBBase
    {
        public DataTable GetDataTable(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);
                command.Parameters.AddRange(parameters.ToArray());

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public object GetScale(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach (var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                command.Parameters.AddRange(parameters2.ToArray());

                try
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int ExecuteNonQuery(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SampleProject; Integrated Security=true";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach (var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                command.Parameters.AddRange(parameters2.ToArray());

                try
                {
                    connection.Open();
                    int totalChange = command.ExecuteNonQuery();
                    return totalChange;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}

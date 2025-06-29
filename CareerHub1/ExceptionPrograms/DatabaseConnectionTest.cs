using System;
using System.Data.SqlClient;
using CareerHub.MyExceptions;

namespace CareerHub.ExceptionPrograms
{
    public class DatabaseConnectionTest
    {
        private string connectionString = "Your Connection String Here"; // Replace this with your string

        public void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    Console.WriteLine("Database connection successful.");

                    string query = "SELECT jobtitle FROM jobs";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("Job Title: " + reader["jobtitle"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw new DatabaseConnectionException("Database connection failed.");
            }
            catch (DatabaseConnectionException ex)
            {
                Console.WriteLine("Custom Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }
    }
}


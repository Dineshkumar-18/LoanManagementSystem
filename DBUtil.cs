using System;

using System.Data.SqlClient;


namespace LoanManagementSystem
{
    public class DBUtil
    {
           private static string connectionString = "Data Source=DESKTOP-F6IQNUS;Initial Catalog=LoanManagementSystem;Integrated Security=True";

        public static SqlConnection GetDBConn()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connected to database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to database: " + ex.Message);
            }
            return connection;
        }
    }
}

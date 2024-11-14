/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    public class ConnectionFile
    {
        public static SqlConnection GetConn()
        {
            try
            {
                // Connection string for the database
                string connectionString = "Data Source=LAPTOP-JOEL\\SQLEXPRESS;Initial Catalog=KaraboGS_2230541;Integrated Security=True;Encrypt=False";
                // Create and return the connection object without opening it
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create connection");
                MessageBox.Show("Error: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
                return null;
            }
        }
    }
}

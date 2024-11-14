/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    internal class Logger
    {
        // for activity logging
        private static readonly string logFilePath = @"..\..\Files\operation_log.txt";
        // for exception logging
        private static readonly string errorFilePath = @"..\..\Files\exception_log.txt";

        // Method to log user actions
        public static void LogUserAction(string username, string action)
        {
            try
            {
                string logEntry = $"{DateTime.Now:G}\t - \tUsername: '{username}'\t {action}";

                // Append the log entry to the file
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging action: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to log user actions
        public static void LogException(Exception error)
        {
            try
            {
                string logEntry = $"{DateTime.Now:G}\t - \tException: '{error}'";

                // Append the log entry to the file
                using (StreamWriter writer = new StreamWriter(errorFilePath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging action: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to read the log file content
        public static string ReadLogFile()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    // Read all text from the file
                    return File.ReadAllText(logFilePath);
                }
                else
                {
                    MessageBox.Show("Log file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // methods to select counts of different tables
        public static int GetUserCount()
        {
            using (var context = new KGSDataClasses1DataContext())
            {
                return context.tblUsers.Count();
            }
        }

        public static int GetStaffCount()
        {
            using (var context = new KGSDataClasses1DataContext())
            {
                return context.tblStaffs.Count();
            }
        }

        public static int GetStudentCount()
        {
            using (var context = new KGSDataClasses1DataContext())
            {
                return context.tblStudents.Count();
            }
        }
    }
}

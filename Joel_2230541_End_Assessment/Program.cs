/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using Joel_2230541_End_Assessment.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show the splash screen as a modal
            using (SplashScreen splash = new SplashScreen())
            {
                splash.ShowDialog();
            }

            while (true)
            {
                using (LoginForm loginForm = new LoginForm())
                {
                    // If login is successful, show the appropriate dashboard
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Form mainForm = null;

                        // Check the user role and open the respective dashboard form
                        if (Person.RoleID == "ADM" || Person.RoleID == "PRN" || Person.RoleID == "DPR")
                        {
                            mainForm = new AdminDash(Person.sessionId);
                        }
                        else if (Person.RoleID == "CLK")
                        {
                            mainForm = new ClerkForm(Person.sessionId);
                        }
                        else if (Person.RoleID == "TCH")
                        {
                            mainForm = new TeacherForm(Person.sessionId);
                        }
                        else if (Person.RoleID == "STD")
                        {
                            mainForm = new StudentForm(Person.sessionId);
                        }

                        // Run the main form if it was set; otherwise break the loop
                        if (mainForm != null)
                        {
                            Application.Run(mainForm);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // If login was canceled or closed, exit the loop
                        break;
                    }
                }
            }

            // Exit the application after the main form is closed
            Application.Exit();

        }

    }
}

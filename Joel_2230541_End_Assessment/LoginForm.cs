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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    public partial class LoginForm : Form
    {
        // Static variable to hold session ID
        public static string sessionId;
        // SQL Connection
        SqlConnection conn = null;
        // sql command instance variable
        SqlCommand cmd = null;
        // reader variable
        SqlDataReader reader = null;
        // To keep track of locked accounts
        bool isLocked = false;
        public LoginForm()
        {
            // get connection
            try
            {
                conn = ConnectionFile.GetConn();
            } catch (Exception ex)
            {
                // log exception to file
                Logger.LogException(ex);
                MessageBox.Show(ex.Message, "Erro Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InitializeComponent();
            this.KeyPreview = true; // Allow the form to capture key presses
        }

        // reset fields method | uses params keyword
        public void Reset(params TextBox[] fields)
        {
            foreach (var field in fields)
            {
                field.Text = "";
            }
        }

        // method for input validation
        public static bool ValidateInput(params TextBox[] fields)
        {
            foreach (var field in fields)
            {
                if (field.Text == "")
                {
                    MessageBox.Show($"Please fill in all fields", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
            }
            return true;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset(txtUsername, txtPassword);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = ValidateInput(txtUsername, txtPassword);
            // if validation is successful
            Person person = new AppUser(0, "", "", "", "", "", DateTime.Now, "", "", "", "");
            if (isValid)
            {
                //login
                bool isAuthenticated = person.Login(txtUsername.Text, txtPassword.Text);
                if (isAuthenticated)
                {
                    // Set the dialog result to OK | Used in Program.cs
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                } else
                {
                    // log context details to file
                    Logger.LogUserAction(txtUsername.Text, "Failed Login Attempt");
                    return;
                }

            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Check if input is valid
                bool isValid = ValidateInput(txtUsername, txtPassword);
                Person person = new AppUser(0, "", "", "", "", "", DateTime.Now, "", "", "", "");

                if (isValid)
                {
                    // Attempt login
                    bool isAuthenticated = person.Login(txtUsername.Text, txtPassword.Text);
                    if (isAuthenticated)
                    {
                        this.DialogResult = DialogResult.OK;  // Redirect to Admin
                        this.Close(); // Close the login form
                    }
                    else
                    {
                        // log context details to file
                        Logger.LogUserAction(txtUsername.Text, "Failed Login Attempt");
                        return;
                    }
                } else
                {
                    // reset password field
                    AdminDash.Reset(txtPassword);
                }
            }
        }

        private void lblVerifyAcc_Click(object sender, EventArgs e)
        {

            VerifyAccount studentAccount = new VerifyAccount();
            this.Hide(); // Hide the LoginForm temporarily

            // Show VerifyAccount as a modal dialog
            studentAccount.ShowDialog();

            // After VerifyAccount closes, show the LoginForm again
            //this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

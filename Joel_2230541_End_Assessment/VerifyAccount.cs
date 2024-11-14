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
    public partial class VerifyAccount : Form
    {
        // connection instance variables
        SqlConnection conn = null;
        SqlDataReader reader = null;

        public VerifyAccount()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
        }

        private void btnConfirmAccount_Click(object sender, EventArgs e)
        {
            bool isValid = LoginForm.ValidateInput(txtStudentID, txtPassword);
            if (isValid)
            {
                DateTime studentDOB = dtpConfirmDOB.Value;
                Clerk person = new Clerk(0, "", "", "", "", "", DateTime.Now, "", "", "", "");
                // call to method for confirming account
                bool isConfirmed = person.AccountVerification(txtStudentID.Text, txtPassword.Text, studentDOB);
            } else
            {
                return;
            }
        }

        
    }
}

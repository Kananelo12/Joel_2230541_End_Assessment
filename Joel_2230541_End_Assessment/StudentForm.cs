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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    public partial class StudentForm : Form
    {
        // Instance variable to store sessionId
        private string userSession;

        // to keep track of current student data
        public int studentId;
        public StudentForm(string sessionId)
        {
            userSession = sessionId;
            InitializeComponent();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            studentId = GetStudentBySession(userSession);
            if (studentId > 0)
            {
                Student.FillMyResults(DGVResults, studentId);
            }
            else
            {
                MessageBox.Show("No student found for the current session.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // log context details to file
            Logger.LogUserAction(userSession, "Student Logged out of system");
            AppUser.Logout(this);
        }


        // get student by their id
        public int GetStudentBySession(string session)
        {
            using (var StudentDataContext = new KGSDataClasses1DataContext())
            {
                var student = StudentDataContext.tblStudents.FirstOrDefault(st => st.Email == session);
                return student?.StudentID ?? 0;
            }
        }

        // method to check if student has oustanding amounts
        public decimal CheckOutstandingFees(int studentId)
        {
            using (var FeesDataContext = new KGSDataClasses1DataContext())
            {
                try
                {
                    // Parse studentId to integer if necessary
                    int id = Convert.ToInt32(studentId);

                    // Query to calculate the total outstanding amount for the specified student
                    var outstandingAmount = FeesDataContext.tblPayments
                        .Where(p => p.StudentID == id)
                        .Sum(p => p.OutstandingAmount);

                    // Return the calculated outstanding amount
                    return (Decimal)outstandingAmount;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // log exception to file
                    Logger.LogException(ex);
                    return 0;
                }
            }
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            Decimal Owing = CheckOutstandingFees(studentId);
            if (Owing > 0)
            {
                MessageBox.Show($"It seems you have an outstanding amount of M{Owing}. Pay your fees to access your results", "Owing Amounts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                pnlStudResults.Visible = true;
            }
        }

        private void btnPayFees_Click(object sender, EventArgs e)
        {
            Decimal Owing = CheckOutstandingFees(studentId);
            if (Owing > 0)
            {
                MessageBox.Show("Your payment will be processed by the School Clerk once available!", "Info Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (Owing == 0)
            {
                MessageBox.Show("You have no outstanding payments!", "Info Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

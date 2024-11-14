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
    public partial class TeacherForm : Form
    {
        private string userSession;
        // keep track of student ID
        int studentID;
        public TeacherForm(string sessionId)
        {
            this.userSession = sessionId;
            InitializeComponent();
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            Teacher.FillResults(DGVResults);
            Teacher.FillTeacherStudents(DGVMyStudents);
        }

        private void DGVMyStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudID.Text = DGVMyStudents.CurrentRow.Cells[0].Value.ToString();
            txtStudID.Enabled = false;
        }

        private void DGVMyStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DGVMyStudents.Rows[e.RowIndex] != null)
            {
                var row = DGVMyStudents.Rows[e.RowIndex];
                txtStudID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtStudID.Enabled = false;
            }
        }

        private void btnCaptureResults_Click(object sender, EventArgs e)
        {
            // validate before inserting
            bool isValid = LoginForm.ValidateInput(txtSubject, txtStudMarks, txtSchoolYear);
            if (isValid)
            {
                // convert mark to integer
                int marks = Convert.ToInt32(txtStudMarks.Text);
                // check for invalid range
                if (marks < 0 || marks > 100)
                {
                    MessageBox.Show("Invalid Mark range, Try Again!", "Invalid Marks", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                    {
                        tblResult results = new tblResult()
                        {
                            StudentID = Convert.ToInt32(txtStudID.Text),
                            Subjects = txtSubject.Text,
                            Marks = Convert.ToInt32(txtStudMarks.Text),
                            Symbol = Teacher.CalculateGradeSymbol(marks),
                            Term = cmbTerms.Text,
                            SchoolYear = Convert.ToInt32(txtSchoolYear.Text)
                        };
                        data.tblResults.InsertOnSubmit(results);
                        data.SubmitChanges();
                        // log context details to file
                        Logger.LogUserAction(userSession, "Captured Student Marks");
                        MessageBox.Show($"Student marks captured successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch (Exception ex)
                {
                    // log exception to file
                    Logger.LogException(ex);
                    MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshResults_Click(object sender, EventArgs e)
        {
            Teacher.FillResults(DGVResults);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void btnClearF_Click(object sender, EventArgs e)
        {
            // clear all fields
            AdminDash.Reset(txtStudID, txtSubject, txtStudMarks, txtSchoolYear);
            cmbTerms.SelectedIndex = -1;
            cmbTerms.Text = "Select Term...";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // log context details to file
            Logger.LogUserAction(userSession, "User Logged out of system");
            AppUser.Logout(this);
        }
    }
}

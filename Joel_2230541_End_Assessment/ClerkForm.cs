/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using Joel_2230541_End_Assessment.KaraboGS_2230541DataSetTableAdapters;
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
    public partial class ClerkForm : Form
    {
        // Instance variable to store sessionId
        private string userSession;
        // keep track of student ID
        int studentID;

        // to keek track of oustanding amount
        protected double Owing = 0.0;
        public ClerkForm(string sessionId)
        {
            // Store the sessionId
            this.userSession = sessionId;
            InitializeComponent();
        }

        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            RegStudent regStudent = new RegStudent(userSession);
            regStudent.Show();
            this.Visible = false;
        }

        private void btnModifyStudent_Click(object sender, EventArgs e)
        {
            // validate before updating
            bool isValid = LoginForm.ValidateInput(txtStudFname, txtStudLname, txtStudPnum, txtStudHaddress);
            if (isValid)
            {
                // update table fields
                using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                {
                    tblStudent student = data.tblStudents.FirstOrDefault(s => s.StudentID == studentID);
                    if (student == null)
                    {
                        MessageBox.Show($"Student with ID: {studentID} not found in the databse");
                        return;
                    }

                    // else update all the new student records
                    student.FirstName = txtStudFname.Text;
                    student.LastName = txtStudLname.Text;
                    student.PhoneNumber = txtStudPnum.Text;
                    student.HomeAddress = txtStudHaddress.Text;
                    student.GradeLevel = cmbStudGradelvl.Text;

                    // submit changes to the database
                    data.SubmitChanges();
                    MessageBox.Show("Student Record Updated Successfully");
                    Teacher.FillDGVStudents(DGVStudents);
                    // log context details to file
                    Logger.LogUserAction(userSession, "Updated Student Records");
                }
            }
            else
            {
                return;
            }
        }

        private void ClerkForm_Load(object sender, EventArgs e)
        {
            Teacher.FillDGVStudents(DGVStudents);
            Clerk.FillOwingStudents(DGVOwingStuds);
            Clerk.FillPayments(DGVPayments);

            // show current cashier for payment processing
            txtCashierID.Text = userSession;
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();

            // load report
            LoadReport();

        }

        private void btnClearStud_Click(object sender, EventArgs e)
        {
            AdminDash.Reset(txtStudFname, txtStudLname, txtStudPnum, txtStudPnum, txtStudHaddress);
            cmbStudGradelvl.SelectedIndex = -1;
            cmbStudGradelvl.Text = "Select grade...";
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
            {
                if (DGVStudents.CurrentRow != null)
                {
                    tblStudent student = data.tblStudents.FirstOrDefault(s => s.StudentID == studentID);
                    try
                    {
                        // Set the DeletedBy user in the UserSession table
                        data.ExecuteCommand("INSERT INTO UserSession (UserID) VALUES ({0})", userSession);

                        // delete student record
                        data.tblStudents.DeleteOnSubmit(student);
                        data.SubmitChanges();
                        MessageBox.Show("Student Record deleted successfully", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // refresh table
                        Teacher.FillDGVStudents(DGVStudents);
                        // log context details to file
                        Logger.LogUserAction(userSession, "Deleted Student Record");
                    }
                    catch (Exception ex)
                    {
                        // log exception to file
                        Logger.LogException(ex);
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a record to delete!", "Empty Delete Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
        }

        private void DGVStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DGVStudents.Rows[e.RowIndex] != null)
            {
                var row = DGVStudents.Rows[e.RowIndex];
                studentID = Convert.ToInt32(row.Cells[0].Value?.ToString() ?? string.Empty);
                txtStudFname.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtStudLname.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                txtStudPnum.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
                txtStudHaddress.Text = row.Cells[6].Value?.ToString() ?? string.Empty;
                cmbStudGradelvl.Text = row.Cells[7].Value?.ToString() ?? string.Empty;
            }
        }

        private void DGVStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            studentID = Convert.ToInt32(DGVStudents.CurrentRow.Cells[0].Value.ToString());
            txtStudFname.Text = DGVStudents.CurrentRow.Cells[1].Value.ToString();
            txtStudLname.Text = DGVStudents.CurrentRow.Cells[2].Value.ToString();
            txtStudPnum.Text = DGVStudents.CurrentRow.Cells[5].Value.ToString();
            txtStudHaddress.Text = DGVStudents.CurrentRow.Cells[6].Value.ToString();
            cmbStudGradelvl.Text = DGVStudents.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // log context details to file
            Logger.LogUserAction(userSession, "User Logged out of system");
            AppUser.Logout(this);
        }

        private void DGVOwingStuds_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudID.Text = DGVOwingStuds.CurrentRow.Cells[0].Value.ToString();
            txtStudID.Enabled = false;
        }

        private void DGVOwingStuds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DGVOwingStuds.Rows[e.RowIndex] != null)
            {
                var row = DGVOwingStuds.Rows[e.RowIndex];
                txtStudID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtStudID.Enabled = false;
            }
        }

        private void btnClearF_Click(object sender, EventArgs e)
        {
            
        }

        // get cashier id from user session
        public int GetCashierID(string session)
        {
            using (var SessionDataContext = new KGSDataClasses1DataContext())
            {
                var cashier = SessionDataContext.tblStaffs.FirstOrDefault(s => s.Email == session);
                return cashier?.StaffID ?? 0;
            }
        }

        // Generate a unique receipt number using date and time with a random suffix
        public string GenReceiptNo()
        {
            
            string datePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomPart = new Random().Next(1000, 9999).ToString();
            return $"RCPT-{datePart}-{randomPart}"; // Combine them with a prefix
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Clerk.FillPayments(DGVPayments);
        }

        private void btnCaptureFees_Click(object sender, EventArgs e)
        {
            if (DGVOwingStuds.CurrentRow != null)
            {
                // validate before capturing payments
                bool isValid = LoginForm.ValidateInput(txtStudID, txtAmount, txtCashierID);
                if (isValid)
                {
                    decimal amount = Convert.ToDecimal(txtAmount.Text);
                    // check for cashier ID
                    int CashierID = GetCashierID(userSession);
                    if (CashierID > 0)
                    {
                        decimal defaultAmount = 2000;
                        try
                        {
                            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                            {
                                tblPayment payments = new tblPayment()
                                {
                                    StudentID = Convert.ToInt32(txtStudID.Text),
                                    AmountPaid = amount,
                                    OutstandingAmount = (defaultAmount - amount),
                                    PaymentDate = DateTime.Now,
                                    CashierID = CashierID,
                                    ReceiptNO = GenReceiptNo()
                                };
                                data.tblPayments.InsertOnSubmit(payments);
                                data.SubmitChanges();
                                MessageBox.Show("Payment captured Successfully!", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // log context details to file
                                Logger.LogUserAction(userSession, "Captured Student Payment");
                                // Generate PDF receipt
                                Clerk.GeneratePDFReceipt(payments);
                            }
                        }
                        catch (Exception ex)
                        {
                            // log exception to file
                            Logger.LogException(ex);
                            MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving the cashier ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void LoadReport()
        {
            // Create an instance of the dataset
            KaraboGS_2230541DataSet dataSet = new KaraboGS_2230541DataSet();

            // Create an instance of the table adapter
            var resultTableAdapter = new KaraboGS_2230541DataSetTableAdapters.tblResultTableAdapter();
            resultTableAdapter.Fill(dataSet.tblResult);

            //load chart data
            var ChartTableAdapter = new KaraboGS_2230541DataSetTableAdapters.GetMostPassedSubjects1TableAdapter();
            ChartTableAdapter.Fill(dataSet.GetMostPassedSubjects1);

            // Set the embedded resource for the report
            reportViewer1.LocalReport.ReportEmbeddedResource = "Joel_2230541_End_Assessment.ReportCard.rdlc";

            // Clear previous data sources to avoid conflicts
            reportViewer1.LocalReport.DataSources.Clear();

            // Add the data source, making sure the name matches the RDLC report data source
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ReportDataSet", (DataTable)dataSet.tblResult));

            // add the data source for the chart
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("GetMostPassedSubjects1", (DataTable)dataSet.GetMostPassedSubjects1));

            // Refresh the ReportViewer to render the report
            reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[3];
        }

        private void btnPrintFees_Click(object sender, EventArgs e)
        {
            FeesStatement.GenerateFeeStatement();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[3];
        }
    }
}

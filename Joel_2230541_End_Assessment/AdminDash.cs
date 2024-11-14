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
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
namespace Joel_2230541_End_Assessment
{
    public partial class AdminDash : Form
    {
        // Instance variable to store sessionId
        private string userSession;
        bool sidebarExpand;

        // variables to help in parsing combo boxes
        private string Roleid = null;
        private string qualification = null;

        // keep track of student ID
        int studentID;
        public AdminDash(string sessionId)
        {
            // Store the sessionId
            this.userSession = sessionId;
            InitializeComponent();
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                // If expanded, minimize
                pnlSideMenu.Width -= 10;
                if (pnlSideMenu.Width <= pnlSideMenu.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                // If minimized, expand
                pnlSideMenu.Width += 10;
                if (pnlSideMenu.Width >= pnlSideMenu.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void AdminDash_Load(object sender, EventArgs e)
        {
            lblUserSession.Text = userSession;
            // refresh tables upon opening the form
            FillDGV(DGVStaff);
            FillUsersDGV(DGVUsers);
            Teacher.FillDGVStudents(DGVStudents);
            Person.FillDeletedStaffDGV(DGVDelStaff);
            Person.FillDeletedStudentsDGV(DGVDelStudents);
            Teacher.FillResults(DGVResults);


            // Load counts when the form loads
            LoadCounts();

            // loading chart data
            LoadRoleChart();

            // load report data
            LoadReport();
        }
        private void LoadRoleChart()
        {
            // Clear existing data points (in case the chart has old data)
            RolesChart.Series["Roles"].Points.Clear();

            // Get the role counts from the database
            var roleCounts = Principal.GetRoleCounts();

            // Populate the chart with the role data
            foreach (var role in roleCounts)
            {
                RolesChart.Series["Roles"].Points.AddXY(role.Key, role.Value);
            }
        }

        // bind labels with select statements
        private void LoadCounts()
        {
            lblAllUsers.Text = Logger.GetUserCount().ToString() + " System Users";
            lblStaff.Text = Logger.GetStaffCount().ToString() + " Staff";
            lblStudents.Text = Logger.GetStudentCount().ToString() + " Students";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }

        private void btnAllUsers_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[3];
        }

        private void btnUserlogs_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[4];
        }

        // Staff Table
        public void FillDGV(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from staff in data.tblStaffs
                          select new
                          { 
                              staff.StaffID,
                              staff.FirstName,
                              staff.LastName,
                              staff.PhoneNumber,
                              staff.Email,
                              staff.PhysicalAddress,
                              staff.RoleID,
                              staff.QualificationID
                          }).ToList();
            dgv.DataSource = result;
        }

        // Users Table
        public void FillUsersDGV(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from user in data.tblUsers
                          select new
                          {
                              user.UserID,
                              user.Username,
                              user.Password,
                              user.RoleID,
                              user.StaffID,
                              user.StudentID,
                              user.isLocked
                          }).ToList();
            dgv.DataSource = result;
        }

        

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // log context details to file
            Logger.LogUserAction(userSession, "User Logged out of system");
            // call logout method
            AppUser.Logout(this);
        }

        private void btnStaffClear_Click(object sender, EventArgs e)
        {
            Reset(txtStaffID, txtStaffName, txtStaffSurname, txtStaffPNo, txtStaffEmail, txtStaffAddress);
            cmbRole.SelectedIndex = -1; // Deselects any selected item in ComboBox
            cmbRole.Text = "Select option..."; // Sets the display text to "Select option"
            cmbQualifications.SelectedIndex = -1;
            cmbQualifications.Text = "Select option...";
        }
        // method to clear text boxes
        public static void Reset(params TextBox[] fields)
        {
            foreach (var field in fields)
            {
                field.Text = "";
            }
        }

        private void btnStaffInsert_Click(object sender, EventArgs e)
        {
            // validate before insertion
            bool isValid = LoginForm.ValidateInput(txtStaffName, txtStaffSurname, txtStaffPNo, txtStaffEmail);
            if (isValid)
            {
                Dictionary<string, string> staffFields = new Dictionary<string, string>
                {
                    //{ "StaffID", txtStaffID.Text }, //auto increments
                    { "FirstName", txtStaffName.Text },
                    { "LastName", txtStaffSurname.Text },
                    { "PhoneNumber", txtStaffPNo.Text },
                    { "Email", txtStaffEmail.Text },
                    { "PhysicalAddress", txtStaffAddress.Text },
                    { "RoleID", ParseRoleID(cmbRole) },
                    { "QualificationID", ParseQualificationID(cmbQualifications) }
                };

                InsertRecord<tblStaff>(staffFields);
                FillDGV(DGVStaff);
                // log context details to file
                Logger.LogUserAction(userSession, "Created New Staff Record");

                // automatically create account for user after registration
                DialogResult createAccount = MessageBox.Show(
                    $"Would you like to automatically create an account for {txtStaffName.Text} {txtStaffSurname.Text} ?\n",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (createAccount == DialogResult.Yes)
                {
                    // get the StaffID for the newly registered user by their email
                    int newStaffID = GetStaffIDByEmail(txtStaffEmail.Text);

                    if (newStaffID > 0)
                    {
                        // setting the default password to name.surname@123
                        string defaultPassword = $"{txtStaffName.Text}.{txtStaffSurname.Text}@123";

                        Dictionary<string, object> usersFields = new Dictionary<string, object>
                        {
                            {"Username", txtStaffEmail.Text},
                            {"Password", defaultPassword.ToLower()},
                            {"RoleID", ParseRoleID(cmbRole)},
                            {"StaffID", newStaffID},
                            {"StudentID", null},
                            {"isLocked", false}
                        };

                        Principal.CreateUserAccount<tblUser>(usersFields);
                        // refresh table
                        FillUsersDGV(DGVUsers);
                    } else
                    {
                        MessageBox.Show("Error retrieving StaffID for the new staff member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            } else
            {
                return;
            }
        }

        // Helper function to fetch StaffID by email
        private int GetStaffIDByEmail(string email)
        {
            using (var EmailDataContext = new KGSDataClasses1DataContext())
            {
                var staff = EmailDataContext.tblStaffs.FirstOrDefault(s => s.Email == email);
                return staff?.StaffID ?? 0;
            }
        }

        // parse RoleID and QualificationID
        public string ParseRoleID(System.Windows.Forms.ComboBox comboBox)
        {
            // Define the role mappings
            var roleMappings = new Dictionary<string, string>
            {
                { "Administrator", "ADM" },
                { "Principal", "PRN" },
                { "Deputy Principal", "DPR" },
                { "Clerk", "CLK" },
                { "Teacher", "TCH" }
            };

            // Check if a role is selected
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role.");
                return null;
            }

            // Retrieve the selected role from ComboBox
            string selectedRole = comboBox.SelectedItem.ToString();

            // Try to get the corresponding Role ID from the dictionary
            if (roleMappings.TryGetValue(selectedRole, out string roleID))
            {
                Roleid = roleID;
                return Roleid;
            }
            else
            {
                MessageBox.Show("Selected role does not have a defined Role ID.");
                return null;
            }
        }

        public string ParseQualificationID(System.Windows.Forms.ComboBox comboBox)
        {
            var qualificationMappings = new Dictionary<string, string>
            {
                { "Doctor of Philosophy", "PhD" },
                { "Master of Science", "MSc" },
                { "Bachelor of Science" , "BSc" },
                { "Certificate" , "Cert" },
                { "Student" , "STQ" }
            };

            // Check if a qualification is selected
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a qualification.");
                return null;
            }

            // Retrieve the selected qualification from ComboBox
            string selectedQualification = comboBox.SelectedItem.ToString();

            // Try to get the corresponding qualification description from the dictionary
            if (qualificationMappings.TryGetValue(selectedQualification, out string qualificationDescription))
            {
                qualification = qualificationDescription;
                return qualification;
            }
            else
            {
                MessageBox.Show("Selected qualification does not have a defined description.");
                return null;
            }
        }

        // Staff method to insert data using LINQ
        public static void InsertRecord<T>(Dictionary<string, string> fieldValues) where T : class, new()
        {
            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
            {
                // Create a new instance of the generic type
                T record = new T();

                // Use reflection to set the properties
                foreach (var fieldValue in fieldValues)
                {
                    var property = typeof(T).GetProperty(fieldValue.Key);
                    if (property != null && property.CanWrite)
                    {
                        // Convert the string to the property type if needed, e.g., int, DateTime, etc.
                        object value = Convert.ChangeType(fieldValue.Value, property.PropertyType);
                        property.SetValue(record, value);
                    }
                }

                // Insert the record into the database
                data.GetTable<T>().InsertOnSubmit(record);
                data.SubmitChanges();
                MessageBox.Show("Record Inserted Successfully");
            }
        }

        private void DGVStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStaffID.Text = DGVStaff.CurrentRow.Cells[0].Value.ToString();
            txtStaffID.Enabled = false;
            txtStaffName.Text = DGVStaff.CurrentRow.Cells[1].Value.ToString();
            txtStaffSurname.Text = DGVStaff.CurrentRow.Cells[2].Value.ToString();
            txtStaffPNo.Text = DGVStaff.CurrentRow.Cells[3].Value.ToString();
            txtStaffEmail.Text = DGVStaff.CurrentRow.Cells[4].Value.ToString();
            txtStaffAddress.Text = DGVStaff.CurrentRow.Cells[5].Value.ToString();
        }

        private void DGVStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DGVStaff.Rows[e.RowIndex] != null)
            {
                var row = DGVStaff.Rows[e.RowIndex];
                txtStaffID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtStaffID.Enabled = false;
                txtStaffName.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtStaffSurname.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                txtStaffPNo.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
                txtStaffEmail.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
                txtStaffAddress.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
            }
        }

        private void btnStaffUpdate_Click(object sender, EventArgs e)
        {
            // Validate before update
            bool isValid = LoginForm.ValidateInput(txtStaffName, txtStaffSurname, txtStaffPNo, txtStaffEmail, txtStaffAddress);
            if (isValid)
            {
                using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                {
                    int staffId;
                    if (!int.TryParse(txtStaffID.Text, out staffId))
                    {
                        MessageBox.Show("Invalid Staff ID.");
                        return;
                    }

                    tblStaff staff = data.tblStaffs.FirstOrDefault(staff1 => staff1.StaffID == staffId);
                    if (staff == null)
                    {
                        MessageBox.Show("Staff member not found.");
                        return;
                    }

                    // Update staff information
                    staff.FirstName = txtStaffName.Text;
                    staff.LastName = txtStaffSurname.Text;
                    staff.PhoneNumber = txtStaffPNo.Text;
                    staff.Email = txtStaffEmail.Text;
                    staff.PhysicalAddress = txtStaffAddress.Text;

                    // Submit changes to the database
                    data.SubmitChanges();
                    MessageBox.Show("Record Updated Successfully");
                    FillDGV(DGVStaff);
                    // log context details to file
                    Logger.LogUserAction(userSession, "Updated Staff Record");
                }
            }
            else
            {
                return;
            }
        }

        private void btnStaffDelete_Click(object sender, EventArgs e)
        {
            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
            {
                if (DGVStaff.CurrentRow != null)
                {
                    int staffId;
                    if (!int.TryParse(txtStaffID.Text, out staffId))
                    {
                        MessageBox.Show("Invalid Staff ID.");
                        return;
                    }
                    tblStaff staff = data.tblStaffs.FirstOrDefault(staff1 => staff1.StaffID == staffId);
                    data.tblStaffs.DeleteOnSubmit(staff);
                    data.SubmitChanges();
                    MessageBox.Show("Record deleted successfully", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // refresh table
                    FillDGV(DGVStaff);
                    // log context details to file
                    Logger.LogUserAction(userSession, "Deleted Staff Record");
                } else
                {
                    MessageBox.Show("Please select a record to delete!", "Empty Delete Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
            {
                if (DGVUsers.CurrentRow != null)
                {
                    int userId;
                    if (!int.TryParse(txtUserID.Text, out userId))
                    {
                        MessageBox.Show("Invalid User ID.");
                        return;
                    }
                    tblUser user = data.tblUsers.FirstOrDefault(user1 => user1.UserID == userId);
                    data.tblUsers.DeleteOnSubmit(user);
                    data.SubmitChanges();
                    MessageBox.Show("Record deleted successfully", "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // refresh table
                    FillUsersDGV(DGVUsers);
                    // log context details to file
                    Logger.LogUserAction(userSession, "Deleted User Account");
                }
                else
                {
                    MessageBox.Show("Please select a record to delete!", "Empty Delete Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            // Validate before update
            bool isValid = LoginForm.ValidateInput(txtUsername, txtUserPwd);
            if (isValid)
            {
                try // handling unexpected exceptions
                {
                    using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                    {
                        int userId;
                        if (!int.TryParse(txtUserID.Text, out userId))
                        {
                            MessageBox.Show("Invalid User ID.");
                            return;
                        }

                        tblUser user = data.tblUsers.FirstOrDefault(user1 => user1.UserID == userId);
                        if (user == null)
                        {
                            MessageBox.Show("User record not found.");
                            return;
                        }

                        // Update user information
                        user.Username = txtUsername.Text;
                        user.Password = txtUserPwd.Text;
                        user.RoleID = SetUserRole(cmbUserRole.Text);
                        user.isLocked = SetAccountStatus(cmbAccountStatus.Text);

                        data.SubmitChanges();
                        MessageBox.Show("User Updated Successfully");
                        FillUsersDGV(DGVUsers);
                        // log context details to file
                        Logger.LogUserAction(userSession, "Modified User Account");
                    }
                } catch (ArgumentException ex)
                {
                    // log exception to file
                    Logger.LogException(ex);
                    MessageBox.Show(ex.Message);
                    return;
                } catch (Exception ex)
                {
                    // log exception to file
                    Logger.LogException(ex);
                    MessageBox.Show("Error: " + ex, "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void btnResetTxts_Click(object sender, EventArgs e)
        {
            Reset(txtUsername, txtUserPwd);
        }

        private void DGVUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserID.Text = DGVUsers.CurrentRow.Cells[0].Value.ToString();
            txtUserID.Enabled = false;
            txtUsername.Text = DGVUsers.CurrentRow.Cells[1].Value.ToString();
            txtUserPwd.Text = DGVUsers.CurrentRow.Cells[2].Value.ToString();
        }

        private void DGVUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DGVUsers.Rows[e.RowIndex] != null)
            {
                var row = DGVUsers.Rows[e.RowIndex];
                txtUserID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtUserID.Enabled = false;
                txtUsername.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtUserPwd.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
            }
        }

        private void btnRegisterStudent_Click(object sender, EventArgs e)
        {
            
            RegStudent regStudent = new RegStudent(userSession);
            regStudent.Show();
            this.Visible = false;
        }

        // Method to get selected role
        public string SetUserRole(string role)
        {
            if (role == "Administrator")
            {
                return "ADM";
            }
            else if (role == "Principal")
            {
                return "PRN";
            }
            else if (role == "Deputy Principal")
            {
                return "DPR";
            }
            else if (role == "Teacher")
            {
                return "TCH";
            }
            else if (role == "Clerk")
            {
                return "CLK";
            }
            else if (role == "Student")
            {
                return "STD";
            }
            else
            {
                throw new ArgumentException("Invalid role");
            }
        }


        // set account status | method that throws exceptions
        public bool SetAccountStatus(string accountStatus)
        {
            if (accountStatus == "Open")
            {
                return false;
            } else if (accountStatus == "Locked") {
                return true;
            } else
            {
                throw new ArgumentException("Invalid account status");
            }
        }

        private void btnClearStud_Click(object sender, EventArgs e)
        {
            Reset(txtStudFname, txtStudLname, txtStudPnum, txtStudPnum, txtStudHaddress);
            cmbStudGradelvl.SelectedIndex = -1;
            cmbStudGradelvl.Text = "Select grade...";
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
                    Logger.LogUserAction(userSession, "Modified Student Record");
                }
            } else
            {
                return;
            }
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
                    } catch (Exception ex)
                    {
                        // log exception to file
                        Logger.LogException(ex);
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else
                {
                    MessageBox.Show("Please select a record to delete!", "Empty Delete Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            FillUsersDGV(DGVUsers);
        }

        private void btnUserlogs_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[5];
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            rtbUserLogs.Text = Logger.ReadLogFile();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[4];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[6];
        }

        // binding report data
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

        private void btnDelStaff_Click(object sender, EventArgs e)
        {
            // Hide student details
            pnlDeletedStudents.Visible = false;
            DGVDelStudents.Visible = false;

            // Show staff details
            pnlDeletedStaff.Visible = true;
            DGVDelStaff.Visible = true;
        }

        private void btnDelStudents_Click(object sender, EventArgs e)
        {
            // Hide staff details
            pnlDeletedStaff.Visible = false;
            DGVDelStaff.Visible = false;

            // Show student details
            pnlDeletedStudents.Visible = true;
            DGVDelStudents.Visible = true;
        }

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            // refresh tables
            Person.FillDeletedStaffDGV(DGVDelStaff);
            Person.FillDeletedStudentsDGV(DGVDelStudents);
        }

        private void DGVDelStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnRestoreStudent_Click(object sender, EventArgs e)
        {
            if (DGVDelStudents.CurrentRow != null)
            {
                Dictionary<string, object> studentFields = new Dictionary<string, object>
                {
                    { "FirstName", DGVDelStudents.CurrentRow.Cells[2].Value.ToString() },
                    { "LastName", DGVDelStudents.CurrentRow.Cells[3].Value.ToString() },
                    { "Email", DGVDelStudents.CurrentRow.Cells[4].Value.ToString() },
                    { "DateOfBirth", Convert.ToDateTime(DGVDelStudents.CurrentRow.Cells[5].Value.ToString()) },
                    { "Gender", DGVDelStudents.CurrentRow.Cells[6].Value.ToString() },
                    { "PhoneNumber", DGVDelStudents.CurrentRow.Cells[7].Value.ToString() },
                    { "HomeAddress", DGVDelStudents.CurrentRow.Cells[8].Value.ToString() },
                    { "GradeLevel", DGVDelStudents.CurrentRow.Cells[9].Value.ToString() },
                    { "JoinDate", Convert.ToDateTime(DGVDelStudents.CurrentRow.Cells[10].Value.ToString()) },
                    { "GuardianID", Convert.ToInt32(DGVDelStudents.CurrentRow.Cells[11].Value.ToString()) },
                    { "TeacherID", Convert.ToInt32(DGVDelStudents.CurrentRow.Cells[12].Value.ToString()) }
                };

                Student.InsertStudents<tblStudent>(studentFields);
                // refresh table
                Teacher.FillDGVStudents(DGVStudents);
                FillUsersDGV(DGVUsers);
                // log context details to file
                Logger.LogUserAction(userSession, "Restored Student Record Successfully");

                // Get first and last name of student
                string Fname = DGVDelStudents.CurrentRow.Cells[2].Value.ToString();
                string Lname = DGVDelStudents.CurrentRow.Cells[3].Value.ToString();

                using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                {
                    tblStudentArchive deleteStudent = data.tblStudentArchives.FirstOrDefault(s => s.FirstName == Fname && s.LastName == Lname);
                    try
                    {

                        // delete record from archive table
                        data.tblStudentArchives.DeleteOnSubmit(deleteStudent);
                        data.SubmitChanges();
                        // refresh table
                        Person.FillDeletedStudentsDGV(DGVDelStudents);
                    }
                    catch (Exception ex)
                    {
                        // log exception to file
                        Logger.LogException(ex);
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                // automatically create account for user after registration
                DialogResult createAccount = MessageBox.Show(
                    $"Would you like to automatically create a login account for {Fname} {Lname} ?\n",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (createAccount == DialogResult.Yes)
                {
                    // get the Student ID for the newly registered student
                    int newStudentID = RegStudent.GetStudentIDByNames(Fname, Lname);

                    if (newStudentID > 0)
                    {
                        // set email to name.surname@kgs.com
                        string studEmail = $"{Fname}.{Lname}.kgs.com";
                        // setting the default password to name.surname@123
                        string defaultPassword = $"{Fname}.{Lname}@123";

                        Dictionary<string, object> usersFields = new Dictionary<string, object>
                        {
                            {"Username", studEmail.ToLower() },
                            {"Password", defaultPassword.ToLower() },
                            {"RoleID", "STD" },
                            {"StaffID", null },
                            {"StudentID", newStudentID },
                            {"isLocked", true }
                        };

                        Principal.CreateUserAccount<tblUser>(usersFields);
                        // log context details to file
                        Logger.LogUserAction(userSession, $"Created a User Account for {Fname} {Lname}");
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving Student ID for the just restored student", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            } else
            {
                MessageBox.Show("Please select a student to restore.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRestoreStaff_Click(object sender, EventArgs e)
        {
            if (DGVDelStaff.CurrentRow != null)
            {
                Dictionary<string, string> staffFields = new Dictionary<string, string>
                {
                    { "FirstName", DGVDelStaff.CurrentRow.Cells[2].Value.ToString() },
                    { "LastName", DGVDelStaff.CurrentRow.Cells[3].Value.ToString() },
                    { "PhoneNumber", DGVDelStaff.CurrentRow.Cells[4].Value.ToString() },
                    { "Email", DGVDelStaff.CurrentRow.Cells[5].Value.ToString() },
                    { "PhysicalAddress", DGVDelStaff.CurrentRow.Cells[6].Value.ToString() },
                    { "RoleID", DGVDelStaff.CurrentRow.Cells[7].Value.ToString() },
                    { "QualificationID", DGVDelStaff.CurrentRow.Cells[8].Value.ToString() }
                };

                InsertRecord<tblStaff>(staffFields);
                // refresh table
                FillDGV(DGVStaff);
                // log context details to file
                Logger.LogUserAction(userSession, "Restored Staff Record Successfully");

                // Get first and last name of student
                string Fname = DGVDelStaff.CurrentRow.Cells[2].Value.ToString();
                string Lname = DGVDelStaff.CurrentRow.Cells[3].Value.ToString();
                string StaffRoleID = DGVDelStaff.CurrentRow.Cells[7].Value.ToString();

                using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                {
                    tblStaffArchive deleteStaff = data .tblStaffArchives.FirstOrDefault(s => s.FirstName.Equals(Fname) && s.LastName.Equals(Lname));
                    try
                    {

                        // delete record from archive table
                        data.tblStaffArchives.DeleteOnSubmit(deleteStaff);
                        data.SubmitChanges();
                        // refresh table
                        Person.FillDeletedStaffDGV(DGVDelStaff);
                    }
                    catch (Exception ex)
                    {
                        // log exception to file
                        Logger.LogException(ex);
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                // automatically create account for user after registration
                DialogResult createAccount = MessageBox.Show(
                    $"Would you like to automatically create a login account for {Fname} {Lname} ?\n",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (createAccount == DialogResult.Yes)
                {
                    // get the Student ID for the newly registered student
                    int newStaffID = Principal.GetStaffIDByNames(Fname, Lname);

                    if (newStaffID > 0)
                    {
                        // set email to name.surname@kgs.com
                        string staffEmail = $"{Fname}.{Lname}.kgs.com";
                        // setting the default password to name.surname@123
                        string defaultPassword = $"{Fname}.{Lname}@123";

                        Dictionary<string, object> usersFields = new Dictionary<string, object>
                        {
                            {"Username", staffEmail.ToLower() },
                            {"Password", defaultPassword.ToLower() },
                            {"RoleID", StaffRoleID },
                            {"StaffID", newStaffID },
                            {"StudentID", null },
                            {"isLocked", false }
                        };

                        Principal.CreateUserAccount<tblUser>(usersFields);
                        // log context details to file
                        Logger.LogUserAction(userSession, $"Created a User Account for {Fname} {Lname}");
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving StaffID for the new staff member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a student to restore.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

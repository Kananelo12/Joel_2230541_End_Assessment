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
    public partial class RegStudent : Form
    {
        // Instance variable to store sessionId
        private string userSession;
        public RegStudent(string sessionId)
        {
            this.userSession = sessionId;
            InitializeComponent();
        }

        private void btnResetFields_Click(object sender, EventArgs e)
        {
            AdminDash.Reset(txtFname, txtSname, txtPhoneNO, txtAddress, txtGuardian);
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            cmbGradeLevel.SelectedIndex = -1;
            cmbGradeLevel.Text = "Select level...";
            dtpDOB.Value = DateTime.Now;
            dtpJoinDate.Value = DateTime.Now;
        }

        // method to set gender
        public string SetGender(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
            {
                return "Male";
            } else if (rdb2.Checked)
            {
                return "Female";
            }
            return null; // Default value if neither radio button is checked
        }

        private void btnRegUser_Click(object sender, EventArgs e)
        {
            // validate before insertion
            bool isValid = LoginForm.ValidateInput(txtFname, txtSname, txtPhoneNO, txtAddress, txtGuardian);
            if (isValid)
            {
                // Split guardian's name from txtGuardian
                string[] guardianNames = txtGuardian.Text.Split(' ');
                string GName = guardianNames.Length > 0 ? guardianNames[0] : "";
                string GSurname = guardianNames.Length > 1 ? guardianNames[1] : "";

                // Get GuardianID for the registered user based on GName and GSurname
                int GuardianID = GetGuardianByNames(GName, GSurname);

                // Split guardian's name from txtGuardian
                string[] teacherNames = txtTeacherNames.Text.Split(' ');
                string TName = teacherNames.Length > 0 ? teacherNames[0] : "";
                string TSurname = teacherNames.Length > 1 ? teacherNames[1] : "";

                // Get TeacherID for the registered user based on firstname and surname
                int TeacherID = GetTeacherByNames(TName, TSurname);
                MessageBox.Show("Teacher ID: " + TeacherID);

                // validate age before insertion
                DateTime BirthDate = dtpDOB.Value;
                string message = Student.CheckStudentAge(BirthDate);
                if (message != null)
                {
                    MessageBox.Show(message);
                    return;
                }

                if (TeacherID == 0)
                {
                    MessageBox.Show("The Specified Teacher does not exist in our database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (GuardianID > 0 && message==null)
                {
                    Dictionary<string, object> studentFields = new Dictionary<string, object>
                    {
                        { "FirstName", txtFname.Text },
                        { "LastName", txtSname.Text },
                        { "Email", txtStudEmail.Text },
                        { "DateOfBirth", dtpDOB.Value.ToString("yyyy-MM-dd") },
                        { "Gender", SetGender(rdbMale, rdbFemale) },
                        { "PhoneNumber", txtPhoneNO.Text },
                        { "HomeAddress", txtAddress.Text },
                        { "GradeLevel", cmbGradeLevel.SelectedItem?.ToString() ?? "" },
                        { "JoinDate", dtpJoinDate.Value.ToString("yyyy-MM-dd") },
                        { "GuardianID", GuardianID },
                        { "TeacherID", TeacherID }
                    };

                    Student.InsertStudents<tblStudent>(studentFields);
                    // log context details to file
                    Logger.LogUserAction(userSession, "Created Student Record");

                    // automatically create account for user after registration
                    DialogResult createAccount = MessageBox.Show(
                        $"Would you like to automatically create an account for {txtFname.Text} {txtSname.Text} ?\n",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (createAccount == DialogResult.Yes)
                    {
                        // get the Student ID for the newly registered student
                        int newStudentID = GetStudentIDByNames(txtFname.Text, txtSname.Text);

                        if (newStudentID > 0)
                        {
                            // setting the default password to name.surname@123
                            string defaultPassword = $"{txtFname.Text}.{txtSname.Text}@123";

                            Dictionary<string, object> usersFields = new Dictionary<string, object>
                            {
                                {"Username", txtStudEmail.Text },
                                {"Password", defaultPassword.ToLower() },
                                {"RoleID", "STD" },
                                {"StaffID", null },
                                {"StudentID", newStudentID },
                                {"isLocked", true }
                            };

                            Principal.CreateUserAccount<tblUser>(usersFields);
                            // log context details to file
                            Logger.LogUserAction(userSession, "Created User Account");
                        } else
                        {
                            MessageBox.Show("Error retrieving Student ID for the just registered student", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                } else
                {
                    MessageBox.Show("The Specified Guardian does not exist in our database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper method to fetch Guardian by name and surname
        private int GetGuardianByNames(string name, string surname)
        {
            try
            {
                using (var NamesDataContexts = new KGSDataClasses1DataContext())
                {
                    var guardian = NamesDataContexts.tblGuardians.FirstOrDefault(s => s.FirstName == name && s.LastName == surname);

                    // if no matching ID is found, return 0
                    return guardian?.GuardianID ?? 0;
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // helper method to get Teacher by name and surname
        private int GetTeacherByNames(string name, string surname)
        {
            try
            {
                using (var NamesDataContexts = new KGSDataClasses1DataContext())
                {
                    // search for first record matching the condition
                    var teacher = NamesDataContexts.tblStaffs.FirstOrDefault(t => t.FirstName == name && t.LastName == surname && t.RoleID == "TCH");

                    // if no matching ID is found, return 0
                    return teacher?.StaffID ?? 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void btnGoDash_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminDash admin = new AdminDash(userSession)
            {
                Visible = true
            };
        }

        // Helper function to fetch Student ID by names
        public static int GetStudentIDByNames(string name, string surname)
        {
            using (var StudentDataContext = new KGSDataClasses1DataContext())
            {
                var student = StudentDataContext.tblStudents.FirstOrDefault(s => s.FirstName == name && s.LastName == surname);
                return student?.StudentID ?? 0;
            }
        }

        private void btnGoTOClerk_Click(object sender, EventArgs e)
        {
            this.Close();
            ClerkForm clerk = new ClerkForm(userSession)
            {
                Visible = true
            };
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
            RegGuardian parent = new RegGuardian(userSession)
            {
                Visible = true
            };
        }
    }
}

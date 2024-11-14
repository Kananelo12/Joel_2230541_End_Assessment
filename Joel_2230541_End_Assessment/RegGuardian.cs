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
    public partial class RegGuardian : Form
    {
        // Instance variable to store sessionId
        private string userSession;
        public RegGuardian(string sessionId)
        {
            this.userSession = sessionId;
            InitializeComponent();
        }

        private void btnResetFields_Click(object sender, EventArgs e)
        {
            AdminDash.Reset(txtFname, txtSname, txtEmail, txtHomePhone, txtWorkPhone, txtAddress);
            rdbFemale.Checked = false;
            rdbMale.Checked = false;
        }

        // method to set gender
        public string SetGender(RadioButton rdb1, RadioButton rdb2)
        {
            if (rdb1.Checked)
            {
                return "Male";
            }
            else if (rdb2.Checked)
            {
                return "Female";
            }
            return null;
        }

        private void btnRegGuardian_Click(object sender, EventArgs e)
        {
            // validate before insertion
            bool isValid = LoginForm.ValidateInput(txtFname, txtSname, txtAddress, txtHomePhone);
            if (isValid)
            {
                try
                {
                    using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                    {
                        tblGuardian parent = new tblGuardian()
                        {
                            FirstName = txtFname.Text,
                            LastName = txtSname.Text,
                            Email = txtEmail.Text,
                            HomePhone = txtHomePhone.Text,
                            WorkPhone = txtWorkPhone.Text,
                            PhysicalAddress = txtAddress.Text
                        };
                        data.tblGuardians.InsertOnSubmit(parent);
                        data.SubmitChanges();
                        MessageBox.Show("Guardian Record Inserted Successfully!");
                        Logger.LogUserAction(userSession, "Created Guardian Record");
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // log exception to file
                    Logger.LogException(ex);
                }
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            RegStudent student = new RegStudent(userSession)
            {
                Visible = true
            };
        }
    }
}

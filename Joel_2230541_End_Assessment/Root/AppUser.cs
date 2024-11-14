/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment.Root
{
    internal class AppUser : Person
    {
        private string UserRole;
        private string Qualifications;
        public AppUser(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password, string userRole, string qualifications)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password)
        {
            UserRole = userRole;
            Qualifications = qualifications;
        }

        // setters and getters
        public string GetUserRole() { return UserRole; }
        public string GetQualifications() { return Qualifications; }
        public void SetQualifications(string qualifications) { this.Qualifications = qualifications; }
        public void SetUserRole(string userRole) { this.UserRole = userRole; }

        // override method to append properties specific to Student
        public new void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"User Role: {UserRole}");
            Console.WriteLine($"Highest Qualifications: {Qualifications}");
        }

        // logout method
        public static void Logout(Form currentForm)
        {
            // Show a confirmation dialog
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?\nYou will have to login again.",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Log the logout action
                Logger.LogUserAction(Person.sessionId, "User Logged out of system");

                // Close the current form
                currentForm.Close();

                // Set a flag or reset relevant session variables if needed
                Person.sessionId = null;
                Person.RoleID = null;
            }
        }

    }
}

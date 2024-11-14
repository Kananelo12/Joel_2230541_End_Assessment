/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment.Root
{
    abstract internal class Person : IPerson
    {
        // Static variable to hold session ID
        public static string sessionId = null;
        // keep track of user type
        public static string RoleID;
        // To keep track of locked accounts
        bool isLocked;

        // SQL Connection
        SqlConnection conn = null;
        // sql command instance variable
        SqlCommand cmd = null;
        // reader variable
        SqlDataReader reader = null;

        // fields
        protected int ID;
        protected string FirstName;
        protected string Surname;
        protected string Gender;
        protected string PhoneNumber;
        protected string EmailAddress;
        protected DateTime DateOfBirth;
        protected string PhysicalAddress;
        protected string Password;

        // parameterized constructor
        public Person(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password)
        {
            ID = id;
            FirstName = firstName;
            Surname = surname;
            Gender = gender;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            DateOfBirth = dob;
            PhysicalAddress = physicalAddress;
            Password = EncryptPassword(password);
        }

        // Method to hash the password using BCrypt
        private string EncryptPassword(string password)
        {
            // Work factor of 13 for strong security
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // getter methods
        public string GetFirstName()
        {
            return this.FirstName;
        }

        public string GetSurname()
        {
            return this.Surname;
        }
        public string GetGender()
        {
            return this.Gender;
        }
        public string GetPhoneNumber()
        {
            return this.PhoneNumber;
        }
        public string GetEmailAddress()
        {
            return this.EmailAddress;
        }
        public DateTime GetDoB()
        {
            return this.DateOfBirth;
        }
        public string GetPhysicalAddress()
        {
            return this.PhysicalAddress;
        }
        // setters
        public void SetID(int ID)
        {
            this.ID = ID;
        }
        public void SetFirstName(string firstName)
        {
            this.FirstName = firstName;
        }
        public void SetSurname(string surname)
        {
            this.Surname = surname;
        }
        public void SetGender(string gender)
        {
            this.Gender = gender;
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
        public void SetEmail(string email)
        {
            this.EmailAddress = email;
        }
        public void SetDoB(DateTime doB)
        {
            this.DateOfBirth = doB;
        }
        public void SetAddress(string address)
        {
            this.PhysicalAddress = address;
        }
        public void SetPassword(string password)
        {
            this.Password = EncryptPassword(password);
        }

        // Register a new user
        public bool Register()
        {
            try
            {
                using (SqlConnection conn = ConnectionFile.GetConn())
                {
                    string query = "INSERT INTO tblUsers (FirstName, Surname, Gender, PhoneNumber, EmailAddress, DateOfBirth, PhysicalAddress, Password) " +
                                   "VALUES (@firstName, @surname, @gender, @phoneNumber, @email, @dob, @address, @password)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@firstName", FirstName);
                    cmd.Parameters.AddWithValue("@surname", Surname);
                    cmd.Parameters.AddWithValue("@gender", Gender);
                    cmd.Parameters.AddWithValue("@phoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", EmailAddress);
                    cmd.Parameters.AddWithValue("@dob", DateOfBirth);
                    cmd.Parameters.AddWithValue("@address", PhysicalAddress);
                    cmd.Parameters.AddWithValue("@password", Password);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if the registration was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registration failed: " + ex.Message);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
        }

        // User Login method
        public bool Login(string username, string password)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                conn = ConnectionFile.GetConn();
                if (conn == null)
                {
                    MessageBox.Show("Connection could not be established.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string query = "SELECT Username, Password, RoleID, isLocked FROM tblUsers WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open(); // Open connection
                Console.WriteLine("Connection Opened Successfully");

                // execute query
                reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    isLocked = Convert.ToBoolean(reader["isLocked"].ToString());
                    string storedPassword = reader["Password"].ToString();

                    if (isLocked)
                    {
                        MessageBox.Show("It seems your account is still locked. Click on register below to unlock it", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (password == storedPassword)
                    {
                        // set session to logged-in user
                        sessionId = username;
                        RoleID = reader["RoleID"].ToString();
                        MessageBox.Show("Welcome, " + username);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Credentials", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.StackTrace);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Invalid Operation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Connection Closed Successfully");
                }
            }
        }

        // method to retrieve deleted student members
        public static void FillDeletedStudentsDGV(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from deleted in data.tblStudentArchives
                          select deleted).ToList();
            dgv.DataSource = result;
        }

        // method to retrieve deleted staff members
        public static void FillDeletedStaffDGV(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from deleted in data.tblStaffArchives
                          select deleted).ToList();
            dgv.DataSource = result;
        }




        // diplay details to the screen
        public void DisplayDetails()
        {
            Console.WriteLine("****************************");
            Console.WriteLine($"ID: {ID} ");
            Console.WriteLine($"Names: {FirstName} {Surname} ");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Phone NO: {PhoneNumber}");
            Console.WriteLine($"Email Address: {EmailAddress}");
            Console.WriteLine($"Date of Birth {DateOfBirth}");
            Console.WriteLine($"Physical Address: {PhysicalAddress}");
        }
    }
}

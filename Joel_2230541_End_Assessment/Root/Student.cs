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
    internal class Student : AppUser
    {
        private string Grade;
        private DateTime JoinDate;
        private Guardian Guardian;
        private Decimal AmountPaid;
        private Decimal OutstandingAmount;
        private string results;

        // parameterized constructor
        public Student(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password, string userRole, string qualifications, string grade, DateTime joinDate, Guardian guardian, decimal amountPaid, decimal outstandingAmount, string results)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password, userRole, qualifications)
        {
            Grade = grade;
            JoinDate = joinDate;
            Guardian = guardian;
            AmountPaid = amountPaid;
            OutstandingAmount = outstandingAmount;
            this.results = results;
        }

        // getters
        public string GetGrade() { return Grade; }
        public DateTime GetJoinDate() { return JoinDate; }
        public Guardian GetGuardian() { return Guardian; }
        public decimal GetAmountPaid() { return AmountPaid; }
        public decimal GetOutstandingAmount() { return OutstandingAmount; }
        public string GetResults() { return results; }

        // setters
        public void SetGrade(string grade) { this.Grade = grade; }
        public void SetJoinDate(DateTime joinDate) { this.JoinDate = joinDate; }
        public void SetGuardian(Guardian guardian) { this.Guardian = guardian; }
        public void SetAmountPaid(Decimal amountPaid) { this.AmountPaid = amountPaid; }
        public void SetOutstandingAmount(Decimal outAmount) { this.OutstandingAmount = outAmount; }
        public void SetResults(string results) { this.results = results; }


        // method to insert student
        public static void InsertStudents<T>(Dictionary<string, object> fieldValues) where T : class, new()
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
                        try
                        {
                            object value = fieldValue.Value;

                            // Check if the property type is nullable and handle accordingly
                            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                // Get the underlying type of the nullable property
                                Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                                value = (fieldValue.Value == null) ? null : Convert.ChangeType(fieldValue.Value, underlyingType);
                            }
                            else
                            {
                                // For non-nullable properties, convert as usual
                                value = Convert.ChangeType(fieldValue.Value, property.PropertyType);
                            }

                            property.SetValue(record, value);
                        }
                        catch (InvalidCastException ex)
                        {
                            MessageBox.Show($"Error setting property {fieldValue.Key}: {ex.Message}");
                            // log exception to file
                            Logger.LogException(ex);
                            return;
                        }
                    }
                }

                // Insert the record into the database
                try
                {
                    data.GetTable<T>().InsertOnSubmit(record);
                    data.SubmitChanges();
                    MessageBox.Show("Record Inserted Successfully");
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 
        public static string CheckStudentAge(DateTime birthDate)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate the age
            int age = today.Year - birthDate.Year;

            // Adjust if the birth date has not yet occurred this year
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            // Check if the age is within the allowed range
            if (age < 3 || age > 20)
            {
                return "Sorry, you are too young to enroll at Karabo Group of Schools. Please try again next time";
            }
            else
            {
                return null;
            }
        }


        // get the results of the current student
        public static void FillMyResults(DataGridView dgv, int Id)
        {
            try
            {
                KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
                var result = (from res in data.tblResults
                              where res.StudentID == Id
                              select new
                              {
                                  res.ResultID,
                                  res.StudentID,
                                  res.Subjects,
                                  res.Marks,
                                  res.Symbol,
                                  res.Term
                              }).ToList();
                dgv.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
            }
        }




        // override method to append properties specific to Student
        public new void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Grade: {Grade}");
            Console.WriteLine($"JoinDate: {JoinDate}");
            Console.WriteLine($"Guardian: {Guardian}");
            Console.WriteLine($"Amount Paid: {AmountPaid}");
            Console.WriteLine($"Outstanding Fees: {OutstandingAmount}");
            Console.WriteLine($"Results: {results} ");
        }
    }
}

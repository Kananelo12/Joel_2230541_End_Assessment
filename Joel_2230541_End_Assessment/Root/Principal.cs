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
    internal class Principal : AppUser
    {
        public Principal(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password, string userRole, string qualifications)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password, userRole, qualifications)
        {

        }

        // Staff method to insert data using LINQ
        public static void CreateUserAccount<T>(Dictionary<string, object> fieldValues) where T : class, new()
        {
            using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
            {
                T record = new T();

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
                            // log exception to file
                            Logger.LogException(ex);
                            MessageBox.Show($"Error setting property {fieldValue.Key}: {ex.Message}");
                            return;
                        }
                    }
                }

                // Insert the record into the database
                data.GetTable<T>().InsertOnSubmit(record);
                data.SubmitChanges();
                MessageBox.Show("Record Inserted Successfully");
            }
        }



        // Method to Retrieve Role Counts
        public static Dictionary<string, int> GetRoleCounts()
        {
            using (var db = new KGSDataClasses1DataContext())
            {
                // Group by role in the tblStaff table and count each role
                var userRoles = db.tblUsers
                                   .GroupBy(s => s.RoleID)
                                   .Select(group => new { RoleID = group.Key, Count = group.Count() })
                                   .ToDictionary(g => g.RoleID, g => g.Count);
                return userRoles;
            }
        }

        // Helper function to fetch Student ID by names
        public static int GetStaffIDByNames(string name, string surname)
        {
            using (var staffDataContext = new KGSDataClasses1DataContext())
            {
                var staff = staffDataContext.tblStaffs.FirstOrDefault(s => s.FirstName == name && s.LastName == surname);
                return staff?.StaffID ?? 0;
            }
        }

    }
}

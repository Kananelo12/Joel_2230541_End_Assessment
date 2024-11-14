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
    internal class Teacher : AppUser
    {
        public Teacher(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password, string userRole, string qualifications)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password, userRole, qualifications)
        {

        }

        // calculate the marks on the given range
        public static string CalculateGradeSymbol(int mark)
        {
            if (mark >= 90 && mark <= 100)
            {
                return "A";
            }
            else if (mark >= 80 && mark <= 89)
            {
                return "B";
            }
            else if (mark >= 70 && mark <= 79)
            {
                return "C";
            }
            else if (mark >= 60 && mark <= 69)
            {
                return "D";
            }
            else if (mark >= 50 && mark <= 59)
            {
                return "E";
            }
            else if (mark < 50)
            {
                return "F";
            }
            else
            {
                return "Invalid mark";
            }
        }
        public static void FillDGVStudents(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from Student in data.tblStudents
                          select new
                          {
                              Student.StudentID,
                              Student.FirstName,
                              Student.LastName,
                              Student.DateOfBirth,
                              Student.Gender,
                              Student.PhoneNumber,
                              Student.HomeAddress,
                              Student.GradeLevel,
                              Student.JoinDate,
                              Student.GuardianID,
                              Student.TeacherID
                          }).ToList();
            dgv.DataSource = result;
        }

        // get the results set
        public static void FillResults(DataGridView dgv)
        {
            try
            {
                KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
                var result = (from res in data.tblResults
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
            } catch (Exception ex)
            {
                // log exception to file
                Logger.LogException(ex);
                MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // get the current teacher's student
        public static void FillTeacherStudents(DataGridView dgv)
        {
            try
            {
                KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
                var result = (from student in data.tblStudents
                              join staff in data.tblStaffs on student.TeacherID equals staff.StaffID
                              select new
                              {
                                  student.StudentID,
                                  student.FirstName,
                                  student.LastName,
                                  student.GradeLevel,
                                  student.TeacherID
                              }).ToList();
                dgv.DataSource = result;
            } catch (Exception ex)
            {
                // log exception to file
                Logger.LogException(ex);
                MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

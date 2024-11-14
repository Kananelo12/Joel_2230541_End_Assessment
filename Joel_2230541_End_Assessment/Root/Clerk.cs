/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Joel_2230541_End_Assessment.Root
{
    internal class Clerk : AppUser
    {
        // connection instance variables
        SqlConnection conn = null;
        SqlDataReader reader = null;

        public Clerk(int id, string firstName, string surname, string gender, string phoneNumber, string emailAddress, DateTime dob, string physicalAddress, string password, string userRole, string qualifications)
        : base(id, firstName, surname, gender, phoneNumber, emailAddress, dob, physicalAddress, password, userRole, qualifications)
        {
        }

        public bool AccountVerification(string studentId, string password, DateTime DoB)
        {
            try
            {
                conn = ConnectionFile.GetConn();
                if (conn == null)
                {
                    MessageBox.Show("Connection could not be established.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string query = "SELECT u.Password, u.StudentID, u.isLocked, s.StudentID, s.DateOfBirth FROM tblUsers as u JOIN tblStudent as s ON u.StudentID = s.StudentID WHERE u.StudentID = @studentId AND u.Password = @password AND s.DateOfBirth = @DoB";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@DoB", DoB);

                // open connection
                conn.Open();
                Console.WriteLine("Connection Opened Successfully");

                // execute query
                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    using (KGSDataClasses1DataContext data = new KGSDataClasses1DataContext())
                    {
                        int StudID = Convert.ToInt32(studentId);
                        if (!int.TryParse(studentId, out StudID))
                        {
                            MessageBox.Show("Invalid Student ID.");
                            return false;
                        }

                        //tblStudent student = new tblStudent();
                        tblUser user = data.tblUsers.FirstOrDefault(u => u.StudentID == StudID && u.Password == password && u.tblStudent.DateOfBirth == DoB);
                        if (user == null)
                        {
                            MessageBox.Show("Student with this details not found!");
                            return false;
                        }

                        // Update student account to unlocked
                        user.isLocked = false;

                        // Submit changes to the database
                        data.SubmitChanges();
                        MessageBox.Show("Your account has been created and Unlcoked", "Account Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }

                }
                else
                {
                    MessageBox.Show("The Entered Credentials do not match. Try Again", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
                return false;
            }
            finally
            {
                // close reader
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                // close connection
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Connection Closed Successfully");
                }
            }
        }

        // populate student in fincance management of the ClerkForm
        public static void FillOwingStudents(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from owing in data.tblStudents
                          select new
                          {
                              owing.StudentID,
                              owing.FirstName,
                              owing.LastName,
                              owing.GradeLevel
                          }).ToList();
            dgv.DataSource = result;
        }

        // populate payment table
        public static void FillPayments(DataGridView dgv)
        {
            KGSDataClasses1DataContext data = new KGSDataClasses1DataContext();
            var result = (from paid in data.tblPayments
                          select new
                          {
                              paid.PaymentID,
                              paid.StudentID,
                              paid.AmountPaid,
                              paid.OutstandingAmount,
                              paid.PaymentDate,
                              paid.CashierID,
                              paid.ReceiptNO
                          }).ToList();
            dgv.DataSource = result;
        }

        // Function to generate PDF receipt
        public static void GeneratePDFReceipt(tblPayment payment)
        {
            // file path of the receipt
            string filePath = $@"..\..\Files\Receipt_{payment.ReceiptNO}.pdf";

            // initializing the document and writer
            Document doc = new Document(PageSize.A4);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                Paragraph title = new Paragraph("Payment Receipt", FontFactory.GetFont("Arial", 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                // append all details to the document
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"Receipt No: {payment.ReceiptNO}"));
                doc.Add(new Paragraph($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}"));
                doc.Add(new Paragraph($"Student ID: {payment.StudentID}"));
                doc.Add(new Paragraph($"Amount Paid: {payment.AmountPaid:C}"));
                doc.Add(new Paragraph($"Outstanding Amount: {payment.OutstandingAmount:C}"));
                doc.Add(new Paragraph($"Cashier ID: {payment.CashierID}"));

                doc.Add(new Paragraph("\nThank you for your payment!", FontFactory.GetFont("Arial", 12, Font.ITALIC)));

                MessageBox.Show("PDF receipt generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
            }
            finally
            {
                doc.Close();
            }

            // Automatically open the PDF
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log exception to file
                Logger.LogException(ex);
            }
        }

    }
}

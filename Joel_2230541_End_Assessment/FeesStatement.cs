/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    internal class FeesStatement
    {
        public static void GenerateFeeStatement()
        {
            // Define file path for the fees statement PDF
            string filePath = @"..\..\Files\FeesStatement.pdf";

            // Initialize the document and writer
            Document doc = new Document(PageSize.A4);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                // Add title to the document
                Paragraph title = new Paragraph("Fees Statement", FontFactory.GetFont("Arial", 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(new Paragraph("\n"));

                // Create a table with appropriate columns for the payment details
                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1, 1, 1, 1, 1, 1 });

                // Add headers to the table
                AddCellToHeader(table, "Receipt No");
                AddCellToHeader(table, "Date");
                AddCellToHeader(table, "Student ID");
                AddCellToHeader(table, "Amount Paid");
                AddCellToHeader(table, "Outstanding Amount");
                AddCellToHeader(table, "Cashier ID");

                // Query the data from the database using LINQ
                using (var context = new KGSDataClasses1DataContext())
                {
                    var payments = context.tblPayments.ToList();

                    // Populate the table with each record from the query
                    foreach (var payment in payments)
                    {
                        AddCellToBody(table, payment.ReceiptNO);
                        AddCellToBody(table, payment.PaymentDate.ToString("yyyy-MM-dd"));
                        AddCellToBody(table, payment.StudentID.ToString());
                        AddCellToBody(table, payment.AmountPaid.ToString());
                        AddCellToBody(table, payment.OutstandingAmount.ToString());
                        AddCellToBody(table, payment.CashierID.ToString());
                    }
                }

                // Add the table to the document
                doc.Add(table);

                MessageBox.Show("PDF fees statement generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF fees statement: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Helper method to add header cells with formatting
        private static void AddCellToHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont("Arial", 12, Font.BOLD)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.ORANGE;
            table.AddCell(cell);
        }

        // Helper method to add body cells with formatting
        private static void AddCellToBody(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont("Arial", 10, Font.NORMAL)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
        }
    }
}

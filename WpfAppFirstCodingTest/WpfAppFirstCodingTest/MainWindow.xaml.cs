using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using WinForms = System.Windows.Forms;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using SpreadsheetLight;

namespace WpfAppFirstCodingTest
{

    public partial class MainWindow : System.Windows.Window
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
        private SqlConnection _sqlConnection;


        public MainWindow()
        {
            InitializeComponent();

            _sqlConnection = new SqlConnection(_connectionString);
            TextBox.Text = _connectionString;
            Disable();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = ChangeConnectionString();
        }

        private void Disable()
        {
            UsernameTextBox.IsEnabled = false;
            PasswordBox.IsEnabled = false;
        }

        private void Enable()
        {
            UsernameTextBox.IsEnabled = true;
            PasswordBox.IsEnabled = true;
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
                    folderDialog.ShowNewFolderButton = false;
                    folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    String sPath = folderDialog.SelectedPath;
                    DirectoryInfo folder = new DirectoryInfo(sPath);

                    if (folder.Exists)
                    {
                        var sPath_SubDirectory = folder.FullName + "\\" + "MyDocuments";

                        if (Directory.Exists(sPath_SubDirectory) == false)
                        {
                            Directory.CreateDirectory(sPath_SubDirectory);
                            sPath = sPath_SubDirectory;
                        }
                        else
                        {
                            sPath_SubDirectory = folder.FullName + "\\" + "MyDocuments";
                            sPath = sPath_SubDirectory;
                        }

                        var path = sPath + "/test.pdf";

                        //CreatePdf(path);
                        Process.Start(path);
                    }
                    else
                    {
                        MessageBox.Show("Folder doesn't exixt");
                    }
                }
                else
                {
                    var path = filePath + "/test.pdf";

                    // CreatePdf(path);
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        string filePath = string.Empty;
        //private object ExcelVersion;

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            filePath = dialog.SelectedPath;  // file path
        }

        /*  private void CreatePdf(string path)
          {
              var pdfDocument = new Document(PageSize.LETTER);
              try
              {
                  var writer = PdfWriter.GetInstance(pdfDocument, new FileStream(path, FileMode.Create));

                  pdfDocument.Open();

                  var table = new PdfPTable(3);
                  table.SetTotalWidth(new float[] { 600, 300, 600 });
                  table.DefaultCell.FixedHeight = 30;
                  var cell = new PdfPCell(new Phrase("Enter The Server Name:"));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.PaddingTop = 20;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.FixedHeight = 45;
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.PaddingTop = 20;
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);

                  table.AddCell("");
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);

                  cell = new PdfPCell(new Phrase("Authentication Type:"));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.PaddingTop = 20;
                  cell.FixedHeight = 40;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 40;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase("Enter The database Name:"));
                  cell.FixedHeight = 40;
                  cell.PaddingTop = 20;
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);

                  cell = new PdfPCell(new Phrase("Windows Authentication"));
                  cell.BackgroundColor = new BaseColor(221, 221, 221);
                  cell.PaddingTop = 8;
                  cell.FixedHeight = 30;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.FixedHeight = 30;
                  table.AddCell(cell);

                  cell = new PdfPCell(new Phrase("Enter UserName:"));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.PaddingTop = 20;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.FixedHeight = 45;
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase("Enter Password:"));
                  cell.PaddingTop = 20;
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);

                  table.AddCell("");
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  table.AddCell(cell);
                  table.AddCell("");

                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  table.AddCell(cell);

                  var buttonTable = new PdfPTable(3); //here used Nested Table 
                  buttonTable.SetTotalWidth(new float[] { 150, 300, 150 });
                  table.DefaultCell.FixedHeight = 45;
                  table.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  buttonTable.AddCell(cell);
                  cell = new PdfPCell(new Phrase("Update"));
                  cell.HorizontalAlignment = Element.ALIGN_CENTER;
                  cell.BackgroundColor = new BaseColor(221, 221, 221);
                  cell.FixedHeight = 45;
                  cell.PaddingTop = 15;
                  buttonTable.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  buttonTable.AddCell(cell);
                  table.AddCell(buttonTable);

                  buttonTable = new PdfPTable(1); //here used Nested Table
                  buttonTable.SetTotalWidth(new float[] { 300 });
                  cell = new PdfPCell(new Phrase("PDF"));
                  cell.HorizontalAlignment = Element.ALIGN_CENTER;
                  cell.BackgroundColor = new BaseColor(221, 221, 221);
                  cell.FixedHeight = 45;
                  cell.PaddingTop = 15;
                  buttonTable.AddCell(cell);
                  table.AddCell(buttonTable);

                  buttonTable = new PdfPTable(3); //here used Nested Table
                  buttonTable.SetTotalWidth(new float[] { 150, 300, 150 });
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  buttonTable.AddCell(cell);
                  cell = new PdfPCell(new Phrase("Select Location"));
                  cell.HorizontalAlignment = Element.ALIGN_CENTER;
                  cell.BackgroundColor = new BaseColor(221, 221, 221);
                  cell.FixedHeight = 45;
                  cell.PaddingTop = 7;
                  buttonTable.AddCell(cell);
                  cell = new PdfPCell(new Phrase(""));
                  cell.Border = Rectangle.NO_BORDER;
                  cell.FixedHeight = 45;
                  buttonTable.AddCell(cell);
                  table.AddCell(buttonTable);

                  pdfDocument.Add(table);
                  var baseFontStyle = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, false);
                  var baseColor = new BaseColor(0, 0, 0, 45);
                  var times = new Font(baseFontStyle, 180, Font.ITALIC, baseColor);
                  // Dim wfont = New Font(BaseFont.TIMES_ROMAN, 1.0F, BaseFont.COURIER, BaseColor.LIGHT_GRAY)
                  ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_RIGHT, new Phrase("Hossain", times), 500, 700, 45);
                  ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_BASELINE, new Phrase("Hossain", times),0, 1, 0);

                  //End water mark
                  pdfDocument.Close();
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          } */

        private void DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (ComboBox.SelectedIndex == 1)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
            }
            catch (Exception)
            {

            }
        }

        private string ChangeConnectionString()
        {
            var connectionString = _connectionString;
            try
            {
                if (ComboBox.SelectedIndex == 0)
                {
                    connectionString = connectionString.Replace("True", "False;");

                    connectionString = connectionString + " User Id=" + UsernameTextBox.Text + "; Password=" + PasswordBox.Password + "";
                }
            }
            catch (Exception)
            {

            }
            return connectionString;
        }

        private void ExcelFileWithDropDownButton_Click(object sender, RoutedEventArgs e)
        {
            var app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook objBook = app.Workbooks.Add(System.Reflection.Missing.Value);

            //Get the First sheet.
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)objBook.Sheets["Sheet1"];

            //Add data into A1 and B1 Cells as headers.
            sheet.Cells[1, 1] = "Product ID";

            sheet.Cells[1, 2] = "Product Name";

            //Add data into details cells.
            //sheet.Cells[2, 1] = 1;
            //Enable Auto-filter.           
            sheet.EnableAutoFilter = true;
            ////Create the range.
            Microsoft.Office.Interop.Excel.Range range = sheet.get_Range("A1", "B5");

            ////Auto-filter the range.
            range.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

            ////Auto-fit the second column.
            sheet.get_Range("B1", "B5").EntireColumn.AutoFit();
            sheet.get_Range("C1", "C5").EntireColumn.AutoFit();


            //            var workbook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //            var workSheet = workbook.Worksheets[1];
            //            // workSheet = workbook.Worksheets[2];

            //            for(int i = 1; i<=8; i++)
            //            {
            //                workSheet.Columns[i].ColumnWidth = 22;
            //            }
            //            workSheet.Range["A1"].Value = "Enter Server Name:";
            //            workSheet.Range["A3"].Value = "Authentication type";

            //            var list = new List<string>();
            //            list.Add("Windows Authentication");
            //            list.Add("SQL Server Authentication");
            //            var flatList = string.Join(",", list);

            //            var cell = (Range)workSheet.Cells[4,4];
            //            cell.Validation.Delete();
            //            cell.Validation.Add(
            //               XlDVType.xlValidateList,
            //               XlDVAlertStyle.xlValidAlertInformation,
            //               XlFormatConditionOperator.xlBetween,
            //               flatList,
            //               Type.Missing);
            //            cell.Value = list[0];
            //            cell.Validation.IgnoreBlank = true;           
            //            cell.Validation.InCellDropdown = true;

            //            workSheet.Range["B3"].Value = "Enter database Name:";
            //            workSheet.Range["A6"].Value = "UserName:";
            //            workSheet.Range["B6"].Value = "Password:";
            //-----------------------------------------------------------------------
            // creating Excel Application

            //try
            //{
            //    var app = new Microsoft.Office.Interop.Excel.Application();
            //// creating new WorkBook within Excel application
            //var workbook = app.Workbooks.Add(Type.Missing);
            //    // creating new Excelsheet in workbook
            //    _Worksheet worksheet = null;

            //Sheets xlSheets = null;
            //Worksheet xlNewSheet = null;


            //xlSheets = workbook.Sheets as Sheets;

            //    // see the excel sheet behind the program
            //    app.Visible = true;

            //    //Select the sheet
            //    worksheet = workbook.Worksheets[1];

            //    for (int i = 0; i < 10; i++)
            //    {
            //        xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
            //        xlNewSheet.Name = $"MySheet {i}";
            //    }

            //    // save the application
            //    workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //    // Exit from the application

            //    app.Quit();

        }                         

        private void ExcelSheetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ExcelFilter();
            MessageBox.Show("File created on this 'G:\\_Office\\Internship_Tasks' path");
        }

        private void ExcelFilter()
        {
            var spreadSheet = new SLDocument();
            var spreadSheet2 = new SLDocument();
            filePath = @"G:\_Office\Internship_Tasks\Thermon.xlsx";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            spreadSheet.SetCellValue(1, 1, "TPR Name ");
            spreadSheet.SetCellValue(1, 2, "TPR Number ");
            spreadSheet.SetCellValue(1, 3, "Country      .");
            spreadSheet.SetCellValue(1, 4, "Diligence Level      .");
            spreadSheet.SetCellValue(1, 5, "Approval Status      .");
            spreadSheet.SetCellValue(1, 6, "Data Approved      .");
            spreadSheet.SetCellValue(1, 7, "Questionnaire Status      .");
            spreadSheet.SetCellValue(1, 8, "Training Status      .");
            spreadSheet.SetCellValue(1, 9, "Certification Status      .");
            spreadSheet.SetCellValue(1, 10, "Sponsor");
            spreadSheet.AutoFitColumn(1, 10);

            for (int i = 2; i <= 300; i++)
            {
                if (i % 2 == 0)
                {
                    spreadSheet.SetCellValue(i, 5, "Approved");
                }
                else
                    spreadSheet.SetCellValue(i, 5, "Not Approved");
            }

            spreadSheet.Filter("C1", "I1");

            spreadSheet.SaveAs(filePath);
            spreadSheet2.SaveAs(filePath);
        }
    }
}


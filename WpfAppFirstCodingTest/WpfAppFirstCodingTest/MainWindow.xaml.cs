﻿using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using WinForms = System.Windows.Forms;
using System.Configuration;


namespace WpfAppFirstCodingTest
{
    public partial class MainWindow : Window
    {
        private string _conncetionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
        private SqlConnection _sqlConnection;

        Document pdfDocument = new Document(PageSize.A4);

        public MainWindow()
        {
            InitializeComponent();

            _sqlConnection = new SqlConnection(_conncetionString);
            TextBox.Text = _conncetionString;
            Disable();           
        }
   
        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = ChageConnectionString();
        }     

        public void Disable()
        {
            UsernameTextBox.IsEnabled = false;
            PasswordBox.IsEnabled = false;
        }

        public void Enable()
        {
            UsernameTextBox.IsEnabled = true;
            PasswordBox.IsEnabled = true;
        }

        public void PrintBtn_Click(object sender, RoutedEventArgs e)
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
                        
                        PdfWriter.GetInstance(pdfDocument,
                                 new FileStream(path, FileMode.Create));
                        pdfDocument.Open();

                        CreatePdf();

                        pdfDocument.Close();
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
                    PdfWriter.GetInstance(pdfDocument,
                             new FileStream(path, FileMode.Create));
                    pdfDocument.Open();
                    CreatePdf();
                    pdfDocument.Close();
                    Process.Start(path);
                }
            }
            catch (Exception )
            {
               
            }
                
        }  

        string filePath = string.Empty;

        public void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {         
            var dialog = new System.Windows.Forms.FolderBrowserDialog();           
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();          
            filePath = dialog.SelectedPath;  // file path
        }

        private Document CreatePdf()
        {           
            PdfPTable table = new PdfPTable(3);
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

            var nestedTable = new PdfPTable(3);
            nestedTable.SetTotalWidth(new float[] { 150, 300, 150 });
            table.DefaultCell.FixedHeight = 45;
            table.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
            cell = new PdfPCell(new Phrase(""));
            cell.Border = Rectangle.NO_BORDER;
            cell.FixedHeight = 45;
            nestedTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Update"));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = new BaseColor(221, 221, 221);
            cell.FixedHeight = 45;
            cell.PaddingTop = 15;
            nestedTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(""));
            cell.Border = Rectangle.NO_BORDER;
            cell.FixedHeight = 45;
            nestedTable.AddCell(cell);
            table.AddCell(nestedTable);

            nestedTable = new PdfPTable(1);
            nestedTable.SetTotalWidth(new float[] { 300 });
            cell = new PdfPCell(new Phrase("PDF"));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = new BaseColor(221, 221, 221);
            cell.FixedHeight = 45;
            cell.PaddingTop = 15;
            nestedTable.AddCell(cell);
            table.AddCell(nestedTable);

            nestedTable = new PdfPTable(3);
            nestedTable.SetTotalWidth(new float[] { 150, 300, 150 });
            cell = new PdfPCell(new Phrase(""));
            cell.Border = Rectangle.NO_BORDER;
            cell.FixedHeight = 45;
            nestedTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Select Location"));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = new BaseColor(221, 221, 221);
            cell.FixedHeight = 45;
            cell.PaddingTop = 7;
            nestedTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(""));
            cell.Border = Rectangle.NO_BORDER;
            cell.FixedHeight = 45;
            nestedTable.AddCell(cell);
            table.AddCell(nestedTable);

            pdfDocument.Add(table);

            return pdfDocument;         
        }

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

        private string ChageConnectionString()
        {
            var conncetionString = _conncetionString;
            try
            {             
            if (ComboBox.SelectedIndex == 0)
                {
                conncetionString = conncetionString.Replace("True", "False;");
                 
                conncetionString = conncetionString +" User Id=" + UsernameTextBox.Text + "; Password=" + PasswordBox.Password + "";                   
                }         
            }
            catch (Exception)
            {

            }
            return conncetionString;
        }        
    }       
}


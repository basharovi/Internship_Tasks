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


namespace WpfAppFirstCodingTest
{
    public partial class MainWindow : Window
    {
        private string _conncetionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
        private SqlConnection _sqlConnection;

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
                        Document pdfDocument = new Document(PageSize.A4);
                        PdfWriter.GetInstance(pdfDocument,
                                 new FileStream(path, FileMode.Create));
                        pdfDocument.Open();
                        
                        PdfPTable table = new PdfPTable(2);
                        table.SetTotalWidth(new float[] {250,250});
                        table.DefaultCell.Padding = 1;
                        
                        PdfPCell cell = new PdfPCell(new Phrase("Header spanning 3 columns"));
                        cell.BackgroundColor = new BaseColor(100,100,100);
                        cell.Colspan = 2;
                        cell.FixedHeight = 50;
                        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right                        
                        table.AddCell(cell);

                        table.AddCell("Row 2, Col 1");
                        table.AddCell("Row 2, Col 2");
                                             
                        cell = new PdfPCell(new Phrase("Row 3, Col 1 & 2 "));
                        cell.Colspan = 2;
                        cell.FixedHeight = 50;
                        table.AddCell(cell);                   

                        table.AddCell("Row 4, Col 1");
                        table.AddCell("Row 4, Col 2");

                        table.AddCell("Row 5, Col 1");
                        table.AddCell("Row 5, Col 2");

                        table.AddCell("Row 6, Col 1");
                        cell = new PdfPCell(new Phrase("Row 6,  2 "));
                        cell.FixedHeight = 50;
                        table.AddCell(cell);

                        table.AddCell("Row 7, Col 1");
                        table.AddCell("Row 7, Col 2");

                        pdfDocument.Add(table);
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
                    var pdfDocument = new Document();
                    PdfWriter.GetInstance(pdfDocument,
                             new FileStream(path, FileMode.Create));
                    pdfDocument.Open();
                    pdfDocument.Add(new Paragraph("Here is a test of creating a PDF"));
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


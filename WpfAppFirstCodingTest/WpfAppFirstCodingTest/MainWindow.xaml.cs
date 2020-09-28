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
            try {
                var path = filePath + "/test.pdf";
                var pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument,
                         new FileStream(path, FileMode.Create));
                pdfDocument.Open();
                pdfDocument.Add(new Paragraph("Here is a test of creating a PDF"));
                pdfDocument.Close();              
                Process.Start(path);
            }
            catch (Exception)
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    var folderDialog = new WinForms.FolderBrowserDialog();
                    folderDialog.ShowNewFolderButton = false;
                    folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                
                    var sPath = folderDialog.SelectedPath;
                    DirectoryInfo folder = new DirectoryInfo(sPath);

                    if (folder.Exists)
                    {
                        foreach (FileInfo fileInfo in folder.GetFiles())
                        {
                            var sPath_SubDirectory = folder.FullName + "\\" + "MyDocuments";

                            if (Directory.Exists(sPath_SubDirectory) == false)
                            { Directory.CreateDirectory(sPath_SubDirectory);
                                sPath = sPath_SubDirectory;
                            }
                            else
                            {
                                sPath_SubDirectory = folder.FullName + "\\" + "MyDocuments";
                                sPath = sPath_SubDirectory;
                            }
                        }
                    }
                var path = sPath + "/test.pdf";
                var pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument,
                         new FileStream(path, FileMode.Create));            
                pdfDocument.Open();
                pdfDocument.Add(new iTextSharp.text.Paragraph("Here is a test of creating a PDF"));
                pdfDocument.Close();
             
                Process.Start(path);
               }      
                else
                {
                    MessageBox.Show("something Wrong!!!!");
                }
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


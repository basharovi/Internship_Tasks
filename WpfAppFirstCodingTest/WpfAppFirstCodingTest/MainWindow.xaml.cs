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
        }
   
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = _conncetionString;
            try
            {
                var sqlQuery = "Insert INTO login_tbl values (@Username,@Password)";

                //SqlConnection _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, _sqlConnection);
                // I have placed this command to test the connection that is connected to the database.
                cmd.Parameters.AddWithValue("@Username", UsernameTextBox.Text);
                cmd.Parameters.AddWithValue("@Password", PasswordBox.Password);
                cmd.Connection = _sqlConnection;
                cmd.ExecuteNonQuery();               
                MessageBox.Show("Updated!!!");
                _sqlConnection.Close();
            }
            catch (Exception)
            {
                //if (string.IsNullOrEmpty(DbNameTextBox.Text))
                //{
                //    MessageBox.Show("Database name is required!");
                //}
                //else
                //{
                //    MessageBox.Show("Please Enter the right Database!!");
                //}
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            try
            {
#pragma warning disable CS0252 
                if (ComboBox.SelectedIndex == 1)
                {
                    _conncetionString = _conncetionString.Replace("False;", "True");
                    UsernameTextBox.IsEnabled = false;
                    PasswordBox.IsEnabled = false;
                    _sqlConnection = new SqlConnection(_conncetionString);
                    //SqlConnection _sqlConnection = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DbNameTextBox.Text + ";Integrated Security=true;");
                    _sqlConnection.Open();                  
                    _sqlConnection.Close();                   
                }
#pragma warning disable CS0252 
                else if (ComboBox.SelectedIndex == 0)
                {
                  _conncetionString = _conncetionString.Replace("True", "False;");
                    var getUserName = UsernameTextBox.Text;
                    var getPassword = PasswordBox.Password;
                   _conncetionString = _conncetionString+" "+" User Id=" +getUserName + ";Password=" +getPassword+ "";
          
                    UsernameTextBox.IsEnabled = true;
                    PasswordBox.IsEnabled = true;
                   
                    _sqlConnection = new SqlConnection(_conncetionString);
                    //  SqlConnection _sqlConnection = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DbNameTextBox.Text + ";Integrated Security=false; User Id=" + UsernameTextBox.Text + ";Password=" + PasswordBox.Password + ";");
                    _sqlConnection.Open();
                    _sqlConnection.Close();
                    _conncetionString = "";
                }
                else
                {
                    MessageBox.Show("something wrong!");
                }
            }
            catch (Exception)
            {

            }          
        }
        public void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try {
                var path = filePath + "/test.pdf";
                Document pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument,
                         new FileStream(path, FileMode.Create));
                pdfDocument.Open();
                pdfDocument.Add(new iTextSharp.text.Paragraph("Here is a test of creating a PDF"));
                pdfDocument.Close();              
                Process.Start(path);
            }
            catch (Exception)
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
                        foreach (FileInfo fileInfo in folder.GetFiles())
                        {
                            Debug.WriteLine("#Debug: File: " + fileInfo.Name);
                            String sPath_SubDirectory = folder.FullName + "\\" + "MyDocuments";

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
                Document pdfDocument = new Document();
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
        //string fileContent = string.Empty;
        string filePath = string.Empty;
        public void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {         
            var dialog = new System.Windows.Forms.FolderBrowserDialog();           
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();          
            filePath = dialog.SelectedPath;  // file path
        }
    }    
}


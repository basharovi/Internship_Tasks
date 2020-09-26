using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using WinForms = System.Windows.Forms;

namespace WpfAppFirstCodingTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connectionString = "Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DbNameTextBox.Text + ";Integrated Security=true;";
                var sqlQuery = "Insert INTO login_tbl values (@Username,@Password)";

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                // I have placed this command to test the connection that is connected to the database.
                cmd.Parameters.AddWithValue("@Username", UsernameTextBox.Text);
                cmd.Parameters.AddWithValue("@Password", PasswordBox.Password);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated!!!!!");

                con.Close();
            }
            catch (Exception)
            {
                if (string.IsNullOrEmpty(DbNameTextBox.Text))
                {
                    MessageBox.Show("Database name is required!");
                }
                else
                {
                    MessageBox.Show("Please Enter the right Database!!");
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
#pragma warning disable CS0252 
                if (ComboBox.SelectedIndex == 1)
                {
                    UsernameTextBox.IsEnabled = false;
                    PasswordBox.IsEnabled = false;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DbNameTextBox.Text + ";Integrated Security=true;");
                    con.Open();
                    con.Close();
                }
#pragma warning disable CS0252 
                else if (ComboBox.SelectedIndex == 0)
                {
                    UsernameTextBox.IsEnabled = true;
                    PasswordBox.IsEnabled = true;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DbNameTextBox.Text + ";Integrated Security=false; User Id=" + UsernameTextBox.Text + ";Password=" + PasswordBox.Password + ";");
                    con.Open();
                    con.Close();
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
        string fileContent = string.Empty;
        string filePath = string.Empty;
        public void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {         
            var dialog = new System.Windows.Forms.FolderBrowserDialog();           
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();          
            filePath = dialog.SelectedPath;  // file path
        }     
    }    
}


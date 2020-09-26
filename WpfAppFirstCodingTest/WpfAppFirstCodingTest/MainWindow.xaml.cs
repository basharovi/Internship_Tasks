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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private List<string> items;
        // SqlConnection con = new SqlConnection();

        // SqlCommand cmd = new SqlCommand();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var connectionString = "Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DB_NameTextBox.Text + ";Integrated Security=true;";
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
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(DB_NameTextBox.Text))
                {
                    MessageBox.Show("Database name is required!");
                }

                else
                {
                    MessageBox.Show("Please Enter the right Database!!");
                }

                //MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                if (ComboBox.SelectedIndex == 1)

                {
                    UsernameTextBox.IsEnabled = false;
                    PasswordBox.IsEnabled = false;

                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DB_NameTextBox.Text + ";Integrated Security=true;");

                    // MessageBox.Show("Enter your Server Name and Database Name");
                    con.Open();
                    con.Close();

                }

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                else if (ComboBox.SelectedIndex == 0)
                {
                    UsernameTextBox.IsEnabled = true;
                    PasswordBox.IsEnabled = true;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AUDNFC2;Initial Catalog=" + DB_NameTextBox.Text + ";Integrated Security=false; User Id=" + UsernameTextBox.Text + ";Password=" + PasswordBox.Password + ";");
                    con.Open();

                    // MessageBox.Show("Enter your Server Name , Database Name, UserName and Password");
                    con.Close();


                }

                else
                {
                    MessageBox.Show("something wrong!");
                }
            }

            catch (Exception ex)
            {


                //  MessageBox.Show(ex.Message);
            }


            // int Combobox_Process_Selected_Index = ComboBox.SelectedIndex;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // MessageBox.Show("Enter your Server Name , Database Name, UserName and Password");
        }

        public void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            /* for current window print 
             try
             {
                 PrintDialog printDlg = new PrintDialog();
                 if (printDlg.ShowDialog() == true)
                 {

                     printDlg.PrintVisual(this, "Window Printing.");

                 }

                 else
                 {

                     updateBtn.Visibility = Visibility.Hidden;
                     printBtn.Visibility = Visibility.Hidden;

                 }

             }

             catch (Exception ex)
             {

                 MessageBox.Show(ex.Message);
             }
         ----------------------------------------------------------------------------------------------------------*/


            try{
                //var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = filePath + "/test.pdf";
                Document pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument,
                         new FileStream(path, FileMode.Create));
                // FileStream outputFileStream = File.Open(path, FileMode.Create);
                pdfDocument.Open();
                pdfDocument.Add(new iTextSharp.text.Paragraph("Here is a test of creating a PDF"));
                pdfDocument.Close();
              //  MessageBox.Show("created");
                Process.Start(path);


            }

            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
                    folderDialog.ShowNewFolderButton = false;
                    folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    // WinForms.DialogResult result = folderDialog.ShowDialog();
                    String sPath = folderDialog.SelectedPath;
                    DirectoryInfo folder = new DirectoryInfo(sPath);

                    /* if (result == WinForms.DialogResult.OK)
                     {
                         //----< Selected Folder >----
                         //< Selected Path >
                         String sPath = folderDialog.SelectedPath;
                         // tbxFolder.Text = sPath;
                         //</ Selected Path >
                         String sPath = folderDialog.SelectedPath;
                         //--------< Folder >--------
                         DirectoryInfo folder = new DirectoryInfo(sPath);
                         if (folder.Exists)
                         {
                             //------< @Loop: Files >------
                             foreach (FileInfo fileInfo in folder.GetFiles())
                             {
                                 //----< File >----
                                 //String sDate = fileInfo.CreationTime.ToString("yyyy-MM-dd");
                                 Debug.WriteLine("#Debug: File: " + fileInfo.Name);
                                 //----</ File >----
                             }
                         }
                     }
                     */
                    var path = sPath + "/test.pdf";
                    Document pdfDocument = new Document();
                    PdfWriter.GetInstance(pdfDocument,
                             new FileStream(path, FileMode.Create));
                    // FileStream outputFileStream = File.Open(path, FileMode.Create);
                    pdfDocument.Open();
                    pdfDocument.Add(new iTextSharp.text.Paragraph("Here is a test of creating a PDF"));
                    pdfDocument.Close();
                    //  MessageBox.Show("created");
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
            // FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
           // dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //openFileDialog.FilterIndex = 2;
            // openFileDialog.RestoreDirectory = true;
            filePath = dialog.SelectedPath;  // file path
        }
       
    }    
}


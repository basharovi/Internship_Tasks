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
using Image = System.Windows.Controls.Image;

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

                        CreatePdf(path);
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

                    CreatePdf(path);
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        string filePath = string.Empty;

        public void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            filePath = dialog.SelectedPath;  // file path
        }

        private void CreatePdf(string path)
        {
            Document pdfDocument = new Document(PageSize.A4);
            try
            {
                var writer = PdfWriter.GetInstance(pdfDocument, new FileStream(path, FileMode.Create));

                pdfDocument.Open();

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

                var button = new PdfPTable(3); //here used Nested Table 
                button.SetTotalWidth(new float[] { 150, 300, 150 });
                table.DefaultCell.FixedHeight = 45;
                table.DefaultCell.BorderColor = new BaseColor(255, 255, 255);
                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 45;
                button.AddCell(cell);
                cell = new PdfPCell(new Phrase("Update"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(221, 221, 221);
                cell.FixedHeight = 45;
                cell.PaddingTop = 15;
                button.AddCell(cell);
                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 45;
                button.AddCell(cell);
                table.AddCell(button);

                button = new PdfPTable(1); //here used Nested Table
                button.SetTotalWidth(new float[] { 300 });
                cell = new PdfPCell(new Phrase("PDF"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(221, 221, 221);
                cell.FixedHeight = 45;
                cell.PaddingTop = 15;
                button.AddCell(cell);
                table.AddCell(button);

                button = new PdfPTable(3); //here used Nested Table
                button.SetTotalWidth(new float[] { 150, 300, 150 });
                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 45;
                button.AddCell(cell);
                cell = new PdfPCell(new Phrase("Select Location"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(221, 221, 221);
                cell.FixedHeight = 45;
                cell.PaddingTop = 7;
                button.AddCell(cell);
                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 45;
                button.AddCell(cell);
                table.AddCell(button);

                pdfDocument.Add(table);
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, false);
                BaseColor bc = new BaseColor(0, 0, 0, 45);
                Font times = new Font(bfTimes, 100F, Font.ITALIC, bc);
                // Dim wfont = New Font(BaseFont.TIMES_ROMAN, 1.0F, BaseFont.COURIER, BaseColor.LIGHT_GRAY)
                ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase("Hossain", times), 297, 550, -70);
               //End water mark
                pdfDocument.Close();
            }
            catch (Exception)
            {
                pdfDocument.Close();
            }
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

                    conncetionString = conncetionString + " User Id=" + UsernameTextBox.Text + "; Password=" + PasswordBox.Password + "";
                }
            }
            catch (Exception)
            {

            }
            return conncetionString;
        }
    }
}


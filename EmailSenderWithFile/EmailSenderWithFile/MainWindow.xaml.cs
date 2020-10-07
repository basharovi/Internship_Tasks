using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace EmailSenderWithFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    
       private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("hossainahmed162012@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mail.From.Address, "@hossain@");
                smtp.Host = "smtp.gmail.com";

                //recipient
                mail.To.Add(new MailAddress(toMailTextBox.Text));

                mail.IsBodyHtml = true;

                mail.Subject = subjectTextBox.Text;
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.*";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    attachmentTextBox.Text = dlg.FileName;

                    mail.Attachments.Add(new Attachment(attachmentTextBox.Text));
                }
                mail.Body = bodyTextBox.Text;
                smtp.Send(mail);

                MessageBox.Show("Your Mail is sended");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

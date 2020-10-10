using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;

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
            //DESKTOP-AUDNFC2
            //192.168.1.136

            // = (Dns.GetHostName());
        
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("hossain@perkyrabbit.com");
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "mail.perkyrabbit.com";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("hossain@perkyrabbit.com", "01779432824@@");             
          
                mail.To.Add(new MailAddress("ovi@perkyrabbit.com"));

                mail.IsBodyHtml = true;
                mail.Subject = subjectTextBox.Text;              
                mail.Body = bodyTextBox.Text;
                smtp.Send(mail);

                MessageBox.Show("Your Mail Sent!!!!!");
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

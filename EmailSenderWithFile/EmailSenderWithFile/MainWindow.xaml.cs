using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Specialized;
using MailBee;
using MailBee.SmtpMail;
using MailBee.ImapMail;

namespace EmailSenderWithFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LinkedResource inline;

        public MainWindow()
        {
            InitializeComponent();
        }
    
       private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            var imapComponent = new Imap("MN120-35FDFD54FDFEFD4FFD565FC6E013-CF63");
            Smtp mailer = new Smtp();

            // Use SMTP relay with authentication.
            SmtpServer server = mailer.SmtpServers.Add(
                "mail.perkyrabbit.com", "hossain@perkyrabbit.com", "01779432824@@");

            // Use alternate SMTP port.
            server.Port = 587;

            mailer.SmtpServers.Add(server);

            

            // Compose and send simple e-mail.
            mailer.From.Email = "hossain@perkyrabbit.com";
            mailer.To.Add("ovi@perkyrabbit.com");
            mailer.Subject = "Test message";
            mailer.BodyPlainText = "testing mail from web mail";
            mailer.Send();
            MessageBox.Show("sent");
        }

        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


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
            try
            {
                var linkedResource = new LinkedResource(@"C:\Users\Hossain\Downloads\Download\MailLogo.png", MediaTypeNames.Image.Jpeg);
                // this task also can be done by aspose reference/pakage.              
                //var htmlBody = $"{bodyTextBox.Text}<img height = 100px width = 100px src=\"cid:{linkedResource.ContentId}\"/>";
                var htmlBody = "Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out print <a href='https://www.google.com/' target='_blank'> Google </a>, graphic or web designs. The passage is attributed to an unknown typesetter in the 15th century who is thought to have scrambled parts of Cicero's De Finibus Bonorum et Malorum for use in a type specimen book.";
               // htmlBody = $"<img height = 100px width = 100px src=\"cid:{linkedResource.ContentId}\"/>";
                var alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(linkedResource);
                var mail = new MailMessage()
                {
                    AlternateViews = { alternateView }
                };
             
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
                
                smtp.Send(mail);

                MessageBox.Show("Your Mail sent");
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

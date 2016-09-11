using System;
using System.IO;
using System.Net.Mail;

namespace emailQuotes
{

  
     /*
  *
  * Creating a small console app to send random quotes via email daily. 
  * 
 */

    class Program
    {

        static string emailMessage;

        static void Main(string[] args)
        {
            // file info
            string fileDirectory = Properties.Settings.Default.directoryPath;
            string[] fileArray = Directory.GetFiles(fileDirectory);
            // variables to select file and quote
            var guess = new Random();

            if (fileArray != null)
            {
                int category = guess.Next(fileArray.Length);
                
                var linePicker = File.ReadAllLines(fileArray[category]);
                category = guess.Next(linePicker.Length);
                emailMessage = linePicker[category].ToString();
               
            }

            sendhotMail(emailMessage);

        }

        static void sendhotMail(string message)
        {
          
            MailMessage mMailMessage = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.live.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("<YourhotmailEmail@msn.com", "<YourPassword>"); // Change to your credentials.And remove the <>!
            mMailMessage.Subject = "Good Morning";
            mMailMessage.Body = message;
            mMailMessage.From = new MailAddress("<Youremail>");
            mMailMessage.To.Add(new MailAddress("<SendEmailAddress>")); //TODO - make this a variable
            // mMailMessage.Bcc.Add(new MailAddress("<ExtraEmailsAddress>")); // You can CC or Blind Copy as well
            client.Send(mMailMessage);
        }

        static void sendXChangeMail(string message)
        {
            MailMessage mMailMessage = new MailMessage();
            SmtpClient client = new SmtpClient("smtpmail.<exchangeServer>");
            mMailMessage.Subject = "Good Morning";
            mMailMessage.Body = message;
            mMailMessage.From = new MailAddress("FromEmailName@YourDomainn.com");
            mMailMessage.To.Add(new MailAddress("Deliverto@ymail.com")); //TODO - make this a variable
            // mMailMessage.To.Add(new MailAddress("moreEmailAddress@domain.com));
            client.Send(mMailMessage);
        }

    }


}






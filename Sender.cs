using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Hitachi_Solutions_Task
{
    public class Sender
    {
        //Here we send an email with the appropriate day and with the new .csv file
        public void SendEmail(string senderEmail, string senderPassword, string recieverEmail, Day mostAppropriateDay)
        {
            
            //We use this string builder for the body of the email
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The most appropriate day is: ");
            sb.AppendLine($"Day {mostAppropriateDay.Id}: Temperature = {mostAppropriateDay.Temperature}, Wind = {mostAppropriateDay.Wind}, Humidity = {mostAppropriateDay.Humidity}, Precipitation = {mostAppropriateDay.Precipitation}, Lightning = No, Clouds = {mostAppropriateDay.Clouds}");

            //Here we create the SMTP client, we use gmail
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            
            string subject = "Most appropriate day";
            string body = sb.ToString().TrimEnd();
            MailMessage mailObj = new MailMessage(senderEmail, recieverEmail, subject, body);
            
            //Here we attach the new csv file
            Attachment attachment = new Attachment("WeatherReport.csv", new System.Net.Mime.ContentType("text/csv"));
            attachment.Name = "WeatherReport.csv";
            mailObj.Attachments.Add(attachment);
            //And here we (try to) send the email.
            try
            {
                Console.WriteLine("Sending email...");
                email.Send(mailObj);
                Console.WriteLine("Email sent");
            }
            catch(SmtpException e)
            {
                Console.WriteLine("Error! Email not send.");
            }
        }
        //Here we send an email only with the new .csv file
        public void SendEmailWithNoAppropriateDay(string senderEmail, string senderPassword, string recieverEmail)
        {
            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            string subject = "Most appropriate day";
            string body = "There was no appropiate day found!";
            MailMessage mailObj = new MailMessage(senderEmail, recieverEmail, subject, body);
            Attachment attachment = new Attachment("WeatherReport.csv", new System.Net.Mime.ContentType("text/csv"));
            attachment.Name = "WeatherReport.csv";
            mailObj.Attachments.Add(attachment);
            try
            {
                Console.WriteLine("Sending email...");
                email.Send(mailObj);
                Console.WriteLine("Email sent.");
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Error! Email not send.");
            }
        }
    }
}

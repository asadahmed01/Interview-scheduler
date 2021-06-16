using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;


namespace AppointmentBooking.Data
{
    public class EmailServices : IEmailServices
    {
        public void SendMail(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("asad.awaare@gmail.com", "Awaare@990");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

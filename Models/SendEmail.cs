using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Tema2.Models
{
    public class SendEmail
    {
        public void SEmail(string email, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("email", "pass");

                MailMessage emailMessage = new MailMessage();
                emailMessage.From = new MailAddress("email");
                emailMessage.Subject = subject;
                emailMessage.Body = body;
                emailMessage.To.Add(email);
                emailMessage.IsBodyHtml = true;
                smtpClient.Send(emailMessage);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

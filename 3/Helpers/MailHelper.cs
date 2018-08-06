using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace _3.Helpers
{
    public class MailHelper
    {
        public static void SendMail(string to, string subject, string body)
        {
            var message = new MailMessage(ConfigurationManager.AppSettings["sender"], to)
            {
                Subject = subject,
                Body = body
            };

            var smtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["sender"],
                    ConfigurationManager.AppSettings["passwd"]
                    ),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
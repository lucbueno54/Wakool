using Revisao.LogConfing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionControl.LogConfing
{
    public class SendEmailWay
    {
        public void SendEmailWAy(Email email, string error)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = "<p style='font-size: 12px;color: red;'>Erros</p>";
                mail.IsBodyHtml = true;
                mail.Subject = email.Subject;
                mail.Body = error;

                foreach (string item in email.EmailDestinatarios)
                {
                    mail.To.Add(item);
                }

                mail.From = new MailAddress("TRABALHOENTRA21@GMAIL.COM");
                SmtpClient servidor = new SmtpClient("smtp.gmail.com", 587);
                servidor.EnableSsl = true;
                servidor.UseDefaultCredentials = false;
                servidor.Credentials = new NetworkCredential("trabalhoentra21@gmail.com", "csharp>java");
                servidor.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

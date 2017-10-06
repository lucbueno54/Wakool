using ExceptionControl.LogConfing;
using Revisao.LogConfing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Revisao.LogConfing
{
    public class SendEmail
    {
        public SendEmail(string error)
        {
            Email email = new Email();
            email = new TakeXML().TkXML(email);

            try
            {
                SendEmailWay sendemailway = new SendEmailWay();
                sendemailway.SendEmailWAy(email, error);
            }
            catch (Exception ex)
            {
                ExceptionText extxt = new ExceptionText(ex);
            }
        }
    }
}

/*<?xml version="1.0"?>
<Email xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<Assunto>Cali lindo d+</Assunto>
<EmailDestinatarios>
<string>marcelo.bernart@gmail.com</string>
<string>calielyangcs@gmail.com</string>
</EmailDestinatarios>
<DestinatarioAnexo>
<string>om_02.png</string>
<string>space-galaxy-wallpaper-HD6.jpg</string>
</DestinatarioAnexo>
</Email>
*/

using Revisao.LogConfing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ExceptionControl.LogConfing
{
    public class TakeXML
    {
        public Email TkXML(Email email)
        {
            try
            {
                email = new Email() { Subject = "te" };
                string[] data = Directory.GetFiles(@"C:\Users\Public");
                XmlSerializer xml = new XmlSerializer(typeof(Email));
                FileStream fs = new FileStream(@"C:\Users\Public\products.xml", FileMode.Create);
                email = new Email();
                xml.Serialize(fs, email);
                string temp = string.Empty;
                foreach (var item in data)
                {
                    if (item.ToLower().Trim().Contains("c:\\users\\public\\configlog.xml"))
                        temp = item;
                }

                FileStream fss = new FileStream(temp, FileMode.Open);
                email = (Email)xml.Deserialize(fss);
                fs.Dispose();
            }
            catch (Exception ex)
            {

            }
            return email;
        }
    }
}

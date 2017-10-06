using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revisao.LogConfing
{
    public class Email
    {
        public string Subject { get; set; }
        public List<String> EmailDestinatarios { get; set; }
        public List<String> AnexoDestionatarios { get; set; }

        public Email()
        {
            EmailDestinatarios = new List<string>();
            AnexoDestionatarios = new List<string>();
        }
    }
}

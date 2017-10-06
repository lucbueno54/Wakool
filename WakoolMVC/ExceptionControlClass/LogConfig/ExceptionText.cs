using Revisao.LogConfing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionControl.LogConfing
{
    public class ExceptionText
    {
        public ExceptionText(Exception ex)
        {
            string errorFormat = String.Format("Data do ERRO: {0} <br> Mensagem de ERRO: {1} <br> Valores da exceção: {2} <br> Códificação do ERRO: {3} <br> Link referente ao ERRO: {4}", DateTime.Now.ToString(), ex.Message, ex.Data, ex.HResult, ex.HelpLink);
            string dataLog = String.Format("log{0}", DateTime.Now.ToString()).Replace('/', '_').Replace('.', '_').Replace(':', '_').Replace(' ', '_');
            string path = @"C:\Users\Public\WakoolErrors";
            if (!Directory.Exists(path))            
                Directory.CreateDirectory(path);            
            File.AppendAllText((path + "\\" + dataLog), errorFormat);
            new SendEmail(errorFormat);
        }
    }
}

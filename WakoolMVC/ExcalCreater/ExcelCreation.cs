using ExceptionControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelCreator
{
    public class ExcelCreation
    {
        public static void ListToExcel(List<object> list, string nome, Type type)
        {
            try
            {
                //Verifica se o Excel está instalado ná maquina.
                #region Excel Verification
                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                    throw new Exception("Excel is not properly installed!");

                #endregion

                //Adiciona e formata o conteudo ao arquivo Excel.
                #region Excel Creator

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object missValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(missValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                PropertyInfo[] property = type.GetProperties();

                for (int i = 0; i < property.Length; i++)
                {
                    xlWorkSheet.Cells[1, (i + 1)] = property[i].Name;
                }

                int j = 0;
                if(list.Count >0)
                    
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 0)
                        j = 2;
                    else
                        j = j + 1;

                    for (int w = 0; w < property.Length; w++)
                    {
                        string stringDecente = string.Empty;
                            var temp = property[w].GetValue(list[i]);
                            stringDecente = (temp != null) ? temp.ToString() : string.Empty;
                        if(!string.IsNullOrWhiteSpace(stringDecente))
                        if (stringDecente[0] == '0')
                        {
                            stringDecente = "'" + stringDecente;
                        }
                        xlWorkSheet.Cells[j, (w + 1)] = stringDecente;
                    }
                }
                //missValue = Quando não queremos passar o parametro colocamos MissValue para preencher a quantidade de parametros que o método pede, só isso...
                xlWorkBook.SaveAs(nome, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlWorkBook.Close(true, missValue, missValue);
                xlApp.Quit();

                #endregion
                //Fecha os processos Excel para não haver bugs para a execução.
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
        }
    }
}

using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;//Biblioteca iTextSharp e as suas funções
using iTextSharp.text;//Estensão 1 (Text)
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf.draw;//Estensão 2 (PDF)
using MetadataDiscover;

namespace PDFCreator
{
    public class PDFCreation
    {
        public static void BuildPDF(List<object> data, string name, Type type)
        {
            try
            {
                //Cria o arquivo pdf no caminho de destino.
                #region PDF Creator

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(name, FileMode.Create));
                document.Open();

                #endregion

                //Adiciona o nome e a fonte que irá ser usada.
                #region PDF Namer

                var docTitle = new Paragraph("Relatório de " + AssemblyUtils.GetEntityDisplayAttribute(type));
                docTitle.Alignment = Element.ALIGN_CENTER;
                docTitle.SpacingBefore = 50;
                var fontTitle = FontFactory.GetFont("Times New Roman", 10, BaseColor.WHITE);
                docTitle.Font = fontTitle;
                document.Add(docTitle);
                PropertyInfo[] property = type.GetProperties();
                var zebraFont = FontFactory.GetFont("Times New Roman", 7, BaseColor.BLACK);
                var zebraFont2 = FontFactory.GetFont("Times New Roman", 7, BaseColor.WHITE);

                #endregion

                //Cria e formata a tabela e adiciona o conteúdo.
                #region PDF Formater

                PdfPTable table = new PdfPTable(property.Length);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SpacingBefore = 30;

                foreach (var item in property)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.AddElement(new Phrase(new Chunk(item.Name, zebraFont2)));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(0, 0, 0);
                    cell.UseVariableBorders = true;
                    cell.BorderColorLeft = BaseColor.BLACK;
                    cell.BorderColorRight = BaseColor.BLACK;
                    cell.BorderColorTop = BaseColor.BLACK;
                    table.AddCell(cell);
                }

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < property.Length; j++)
                    {
                        if (i % 2 == 0)
                        {
                            string cellString  = string.Empty;
                            var temp = property[j].GetValue(data[i]);
                            if (temp != null)
	                          cellString = temp.ToString();
                            PdfPCell cell = new PdfPCell();
                            cell.AddElement(new Phrase(new Chunk(cellString, zebraFont)));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 242, 242);
                            cell.UseVariableBorders = true;
                            cell.BorderColorLeft = BaseColor.BLACK;
                            cell.BorderColorRight = BaseColor.BLACK;
                            cell.BorderColorTop = BaseColor.BLACK;
                            cell.BorderColorBottom = BaseColor.BLACK;
                            table.AddCell(cell);
                        }
                        else
                        {
                             string cellString  = string.Empty;
                            var temp = property[j].GetValue(data[i]);
                            if (temp != null)
                                cellString = temp.ToString();
                            PdfPCell cell = new PdfPCell();
                            cell.AddElement(new Phrase(new Chunk(cellString, zebraFont2)));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 51, 51);
                            cell.UseVariableBorders = true;
                            cell.BorderColorLeft = BaseColor.WHITE;
                            cell.BorderColorRight = BaseColor.WHITE;
                            cell.BorderColorTop = BaseColor.WHITE;
                            cell.BorderColorBottom = BaseColor.WHITE;
                            table.AddCell(cell);
                        }
                    }
                }

                #endregion

                //Adiciona a tabela ao arquivo PDF e a fecha.
                document.Add(table);
                document.Close();
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
        }
    }
}

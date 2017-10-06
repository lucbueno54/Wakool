using MetadataDiscover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Globalization;
namespace Guid.Web
{
    public class EntityModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            string entityName =
                request.Form.Get("EntityType");
            Type type = AssemblyUtils.GetEntityByName(entityName);

            object obj = Activator.CreateInstance(type);

            foreach (PropertyInfo propriedade in type.GetProperties())
            {
                if (propriedade.PropertyType == typeof(string))
                {
                    string text = request.Form.Get(propriedade.Name);
                    text = propriedade.RemoveMask(text);
                    propriedade.SetValue(obj, text);
                }
                else if (propriedade.PropertyType == typeof(Boolean))
                {
                    bool temp = false;
                    if ((request.Form.Get(propriedade.Name)) != null)
                    {
                        temp = (request.Form.Get(propriedade.Name).ToString().ToUpper() == "ON") ? true : false;
                    }
                    propriedade.SetValue(obj, temp);
                }
                else if (propriedade.PropertyType.IsEnum || propriedade.PropertyType == typeof(int))
                {
                    int i = 0;
                    object x = ((request.Form.Get(propriedade.Name)));
                    int.TryParse((request.Form.Get(propriedade.Name)).ToString(), out i);
                    Object o = (request.Form.Get(propriedade.Name));
                    propriedade.SetValue(obj, i);
                }
                else if (propriedade.PropertyType == typeof(double))
                {
                    double temp = default(double);
                    string i = request.Form.Get(propriedade.Name).Replace(".", ",");
                    double.TryParse(i, out temp);
                    propriedade.SetValue(obj, temp);
                }
                else if (propriedade.PropertyType == typeof(decimal))
                {
                    decimal temp = default(decimal);
                    string i = request.Form.Get(propriedade.Name).Replace(".", ",");
                    decimal.TryParse(i, out temp);
                    propriedade.SetValue(obj, temp);
                }
                else if (propriedade.PropertyType == typeof(DateTime))
                {
                    DateTime datatemp = default(DateTime);
                    string[] data = request.Form.Get(propriedade.Name).Split('-');
                    if (!string.IsNullOrWhiteSpace(data[0]))
                        if (!string.IsNullOrWhiteSpace(data[2]) && !string.IsNullOrWhiteSpace(data[1]) && !string.IsNullOrWhiteSpace(data[0]))
                        {
                            string temp = data[2] + "/" + data[1] + "/" + data[0] + " 00:00:00";
                            DateTime.TryParse(temp, out datatemp);
                        }
                    propriedade.SetValue(obj, datatemp);
                }
            }
            return obj;
        }
    }
}
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Extensions
{
    static class SqlExtensions
    {
        public static object ToObject(this DataRow row, Type type)
        {
            object item = Activator.CreateInstance(type);
            try
            {
                int i = 0;
                foreach (PropertyInfo property in type.GetProperties())
                {
                    property.SetValue(item, row[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return item;
        }
        public static object ToObjectCollection<T>(this DataTable table, Type type)
        {
            Type typeList = typeof(List<>);
            var constructedTypeList = typeList.MakeGenericType(type);
            var instance = Activator.CreateInstance(constructedTypeList);

            foreach (DataRow row in table.Rows)
            {
                instance.GetType().GetMethod("Add").Invoke(instance, new object[] { ToObject(row, type) });
            }
            return instance;
        }
    }
}

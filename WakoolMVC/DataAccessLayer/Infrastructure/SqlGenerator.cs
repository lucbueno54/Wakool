
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wakool.DataAnnotations;
using Entity;
using MetadataDiscover;

namespace DataAccessLayer.Infrastructure
{
    public class SqlGenerator<T> where T : EntityBase
    {

        private static string GetTableName<T>(Type type)
        {
            return AssemblyUtils.GetPropertyTableNameAttribute(type);
        }

        #region Insert
        public static SqlCommand BuildInsertCommand(T item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2})", GetTableName<T>(item.GetType()), GetInsertFields<T>(item.GetType(), false), GetInsertFields<T>(item.GetType(), true));
            SqlCommand command = new SqlCommand();
            command.CommandText = builder.ToString();
            GenerateInsertParameters(command, item);
            return command;
        }
        private static void GenerateInsertParameters(SqlCommand command, T item)
        {
            foreach (PropertyInfo propriedade in item.GetType().GetProperties())
            {
                if (propriedade.Name != "ID")
                {
                    command.Parameters.AddWithValue("@" + propriedade.Name, propriedade.GetValue(item));
                }
            }
        }
        private static string GetInsertFields<T>(Type type, bool isParameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in type.GetProperties())
            {
                if (item.Name != "ID")
                {
                    if (isParameters)
                    {
                        builder.Append("@" + item.Name + ",");
                    }
                    else
                    {
                        builder.Append(item.Name + ",");
                    }
                }
            }
            builder = builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
        #endregion

        #region Update

        public static SqlCommand BuildUpdateCommand(T item)
        {
            StringBuilder builder = new StringBuilder();
            var variable = item.GetType();
            builder.AppendFormat("UPDATE {0} SET {1} WHERE ID = @ID", GetTableName<T>(item.GetType()), GetUpdateFields<T>(item.GetType()));
            SqlCommand command = new SqlCommand();
            command.CommandText = builder.ToString();
            GenerateUpdateParameters(command, item);
            return command;
        }

        private static string GetUpdateFields<T>(Type type)
        {
            StringBuilder builder = new StringBuilder();
            foreach (PropertyInfo item in type.GetProperties())
            {
                if (item.GetCustomAttribute<NonEditable>() == null && item.Name.ToUpper() != "ID")
                {
                    builder.Append(item.Name + " = @" + item.Name + ",");
                }
            }
            return builder.ToString(0, builder.Length - 1);
        }

        private static void GenerateUpdateParameters(SqlCommand command, object item)
        {
            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                if (property.GetCustomAttribute<NonEditable>() == null)
                {
                    command.Parameters.AddWithValue
                        ("@" + property.Name, property.GetValue(item));
                }
            }
        }

        #endregion

        #region Delete

        public static SqlCommand BuildDeleteCommand(T item)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = string.Format("DELETE FROM {0} WHERE ID = @ID", GetTableName<T>(item.GetType()));
            command.Parameters.AddWithValue("@ID", item.ID);
            return command;
        }

        #endregion
    }
}

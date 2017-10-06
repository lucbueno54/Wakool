using DatabaseCreator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MetadataDiscover;
using System.Data.SqlClient;
using System.Data;
using Wakool.DataAnnotations;
using DataAccessLayer;

namespace DatabaseCreator.Infra
{
    public class TableCreator : ICreateTable
    {
        public string DataTypeMapping(PropertyInfo dataType)
        {
            return DataType(dataType);
        }

        public bool CheckIfTableExists(Type tableType)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\Public");
            string connectionString = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFileName=C:\Users\Public\{0};Integrated Security=True;Connect Timeout=30", DatabaseConfig.DatabaseName);
            string tableName = AssemblyUtils.GetPropertyTableNameAttribute(tableType);
            string ifExists = @"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table) SELECT 1 ELSE SELECT 0";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(ifExists, connection))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(ifExists, connection);
                        cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = tableName;
                        int exists = (int)cmd.ExecuteScalar();
                        if (exists == 1)
                            return true;
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
                return true;
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
                return true;
            }
            return false;
        }

        public void TableManagement()
        { TableExecuter(CreateSqlTable());
        }

        public string CreateSqlTable() {
            return SqlTabelCreator(AssemblyUtils.GetEntities()).ToString(); 
        }

        private StringBuilder SqlTabelCreator(List<Type> items)
        {
            StringBuilder builder = new StringBuilder();
            builder.Clear();

            foreach (Type item in items)
            {
                if (!this.CheckIfTableExists(item))
                {
                    builder.Append(string.Format("CREATE TABLE {0} (", AssemblyUtils.GetPropertyTableNameAttribute(item).Trim().ToUpper()));
                    foreach (PropertyInfo property in item.GetProperties())
                    {
                        builder.Append(string.Format(" {0} {1} {2} ,", property.Name, DataTypeMapping(property), RequiredAttribute(property)));
                    }
                    builder.Append(");");
                }
            }
            return builder;
        }

        public bool TableExecuter(string sql)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    new DataAccessLayer.Infrastructure.DbExecuter().Execute(new SqlCommand(sql));
                    return true;
                }
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
            return false;
        }

        public string RequiredAttribute(System.Reflection.PropertyInfo property)
        {
            try
            {
                if (property.Name == "ID" && property.PropertyType == typeof(int))
                    return "";
                return (property.GetCustomAttribute<RequiredAttribute>() != null) ? " NOT NULL " : " NULL ";
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
            return " NOT NULL ";
        }

        public string DataType(PropertyInfo property)
        {
            try
            {
                if (property.Name == "ID" && property.PropertyType == typeof(int))
                    return " INT PRIMARY KEY IDENTITY ";

                switch (Type.GetTypeCode(property.PropertyType))
                {
                    case TypeCode.Int32: return " INT ";
                    case TypeCode.Boolean: return " BIT ";
                    case TypeCode.Double: return " FLOAT ";
                    case TypeCode.DateTime: return " DATETIME ";
                    case TypeCode.Decimal: return " DECIMAL (18,2) ";
                    case TypeCode.String:
                        if (property.GetCustomAttribute<StringLengthAttribute>() != null)
                            return string.Format(" VARCHAR({0}) ", property.GetCustomAttribute<StringLengthAttribute>().MaximumLength);
                        return " VARCHAR(MAX) ";
                    default: return " VARCHAR(MAX) ";
                }
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
                return " VARCHAR(MAX) ";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreator.Interfaces
{
    public interface ICreateTable
    {
        string DataTypeMapping(PropertyInfo dataType);
        bool CheckIfTableExists(Type tableType);
        void TableManagement();
        string CreateSqlTable();
        bool TableExecuter(string sql);
        string RequiredAttribute(PropertyInfo property);
        string DataType(PropertyInfo type);
    }
}

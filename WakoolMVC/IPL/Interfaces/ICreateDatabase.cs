using DatabaseCreator.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreator.Interfaces
{
    public interface ICreateDatabase
    {
        void CreateSqlFile();
        void CreateSqlDatabase(bool check);
    }
}

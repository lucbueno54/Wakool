using DatabaseCreator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreator.Infra
{
    public class DatabaseCreator : ICreateDatabase
    {
        public void CreateSqlFile()
        {
            try
            {
                string fileName = System.IO.Path.Combine(@"C:\Users\Public", DatabaseConfig.DatabaseName);
                DatabaseConfig.FileName = fileName;
                if (!System.IO.File.Exists(fileName))
                    CreateSqlDatabase(DatabaseConfig.DatabaseCreator);
                DatabaseConfig.DatabaseCreator = true;
                DatabaseConfig.DatabaseChecker = true;
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
        }

        public void CreateSqlDatabase(bool check)
        {
            try
            {
                string databaseName = System.IO.Path.GetFileNameWithoutExtension(DatabaseConfig.FileName);
                using (var connection = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\\v11.0;Initial Catalog=master; Integrated Security=true;"))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = String.Format("CREATE DATABASE {0} ON PRIMARY (NAME={0}, FILENAME='{1}')", databaseName, DatabaseConfig.FileName);
                        command.ExecuteNonQuery();
                        command.CommandText = String.Format("EXEC sp_detach_db '{0}', 'true'", databaseName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
        }
    }
}

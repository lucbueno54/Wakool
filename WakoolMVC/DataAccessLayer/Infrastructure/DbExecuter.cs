using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class DbExecuter
    {
        private ConnectionHelper helper = new ConnectionHelper();

        public int Execute(SqlCommand command)
        {
            using (helper)
            {
                helper.Setup(command);
                return command.ExecuteNonQuery();
            }
        }

        public DataTable GetData(SqlCommand command)
        {
            using (helper)
            {
                helper.Setup(command);
                DataTable table = new DataTable();
                SqlDataReader dt = command.ExecuteReader();
                table.Load(dt);
                return table;
            }
        }
    }
}

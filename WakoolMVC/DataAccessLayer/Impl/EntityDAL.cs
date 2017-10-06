using DataAccessLayer.Infrastructure;
using Entity;
using Wakool.DataAnnotations;
using MetadataDiscover;
using System;
using System.Collections.Generic;
using ExceptionControl;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Infrastructure.Extensions;

namespace DataAccessLayer.Impl
{
    public class EntityDAL<T> where T : EntityBase
    {
        public void Insert(T item)
        {
            if (item != null)
                new DbExecuter().Execute(SqlGenerator<T>.BuildInsertCommand(item));
        }

        public int Update(T item)
        {
            if (item != null)
                return new DbExecuter().Execute(SqlGenerator<T>.BuildUpdateCommand(item));
            return 0;
        }

        public int Delete(T item)
        {
            if (item != null)
                return new DbExecuter().Execute(SqlGenerator<T>.BuildDeleteCommand(item));
            return 0;
        }

        public EntityBase GetByID(T nome, int id, Type type)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = string.Format("SELECT * FROM {0} WHERE ID = @ID", (AssemblyUtils.GetPropertyTableNameAttribute(type)));
                command.Parameters.AddWithValue("@ID", id);
                DataTable table = new DbExecuter().GetData(command);

                if (table.Rows.Count == 0)
                    return null;
                return (EntityBase)table.Rows[0].ToObject(type);
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
            return new EntityBase();
        }

        public object GetAll(Type type)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = string.Format("SELECT * FROM {0}", AssemblyUtils.GetPropertyTableNameAttribute(type).ToUpper());
                DataTable table = new DbExecuter().GetData(command);
                if (table.Rows.Count == 0)
                    return null;
                return table.ToObjectCollection<T>(type);
            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
            return new EntityBase();
        }
    }
}

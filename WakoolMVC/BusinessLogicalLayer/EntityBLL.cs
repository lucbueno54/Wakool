using ExceptionControl;
using BusinessLogicalLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entity;
using BusinessLogicalLayer;
using System.Collections;
using DatabaseCreator.Infra;

namespace BusinessLogicalLayer
{
    public class EntityBLL
    {
        private void ConfigDB()
        {
            try
            {
                if (!DatabaseCreator.Infra.DatabaseConfig.DatabaseCreator)
                    new DatabaseCreator.Infra.DatabaseCreator().CreateSqlFile();

                if (!DatabaseConfig.CheckTabel)
                    new DatabaseCreator.Infra.TableCreator().TableManagement();
                DatabaseConfig.CheckTabel = true;
                DatabaseCreator.Infra.DatabaseConfig.DatabaseCreator = true;

            }
            catch (Exception ex)
            {
                new ExceptionControl.LogConfing.ExceptionText(ex);
            }
        }

        public List<object> GetAll(EntityBase entity, Type type)
        {
            ConfigDB();
            object obj = new DataAccessLayer.Impl.EntityDAL<EntityBase>().GetAll(entity.GetType());
            if (obj is IEnumerable && obj != null)
                return ((IEnumerable)obj).Cast<object>().ToList();
            else
            {
                object standard = Activator.CreateInstance(type);
                List<object> listDef = new List<object>() { standard };
                return listDef;
            }
        }

        public object GetById(EntityBase entity, Type type)
        {
            return new DataAccessLayer.Impl.EntityDAL<EntityBase>().GetByID(entity, entity.ID, type);
        }

        public bool Delete(EntityBase entity, Type type)
        {
            return ((new DataAccessLayer.Impl.EntityDAL<EntityBase>().Delete(entity)) > 0);
        }

        public bool Update(EntityBase entity)
        {
            return ((new DataAccessLayer.Impl.EntityDAL<EntityBase>().Update(entity)) > 0);
        }

        public bool Insert(EntityBase entity)
        {
            ConfigDB();
            if (entity != null)
            {
                if (entity.ID == 0)
                {
                    new DataAccessLayer.Impl.EntityDAL<EntityBase>().Insert(entity);
                    return true;
                }
                else
                    return this.Update(entity);
            }
            return false;
        }
    }
}

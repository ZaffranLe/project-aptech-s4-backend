using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonicationMemory.Resources
{
    public abstract class EntityBaseSql
    {
        public string Insertsql = "";
        public string Updatesql = "";
        public string Deletesql = "";
        public string Selectsql = "";
        public CommandType CommandTypeProcedure = CommandType.StoredProcedure;
        public CommandType CommandTypeText = CommandType.Text;
        /// <summary>
        /// Get sql script or name of storeProc
        /// </summary>
        /// <returns></returns>
        public string GetSqlInsert()
        {
            return Insertsql;
        }
        /// <summary>
        /// Get sql script or name of storeProc
        /// </summary>
        /// <returns></returns>
        public string GetSqlUpdate()
        {
            return Updatesql;
        }
        /// <summary>
        /// Get sql script or name of storeProc
        /// </summary>
        /// <returns></returns>
        public string GetSqlDelete()
        {
            return Deletesql;
        }
        /// <summary>
        /// Get sql script or name of storeProc
        /// </summary>
        /// <returns></returns>
        public string GetSqlSelectAll()
        {
            return Selectsql;
        }

        public virtual string GetSqlAction(EntityCommand entityAction)
        {
            if (entityAction.EntityAction == EntityAction.Insert)
            {
                return GetSqlInsert();
            }
            if (entityAction.EntityAction == EntityAction.Update)
            {
                return GetSqlUpdate();
            }
            if (entityAction.EntityAction == EntityAction.Delete)
            {
                return GetSqlDelete();
            }
            return null;
        }

        public virtual string GetSqlAction(EntityQuery entityAction)
        {
            if (entityAction.QueryAction == EntityGet.GetAllValues)
            {
                return GetSqlSelectAll();
            }
            return null;
        }

        public abstract SqlCommand GetSqlCommandForInsert(BaseEntity baseEntity);
        public abstract SqlCommand GetSqlCommandForUpdate(BaseEntity baseEntity);
        public abstract SqlCommand GetSqlCommandForDelete(BaseEntity baseEntity);
        public virtual SqlCommand GetSqlCommandForGetAll()
        {
            var sqlcommand = new SqlCommand { CommandType = CommandType, CommandText = Selectsql };
            return sqlcommand;
        }
        public virtual SqlCommand GetSqlCommand(EntityCommand entityAction)
        {
            if (entityAction.EntityAction == EntityAction.Insert)
            {
                return GetSqlCommandForInsert(entityAction.BaseEntity);
            }
            if (entityAction.EntityAction == EntityAction.Update)
            {
                return GetSqlCommandForUpdate(entityAction.BaseEntity);
            }
            if (entityAction.EntityAction == EntityAction.Delete)
            {
                return GetSqlCommandForDelete(entityAction.BaseEntity);
            }
            return null;
        }

        public virtual SqlCommand GetSqlCommand(EntityQuery entityAction)
        {
            if (entityAction.QueryAction == EntityGet.GetAllValues)
            {
                return GetSqlCommandForGetAll();
            }
            return null;
        }
        public abstract List<BaseEntity> PopulateBusinessObjectFromReader(IDataReader dataReader);
        public virtual void UpdateEntityId(EntityCommand entityAction, SqlCommand command)
        {
            if (entityAction.EntityAction == EntityAction.Insert)
            {
                entityAction.BaseEntity = UpdateEntityId(entityAction.BaseEntity, command);
            }
        }
        public abstract BaseEntity UpdateEntityId(BaseEntity baseEntity, SqlCommand command);
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;
using ElectricShop.DatabaseDAL.SqlBuilder;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
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
        public virtual SqlCommand GetSqlCommand(EntityCommand entityAction)
        {
            if (entityAction.EntityAction == EntityAction.Insert)
            {
                return GetSqlCommandForInsert(entityAction.BaseEntity.GetEntity());
            }
            if (entityAction.EntityAction == EntityAction.Update)
            {
                return GetSqlCommandForUpdate(entityAction.BaseEntity.GetEntity());
            }
            if (entityAction.EntityAction == EntityAction.Delete)
            {
                return GetSqlCommandForDelete(entityAction.BaseEntity.GetEntity());
            }
            return null;
        }

        public virtual SqlCommand GetSqlCommandForGetAll()
        {
            var sqlcommand = new SqlCommand { CommandType = CommandTypeProcedure, CommandText = Selectsql };
            return sqlcommand;
        }

        public virtual SqlCommand GetSqlCommandForGetGetCustomValues(DescriptorColection colection, string tableName)
        {
            string selectCustom = @"SELECT * FROM " + tableName + " ";
            if (colection.ListField != null && colection.ListField.Count > 0)
            {
                //Truong hop chi lay 1 vai truong cu the
                var field = "";
                for (int i = 0; i < colection.ListField.Count; i++)
                {
                    if (i == colection.ListField.Count - 1)
                        field += colection.ListField[i]; //Bo qua dau phay cuoi cung
                    else
                        field += colection.ListField[i] + ","; 
                }
                if(!string.IsNullOrEmpty(field))
                    selectCustom = string.Format(@"SELECT {0} FROM " + tableName + " ", field);
            }
            if (colection != null && colection.Descriptors.Count > 0)
            {
                selectCustom += DescriptorUtil.GetWhereString(colection);
            }

            var sqlcommand = new SqlCommand { CommandType = CommandTypeText, CommandText = selectCustom };
            return sqlcommand;
        }
        public virtual SqlCommand GetSqlCommand(EntityQuery entityAction)
        {
            if (entityAction.QueryAction == EntityGet.GetAllValues)
            {
                return GetSqlCommandForGetAll();
            }
            else if (entityAction.QueryAction == EntityGet.GetCustomValues)
            {
                //Xử lý custom value qua operator
                return GetSqlCommandForGetGetCustomValues(entityAction.DescriptorColection, entityAction.EntityName);
            }
            return null;
        }


        public abstract List<BaseEntity> PopulateBusinessObjectFromReader(IDataReader dataReader);
        public virtual void UpdateEntityId(EntityCommand entityAction, SqlCommand command)
        {
            if (entityAction.EntityAction == EntityAction.Insert)
            {
                entityAction.BaseEntity = new Entity.Entity(UpdateEntityId(entityAction.BaseEntity.GetEntity(), command));
            }
        }
        public abstract BaseEntity UpdateEntityId(BaseEntity baseEntity, SqlCommand command);
    }
}

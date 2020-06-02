using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonicationMemory.Resources
{
    public class UpdateEntitySql : DataLayerBase
    {
        public bool Update(List<EntityCommand> listEntityActionCommand)
        {
            int commandindex = 0;
            if (MainConnection.State == ConnectionState.Closed)
                MainConnection.Open();
            var transaction = MainConnection.BeginTransaction();
            var command = new SqlCommand[listEntityActionCommand.Count];
            try
            {

                foreach (var entityActionCommand in listEntityActionCommand)
                {

                    var entity = EntityManager.Instance.GetMyEntity(entityActionCommand.BaseEntity.GetName());
                    var sqlCommand = entity.GetSqlCommand(entityActionCommand);
                    sqlCommand.Connection = MainConnection;
                    sqlCommand.Transaction = transaction;
                    command[commandindex] = sqlCommand;
                    sqlCommand.ExecuteNonQuery();
                    entity.UpdateEntityId(entityActionCommand, sqlCommand);
                    commandindex++;
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("UpdateEntitySql::Update::Error occured.", ex);
            }
            finally
            {
                MainConnection.Close();
                for (int i = 0; i < listEntityActionCommand.Count; i++)
                {
                    command[i].Dispose();
                }
            }
        }

        public List<BaseEntity> GetAllEntity(EntityQuery entiryQuery)
        {
            if (MainConnection.State == ConnectionState.Closed)
                MainConnection.Open();

            var sqlCommand = new SqlCommand();
            try
            {

                var entity = EntityManager.Instance.GetMyEntity(entiryQuery.EntityName);
                sqlCommand = entity.GetSqlCommand(entiryQuery);
                sqlCommand.Connection = MainConnection;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return entity.PopulateBusinessObjectFromReader(dataReader);

            }
            catch (Exception ex)
            {
                throw new Exception("UpdateEntitySql::Getall::Error occured.", ex);
            }
            finally
            {
                MainConnection.Close();
                sqlCommand.Dispose();
            }
        }
    }
}

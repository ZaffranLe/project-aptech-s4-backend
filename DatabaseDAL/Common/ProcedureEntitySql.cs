using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Anotar.NLog;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class ProcedureEntitySql : IDisposable
    {
        private const int TimeOut = 20 * 60;
        private readonly SqlConnection _mainConnection = EntityManager.Instance.GetConnection();
        //private readonly SqlConnection _reportConnection = EntityManager.Instance.GetReportConnection();

       

        public bool ProcessProcedureByName(string procedureName)
        {
            //Chi xu ly voi procedure tren database working
            using (var mainConnection = EntityManager.Instance.GetConnection())
            using (var sqlCommand = new SqlCommand { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.Connection = mainConnection;
                    sqlCommand.CommandTimeout = TimeOut;
                    sqlCommand.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    LogTo.Info(ex.ToString());
                    throw new Exception("RUN PROCEDURE ERROR ", ex);
                }
            }
        }

        
        public List<BaseEntity> GetListEntityByProcedureName(string entityName, string procedureName)
        {
            LogTo.Info("RUN STORE ENTITY = {0} IS STORE NAME = {1}", entityName, procedureName);
            using (var con = EntityManager.Instance.GetConnection(entityName))
            using (var sqlCommand = new SqlCommand())
            {
                try
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.Connection = con;


                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        //Tang so luong doc ban ghi len 100 lan
                        var entity = EntityManager.Instance.GetMyEntity(entityName);
                        return entity.PopulateBusinessObjectFromReader(dataReader);
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error("Loi khi lay du lieu EntityName is {0} and ProcedureName is {1}",
                            entityName, procedureName);
                    LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                    throw new Exception("GetListEntitySql::Getall::Error occured.", ex);
                }
            }
        }

        public List<BaseEntity> GetListEntityByProcedureNameWithParam(string entityName, string procedureName, List<DefinitionStoreProce> listDefinition)
        {
            LogTo.Info("RUN STORE ENTITY = {0} IS STORE NAME = {1}", entityName, procedureName);
            using (var con = EntityManager.Instance.GetConnection(entityName))
            using (var sqlCommand = new SqlCommand())
            {
                try
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = procedureName;
                    sqlCommand.Connection = con;

                    if (listDefinition != null && listDefinition.Count > 0)
                    {
                        foreach (var definitionStoreProce in listDefinition)
                        {
                            ParameterDirection parameterDirection;
                            ParameterDirection.TryParse(definitionStoreProce.ParameterDirection, out parameterDirection);
                            SqlDbType oracleDbType;
                            SqlDbType.TryParse(definitionStoreProce.OracleDbType, out oracleDbType);
                            sqlCommand.Parameters.Add(new SqlParameter(definitionStoreProce.FieldName, oracleDbType, definitionStoreProce.Length, parameterDirection, false, 0, 0, "", DataRowVersion.Proposed, definitionStoreProce.FieldValue));

                        }
                    }
                    using (var dataReader = sqlCommand.ExecuteReader())
                    {
                        //Tang so luong doc ban ghi len 100 lan
                        var entity = EntityManager.Instance.GetMyEntity(entityName);
                        return entity.PopulateBusinessObjectFromReader(dataReader);
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error("Loi khi lay du lieu EntityName is {0} and ProcedureName is {1}",
                            entityName, procedureName);
                    LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                    throw new Exception("GetListEntitySql::Getall::Error occured.", ex);
                }
            }
        }
        

        

        public void Dispose()
        {
            _mainConnection.Dispose();
        }
    }
    public class DefinitionStoreProce
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string ParameterDirection { get; set; }
        public string OracleDbType { get; set; }
        public int Length { get; set; }
    }
}

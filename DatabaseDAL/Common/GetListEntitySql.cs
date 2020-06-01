using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Anotar.NLog;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class GetListEntitySql
    {
        /* Sử dụng cho những hàm lấy all thông tin 
         * Sử dụng cho những hàm lấy customer thông tin dùng cho entity không có ref dữ liệu
         */
        public List<BaseEntity> GetAllEntity(EntityQuery entiryQuery)
        {
            var sqlCommand = new SqlCommand();
            try
            {
                var entity = EntityManager.Instance.GetMyEntity(entiryQuery.EntityName);
                sqlCommand = entity.GetSqlCommand(entiryQuery);
                sqlCommand.Connection = EntityManager.Instance.GetConnection(entiryQuery.EntityName);

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return entity.PopulateBusinessObjectFromReader(dataReader);
            }
            catch (Exception ex)
            {
                LogTo.Error("Loi khi lay du lieu EntityName is {0} and QueryAction is {1}",
                        entiryQuery.EntityName, entiryQuery.QueryAction);
                LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                throw new Exception("GetListEntitySql::Getall::Error occured.", ex);
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        

        

        public object GetMaxId(string entityName, string fieldName)
        {
            var sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = string.Format("SELECT MAX({0}) FROM {1}", fieldName, entityName);
                sqlCommand.Connection = EntityManager.Instance.GetConnection(entityName);

                var obj = sqlCommand.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                LogTo.Error("Loi khi lay du lieu EntityName is {0} and FieldName is {1}",
                        entityName, fieldName);
                LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                throw new Exception("GetMaxId::Error occured.", ex);
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        public List<BaseEntity> SelectTextQuerry(string textQuerry, string entityName)
        {
            var listEntityResult = new List<BaseEntity>();
            var sqlCommand = new SqlCommand();
            try
            {
                var entity = EntityManager.Instance.GetMyEntity(entityName);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = textQuerry;
                sqlCommand.Connection = EntityManager.Instance.GetConnection(entityName);


                IDataReader dataReader = sqlCommand.ExecuteReader();
                listEntityResult = entity.PopulateBusinessObjectFromReader(dataReader);
            }
            catch (Exception ex)
            {
                LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                throw new Exception("GetListEntitySql::Getcustom::Error occured.", ex);
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
            return listEntityResult;
        }

        public object GetTotalRowBySql(string entityName, string whereSql)
        {
            //Ham lay tong so ban ghi qua câu lệnh query
            //Ví dụ : Select count(CustomerId) from OrderTransaction where CustomerId = 66
            var sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = string.Format("SELECT COUNT(*) FROM {0} {1} ", entityName, whereSql);
                sqlCommand.Connection = EntityManager.Instance.GetConnection(entityName);

                var obj = sqlCommand.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                LogTo.Error("Loi khi lay du lieu EntityName is {0} and WHERE SQL is {1}",
                        entityName, whereSql);
                LogTo.Error("SQL ERROR:" + sqlCommand.CommandText);
                throw new Exception("GetTotalRowBySql::Error occured.", ex);
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }
    }
}

using System;
using System.Data.SqlClient;
using BaseUtils;
using ElectricShop.Common.Config;
using ElectricShop.DatabaseDAL.EntitySql;
using ElectricShop.Entity.Entities;

namespace ElectricShop.DatabaseDAL.Common
{
    public class EntityManager
    {
        private readonly DatabaseConfig _databaseConfig;
        private readonly string _connectionString;
        private readonly bool _isUseMySql;
        #region Instance

        public static EntityManager Instance
        {
            get { return Nested.Instance; }
        }

        private class Nested
        {
            internal static readonly EntityManager Instance = new EntityManager();
        }

        #endregion

        public EntityManager()
        {
            var configLoader = new ConfigLoader(typeof(DatabaseConfig));
            _databaseConfig = configLoader.Config as DatabaseConfig;

            if (_databaseConfig == null)
                throw new Exception("Not found config database");
            Console.WriteLine("------------------------------------");
            _connectionString = _databaseConfig.ConnectionString;
            Console.WriteLine(_connectionString);
            
        }

        public EntityBaseSql GetMyEntity(string entityName)
        {
            
            #region EquixSyncOrder
            if (entityName.Equals(YdspLme.EntityName())) { return new YdspLmeSql(); }


            #endregion

            Console.WriteLine(entityName);

            return null;
        }

        public SqlConnection GetConnection(string entityName)
        {
            return GetConnection();
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection { ConnectionString = _connectionString };
            // Get connection string from Config File and set to the connection
            connection.Open();
            return connection;
        }

        

        public string GetNameDataWorking()
        {
            var stringName = _connectionString;

            var connection = new SqlConnection { ConnectionString = stringName };
            return connection.Database;
        }
    }
}

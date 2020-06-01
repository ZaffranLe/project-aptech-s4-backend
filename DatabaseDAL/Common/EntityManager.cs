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

            #region GetMyEntity
            if (entityName.Equals(Customer.EntityName())) { return new CustomerSql(); }
            if (entityName.Equals(Image.EntityName())) { return new ImageSql(); }
            if (entityName.Equals(ImportReceipt.EntityName())) { return new ImportReceiptSql(); }
            if (entityName.Equals(Manufacturer.EntityName())) { return new ManufacturerSql(); }
            if (entityName.Equals(OrderDetail.EntityName())) { return new OrderDetailSql(); }
            if (entityName.Equals(Permission.EntityName())) { return new PermissionSql(); }
            if (entityName.Equals(Product.EntityName())) { return new ProductSql(); }
            if (entityName.Equals(ProductType.EntityName())) { return new ProductTypeSql(); }
            if (entityName.Equals(Property.EntityName())) { return new PropertySql(); }
            if (entityName.Equals(Provider.EntityName())) { return new ProviderSql(); }
            if (entityName.Equals(Role.EntityName())) { return new RoleSql(); }
            if (entityName.Equals(RolePermission.EntityName())) { return new RolePermissionSql(); }
            if (entityName.Equals(UserInfo.EntityName())) { return new UserInfoSql(); }
            if (entityName.Equals(UserLogin.EntityName())) { return new UserLoginSql(); }
            if (entityName.Equals(UserRole.EntityName())) { return new UserRoleSql(); }



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

using CommonicationMemory.Common;

namespace CommonicationMemory.DatabaseSchema
{
    /// <summary>
    /// Provides the functionality to get the database schema
    /// </summary>
    interface IDatabaseSchema 
    {
        
        /// <summary>
        /// load the database schema
        /// </summary>
        /// <param name="databaseServer">name of the database server</param>
        /// /// <param name="catalog">name of the catalog</param>
        /// <param name="connectionString">connection string</param>
        /// <returns>database schema</returns>
        Database GetDataBaseSchema(string databaseServer,string catalog, string connectionString);
        Database GetDataBaseSchemaMySql(string databaseServer,string catalog, string connectionString);
    }
}

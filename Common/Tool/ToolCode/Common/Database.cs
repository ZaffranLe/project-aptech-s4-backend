using System.Collections.Generic;

namespace CommonicationMemory.Common
{
    /// <summary>
    /// class to contain to Data base schema
    /// </summary>
    public class Database
    {
        #region Data members

        private string _databaseServer = string.Empty;
        private string _catalog = string.Empty;
        private string _connectionString = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// get/set the name of the Database
        /// </summary>
        public string DatabaseServer
        {
            get { return _databaseServer; }
            set { _databaseServer = value; }
        }

        /// <summary>
        /// get/set the catalog name
        /// </summary>
        public string Catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }

        /// <summary>
        /// get/set the connection string
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Get the collection of the Tables that belongs to this database
        /// </summary>
        public List<DatabaseTable> Tables { get; set; }

        #endregion

    }
}

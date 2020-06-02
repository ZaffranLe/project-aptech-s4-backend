using CommonicationMemory.Common;

namespace CommonicationMemory
{
    using System.Collections.Generic;

    /// <summary>
    /// Singleton class for the tier generator setting
    /// </summary>
    class TierGeneratorSettings
    {
        #region Data Members

        public Dictionary<string, Dictionary<string, string>> DicEntites { get; set; } 
        private static TierGeneratorSettings _setting = null;

        private string _projectNameSpace = string.Empty;
        private string _connectionString = string.Empty;
        private string _codeGenerationPath = string.Empty;
        private string _classPrefix = string.Empty;
        private string _storeProcedurePrefix = string.Empty;
        private bool _generateBusinessLayer = false;
        private Database _database = null;

        #endregion

        #region Private Constructor

        /// <summary>
        /// private constructor
        /// </summary>
        private TierGeneratorSettings() { }

        #endregion

        #region Instance Property

        public static TierGeneratorSettings Instance
        {
            get
            {
                if (_setting == null)
                    _setting = new TierGeneratorSettings();

                return _setting;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// get/set the namespace of the project
        /// </summary>
        public string ProjectNameSpace
        {
            get { return _projectNameSpace; }
            set { _projectNameSpace = value; }
        }

        /// <summary>
        /// get/set the connection string for Data base
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// get/set the path for code generation.
        /// </summary>
        public string CodeGenerationPath
        {
            get { return _codeGenerationPath; }
            set { _codeGenerationPath = value; }
        }

        /// <summary>
        /// get/set the Database schema
        /// </summary>
        public Database Database
        {
            get { return _database; }
            set { _database = value; }
        }

        /// <summary>
        /// get/set the class Prefix
        /// </summary>
        public string ClassPrefix
        {
            get { return _classPrefix; }
            set { _classPrefix = value; }
        }

        /// <summary>
        /// get/set the store procedure prefix
        /// </summary>
        public string StoreProcedurePrefix
        {
            get { return _storeProcedurePrefix; }
            set { _storeProcedurePrefix = value; }
        }

        /// <summary>
        /// get/set the either to generate business layer or not
        /// </summary>
        public bool GenerateBusinessLayer
        {
            get { return _generateBusinessLayer; }
            set { _generateBusinessLayer = value; }
        }

        public bool GenProcess { get; set; }

        public bool UseMaxKey { get; set; }

        public bool GenObjectKeys { get; set; }
        public string FoudationLink { get; set; }
        public string MemoryWorkerBaseLink { get; set; }
        public string EntitiesLink { get; set; }
        public object CheckGenByForder { get; set; }

        #endregion

        #region Public Methods

        public string GetStoreProcedureName(string tableName, StoreProcedureType spType)
        {
            string format = "{0}{1}_{2}";
            var stringReturn = string.Format(format, StoreProcedurePrefix, tableName, spType.ToString());
            return stringReturn;
        }

        public string GetNameTooLong(string header, string tableName)
        {
            var stringReturn = tableName;
            if ((header + stringReturn).Length <= 30)
                return header + stringReturn;

            //Cắt các ký tự
            var newString = "";
            for (int i = 0; i < tableName.Length; i++)
            {
                if (char.IsUpper(tableName[i]))
                {
                    newString += tableName[i];
                    if (tableName.Length > i + 1)
                    {
                        if (!char.IsUpper(tableName[i + 1]))
                            newString += tableName[i + 1];
                    }
                }
            }
            return header + newString;
        }

        #endregion
    }
}

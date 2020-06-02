using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms.VisualStyles;
using CommonicationMemory.CodeGeneration.CreateFile.Commonication;
using CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker;
using CommonicationMemory.Common;
using CommonicationMemory.DatabaseSchema;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration
{
    using System.Linq;

    internal class StoreProceduceGenerator
    {
        #region Properties

        public string RootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace;
            }
        }

        #endregion

        #region Public Methods

        public void GenerateSqlStoreProcedures(bool isCheck)
        {
            if (isCheck)
            {
                var fileCheck = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_CheckName.sql";

                using (var sw = new StreamWriter(fileCheck))
                {
                    foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                    {
                        CheckSuccessFieldName(sw, table);
                    }
                }
                return;
            }

            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_StoredProcedures.sql";

            using (var sw = new StreamWriter(filePath))
            {
                //Loc thong tin database name de phan tach du lieu
                var dicByName = new Dictionary<string, List<DatabaseTable>>();
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (!table.IsSelected) continue;
                    if(!dicByName.ContainsKey(table.DatabaseName))
                        dicByName[table.DatabaseName] = new List<DatabaseTable>();
                    dicByName[table.DatabaseName].Add(table);
                }

                foreach (var keyValue in dicByName)
                {
                    var usingDb = "USE " + keyValue.Key + "\r\n";
                    if (MySqlGenerator.IsConnectMySql)
                    {
                        usingDb = "USE " + keyValue.Key + ";\r\n";
                    }
                    else usingDb += "GO\r\n";
                    sw.WriteLine(usingDb);
                    sw.WriteLine("");

                    foreach (var table in keyValue.Value)
                    {
                        if (!table.IsSelected) continue;
                        InsertSp(sw, table);
                        UpdateSp(sw, table);
                        SelectAllSp(sw, table);
                        DeleteByPrimaryKeySp(sw, table);
                    }
                }
            }

            //Tạo file create bảng riêng
            GeneratorDevzone();
            GenerateSqlCreateTable();
            GenerateSqlDropTableSeq();
            GenerateSqlDropStore();
            GenerateSqlTruncateTable();
            GenerateFileCheckMemory();
            GenerateFileCommonicationMemory();
            GenerateFileMemorySetDatabase();
            GeneratorDevzone();
        }


        public void GenerateSqlStoreProceduresDrop(bool isCheck)
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_DropSequence.sql";

            using (var sw = new StreamWriter(filePath))
            {
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (table.IsSelected)
                    {
                        DropSeq(sw, table);
                    }
                }
            }

            var filePathDelete = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_DeleteTableSequence.sql";

            using (var sw = new StreamWriter(filePathDelete))
            {
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (table.IsSelected)
                    {
                        DeleteTableSeq(sw, table);
                    }
                }
            }
        }

        public void GenerateSqlStoreProceduresCreateAll(bool isCheck)
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_CreateAllSequence.sql";

            using (var sw = new StreamWriter(filePath))
            {
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (table.IsSelected)
                    {
                        CreateAllSeq(sw, table);
                    }
                }
            }
        }

        public void GenerateSqlAutoNumber()
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_AutoNumber.txt";

            using (var sw = new StreamWriter(filePath))
            {
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    AutoNumber(sw, table);
                }
            }
        }

        private void GenerateSqlDropStore()
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_DropStore.sql";

            using (var sw = new StreamWriter(filePath))
            {
                //Loc thong tin database name de phan tach du lieu
                var dicByName = new Dictionary<string, List<DatabaseTable>>();
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (!table.IsSelected) continue;
                    if (!dicByName.ContainsKey(table.DatabaseName))
                        dicByName[table.DatabaseName] = new List<DatabaseTable>();
                    dicByName[table.DatabaseName].Add(table);
                }

                foreach (var keyValue in dicByName)
                {
                    var usingDb = "USE " + keyValue.Key + "\r\n";
                    usingDb += "GO\r\n";
                    sw.WriteLine(usingDb);
                    sw.WriteLine("");

                    foreach (var table in keyValue.Value)
                    {
                        if (table.IsSelected)
                        {
                            DropSp(sw, table);
                        }
                    }
                }
            }
        }

        private void GenerateFileCheckMemory()
        {
            var file = new CreateFileCheckMemory();
            var folderPath = RootPath +
                              Path.DirectorySeparatorChar +
                              "FileGenerator\\FileCheckMemory\\";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (DatabaseTable table in TierGeneratorSettings.Instance.Database.Tables)
            {
                if (table.IsSelected)
                {
                    if (table.TableName.StartsWith("Z_"))
                        continue;
                    string filePath = folderPath + "CheckMemory" + table.TableName + ".cs";
                    using (var sw = new StreamWriter(filePath))
                    {
                        file.CreateFileCompare(sw, table);
                    }
                }
            }
        }

        private void GenerateFileCommonicationMemory()
        {
            var file = new CreateFileDicMemory();
            string folderPath = RootPath +
                              Path.DirectorySeparatorChar +
                              "FileGenerator\\FileCustom\\Commonication\\";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var listDatabaseTables = TierGeneratorSettings.Instance.Database.Tables;
            listDatabaseTables = listDatabaseTables.FindAll(t => !t.TableName.StartsWith("Z_"));
            listDatabaseTables.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));


            string filePath = folderPath + "FileDicMemory.cs";
            using (var sw = new StreamWriter(filePath))
            {
                file.CreateFile(sw, listDatabaseTables);
            }

            var fileProcessing = new CreateFileProcessingMemory();
            string filePathProcessing = folderPath + "ProcessingMemory.cs";
            using (var sw = new StreamWriter(filePathProcessing))
            {
                fileProcessing.CreateFile(sw, listDatabaseTables);
            }

            var fileClone = new CreateFileMemorySetRequest();
            string filePathClone = folderPath + "MemorSet.Request.cs";
            using (var sw = new StreamWriter(filePathClone))
            {
                fileClone.CreateFile(sw, listDatabaseTables);
            }
        }

        private void GenerateFileMemorySetDatabase()
        {

            string folderPath = RootPath +
                              Path.DirectorySeparatorChar +
                              "FileGenerator\\FileCustom\\DatalayerWorker\\";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            List<DatabaseTable> listDatabaseTables = TierGeneratorSettings.Instance.Database.Tables;
            listDatabaseTables = listDatabaseTables.FindAll(t => !t.TableName.StartsWith("Z_"));
            listDatabaseTables.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));

            var file = new CreateFileMemorySet();
            file.CreateFile(folderPath, "MemorySet.Entity.cs", listDatabaseTables);
            var fileSetAndRemove = new CreateFileMemorySetAndRemove();
            fileSetAndRemove.CreateFile(folderPath, "MemorySet.SetAndRemove.cs", listDatabaseTables);

            var fileGet = new CreateFileMemoryGet();
            fileGet.CreateFile(folderPath, "MemoryInfo.Get.cs", listDatabaseTables);

            var fileIsExist = new CreateFileMemoryGetIsExist();
            fileIsExist.CreateFile(folderPath, "MemoryInfo.IsExist.cs", listDatabaseTables);

            var fileGetAll = new CreateFileMemoryGetAll();
            fileGetAll.CreateFile(folderPath, "MemoryInfo.GetAll.cs", listDatabaseTables);

            var fileGetByField = new CreateFileMemoryGetByField();
            fileGetByField.CreateFile(folderPath, "MemoryInfo.GetByField.cs", listDatabaseTables);
        }

        private void GenerateSqlDropTableSeq()
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_DropTableSeq.sql";

            using (var sw = new StreamWriter(filePath))
            {
                //Loc thong tin database name de phan tach du lieu
                var dicByName = new Dictionary<string, List<DatabaseTable>>();
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (!table.IsSelected) continue;
                    if (!dicByName.ContainsKey(table.DatabaseName))
                        dicByName[table.DatabaseName] = new List<DatabaseTable>();
                    dicByName[table.DatabaseName].Add(table);
                }

                foreach (var keyValue in dicByName)
                {
                    var usingDb = "USE " + keyValue.Key + "\r\n";
                    usingDb += "GO\r\n";
                    sw.WriteLine(usingDb);
                    sw.WriteLine("");

                    foreach (var table in keyValue.Value)
                    {
                        if (table.IsSelected)
                        {
                            DropTable(sw, table);
                        }
                    }
                }
            }
        }

        private void GenerateSqlCreateTable()
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_CreateTable.sql";

            var filePathTranfer = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_TranferDataTable.sql";

            using (var sw = new StreamWriter(filePath))
            {
                //Loc thong tin database name de phan tach du lieu
                var dicByName = new Dictionary<string, List<DatabaseTable>>();
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (!table.IsSelected) continue;
                    if (!dicByName.ContainsKey(table.DatabaseName))
                        dicByName[table.DatabaseName] = new List<DatabaseTable>();
                    dicByName[table.DatabaseName].Add(table);
                }

                foreach (var keyValue in dicByName)
                {
                    var usingDb = "USE " + keyValue.Key + "\r\n";
                    usingDb += "GO\r\n";
                    if (MySqlGenerator.IsConnectMySql)
                    {
                        usingDb = "USE " + keyValue.Key + ";\r\n";
                    }

                    sw.WriteLine(usingDb);
                    sw.WriteLine("");

                    foreach (var table in keyValue.Value)
                    {
                        if (table.IsSelected)
                        {
                            CreateTable(sw, table);
                        }
                    }
                }
            }

            using (var sw = new StreamWriter(filePathTranfer))
            {
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (table.IsSelected)
                    {
                        TranferDataTable(sw, table);
                    }
                }
            }
        }

        private void GenerateSqlTruncateTable()
        {
            var filePath = RootPath +
                              Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_TruncateTable.sql";

            using (var sw = new StreamWriter(filePath))
            {
                //Loc thong tin database name de phan tach du lieu
                var dicByName = new Dictionary<string, List<DatabaseTable>>();
                foreach (var table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (!table.IsSelected) continue;
                    if (!dicByName.ContainsKey(table.DatabaseName))
                        dicByName[table.DatabaseName] = new List<DatabaseTable>();
                    dicByName[table.DatabaseName].Add(table);
                }

                foreach (var keyValue in dicByName)
                {
                    var usingDb = "USE " + keyValue.Key + "\r\n";
                    usingDb += "GO\r\n";
                    sw.WriteLine(usingDb);
                    sw.WriteLine("");

                    foreach (var table in keyValue.Value)
                    {
                        if (table.IsSelected)
                        {
                            TruncateTable(sw, table);
                        }
                    }
                }
            }


        }

        private void GeneratorDevzone()
        {
            var file = new CreateFileDevzone();
            string folderPath = RootPath +
                              Path.DirectorySeparatorChar +
                              "FileGenerator\\FileCustom\\Commonication\\";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            List<DatabaseTable> listDatabaseTables = TierGeneratorSettings.Instance.Database.Tables;
            listDatabaseTables = listDatabaseTables.FindAll(t => !t.TableName.StartsWith("Z_"));
            listDatabaseTables.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));

            string filePath = folderPath + "FileTableDevzone.cs";
            using (var sw = new StreamWriter(filePath))
            {
                file.CreateFile(sw, listDatabaseTables);
            }
        }

        #endregion

        #region Private Methods

        #region Store Procedures

        private void CheckSuccessFieldName(StreamWriter sw, DatabaseTable databaseTable)
        {
            bool isFail = false;
            var stringBuilder = new StringBuilder();
            //Check tên bảng
            if (databaseTable.TableName.Length > 30)
            {
                stringBuilder.AppendLine(databaseTable.TableName + ": Table Name Too Long " + databaseTable.TableName.Length);
                isFail = true;
            }

            //Check tạo ra các store proceduce
            var insert = TierGeneratorSettings.Instance.GetStoreProcedureName(databaseTable.TableName, StoreProcedureType.Insert);
            var update = TierGeneratorSettings.Instance.GetStoreProcedureName(databaseTable.TableName, StoreProcedureType.Update);
            var delete = TierGeneratorSettings.Instance.GetStoreProcedureName(databaseTable.TableName, StoreProcedureType.Delete);
            if (insert.Length > 30)
            {
                stringBuilder.AppendLine(insert + ": Store Name Too Long " + insert.Length);
                isFail = true;
            }
            if (update.Length > 30)
            {
                stringBuilder.AppendLine(update + ": Store Name Too Long " + update.Length);
                isFail = true;
            }
            if (delete.Length > 30)
            {
                stringBuilder.AppendLine(delete + ": Store Name Too Long " + delete.Length);
                isFail = true;
            }

            //Check các fields
            foreach (var databaseColumn in databaseTable.Columns.OrderBy(x => x.Name))
            {
                if (databaseColumn.Name.Length > 30)
                {
                    stringBuilder.AppendLine(databaseColumn.Name + ": Field Name Too Long " + databaseColumn.Name.Length);
                    isFail = true;
                }
                if (databaseColumn.IsAutoNumber)
                {
                    //Check tạo ra các sequence
                    var seqName = GetSeqName(databaseTable.TableName);
                    if (seqName.Length > 30)
                    {
                        stringBuilder.AppendLine(seqName + ": Sequence Name Too Long " + seqName.Length);
                        isFail = true;
                    }
                }
            }

            var pkTable = GetContrainName(databaseTable.TableName);
            if (pkTable.Length > 30)
            {
                stringBuilder.AppendLine(pkTable + ": Contrain Name Too Long " + pkTable.Length);
                isFail = true;
            }
            if (isFail)
            {
                sw.WriteLine("----------------------------" + databaseTable.TableName);
                sw.WriteLine(stringBuilder);
            }
        }

        private string GetParameter(DatabaseColumn column, string outPut)
        {
            if (!OracleHelper.IsConectOracle)
            {
                return GetParameterSql(column);
            }
            var parameter = column.SqlParameterName + " " + outPut + " " + GetOracleTypeOnly(column.DataType, column);
            if (column.IsNull)
                parameter += " Default NULL";
            return parameter;
        }

        private string GetParameterSql(DatabaseColumn column)
        {
            string parameter = column.SqlParameterName + " " + column.DataType;
            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    {
                        string size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        parameter += "(" + size + ")";
                        break;
                    }
                case "decimal":
                    {
                        parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                        break;
                    }
            }


            if (column.IsNull)
                parameter += " = null";

            return parameter;
        }

        private string GetParameterCreate(DatabaseColumn column)
        {
            if (!OracleHelper.IsConectOracle)
            {
                return GetParameterCreate_Sql(column);
            }
            var parameter = column.Name + " " + GetOracleType(column.DataType);
            switch (column.DataType.ToLower())
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varchar2":
                case "nvarchar2":
                case "raw":
                case "varchar":
                    {
                        var size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        if (size == "MAX")
                            return column.Name + " " + GetOracleType("NTEXT");
                        parameter += "(" + size + ") ";
                        break;
                    }
                case "decimal":
                    {
                        parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                        break;
                    }
                case "number":
                    {
                        parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                        break;
                    }
                case "varbinary":
                    {
                        var size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        if (size == "MAX")
                            return column.Name + " " + GetOracleType("IMAGE");
                        parameter += "(" + size + ") ";
                        break;
                    }
            }

            return parameter;
        }

        private string GetParameterCreate_Sql(DatabaseColumn column)
        {
            if (MySqlGenerator.IsConnectMySql)
                return GetParameterCreate_MySql(column);
            string parameter = "[" + column.Name + "] [" + column.DataType + "]";

            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    {
                        string size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        parameter += "(" + size + ") ";
                        break;
                    }
                case "decimal":
                    {
                        parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                        break;
                    }
            }

            return parameter;
        }

        private string GetParameterCreate_MySql(DatabaseColumn column)
        {
            string parameter = "`" + column.Name + "` " + column.DataType + "";

            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                {
                    string size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                    parameter += "(" + size + ") ";
                    break;
                }
                case "decimal":
                {
                    parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                    break;
                }
            }

            return parameter;
        }

        private string GetOracleType(string msSqlType)
        {
            switch (msSqlType.ToUpper())
            {
                case "TINYINT":
                    return "NUMBER(3)";
                case "SMALLINT":
                    return "NUMBER(5)";
                case "INTEGER":
                    return "NUMBER(10)";
                case "INT":
                    return "NUMBER(10)";
                case "BIGINT":
                    return "NUMBER(19)";
                case "DECIMAL":
                    return "NUMBER";
                case "NUMERIC":
                    return "NUMBER";
                case "SMALLMONEY":
                    return "NUMBER(10,4)";
                case "MONEY":
                    return "NUMBER(19,4)";
                case "REAL":
                    return "FLOAT(23)";
                case "FLOAT":
                    return "FLOAT(49)";
                case "SMALLDATETIME":
                    return "DATE";
                case "DATETIME":
                    return "DATE";
                case "TIMESTAMP":
                    return "RAW";
                case "DATETIMEOFFSET":
                    return "TIMESTAMP";
                case "CHAR":
                    return "CHAR";
                case "NCHAR":
                    return "NCHAR";
                case "NVARCHAR":
                    return "NVARCHAR2";
                case "VARCHAR":
                    return "VARCHAR2";
                case "VARCHAR(MAX)":
                    return "CLOB";
                case "TEXT":
                    return "CLOB";
                case "NTEXT":
                    return "CLOB";
                case "BINARY":
                case "VARBINARY":
                    return "RAW";
                case "IMAGE":
                    return "LONG RAW";
                case "BIT":
                    return "NUMBER(3)";
                case "UNIQUEIDENTIFIER":
                    return "RAW(16)";
                default:
                    return msSqlType;
            }
        }

        private string GetOracleTypeOnly(string msSqlType, DatabaseColumn column)
        {
            switch (msSqlType.ToUpper())
            {
                case "TINYINT":
                case "SMALLINT":
                case "INTEGER":
                case "INT":
                case "BIGINT":
                case "DECIMAL":
                case "NUMERIC":
                case "SMALLMONEY":
                case "MONEY":
                    return "NUMBER";
                case "REAL":
                    return "FLOAT";
                case "FLOAT":
                    return "FLOAT";
                case "SMALLDATETIME":
                    return "DATE";
                case "DATETIME":
                    return "DATE";
                case "TIMESTAMP":
                    return "RAW";
                case "DATETIMEOFFSET":
                    return "TIMESTAMP";
                case "CHAR":
                    return "CHAR";
                case "NCHAR":
                    return "NCHAR";
                case "NVARCHAR":
                    {
                        var size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        if (size == "MAX")
                            return "CLOB";
                        return "NVARCHAR2";
                    }
                case "VARCHAR":
                    return "VARCHAR2";
                case "VARCHAR(MAX)":
                    return "CLOB";
                case "TEXT":
                    return "CLOB";
                case "NTEXT":
                    return "CLOB";
                case "BINARY":
                case "VARBINARY":
                    {
                        var size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        if (size == "MAX")
                            return "LONG RAW";
                        return "RAW";
                    }
                case "IMAGE":
                    return "RAW";
                case "BIT":
                    return "NUMBER";
                case "UNIQUEIDENTIFIER":
                    return "RAW";
                default:
                    return msSqlType;
            }
        }

        private string GetDeleteExistTable(string tableName)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                return "DROP TABLE IF EXISTS `" + tableName + "`;";
            }
            if (!OracleHelper.IsConectOracle)
            {
                return GetDeleteExistTable_Sql(tableName);
            }
            return "BEGIN EXECUTE IMMEDIATE 'DROP TABLE " + tableName + "';EXCEPTION WHEN OTHERS THEN IF SQLCODE != -942 THEN RAISE; END IF; END;";
        }

        private string GetDeleteExistTable_Sql(string tableName)
        {
            return "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'" + tableName +
                   "') AND type in (N'U')) DROP TABLE [dbo].[" + tableName + "]" + "\r\nGO";
        }

        private string GetDeleteExistSequence(string seqName)
        {
            return "BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE " + seqName + "';EXCEPTION WHEN OTHERS THEN IF SQLCODE != -2289 THEN RAISE; END IF; END;";
        }

        private void CreateAllSeq(StreamWriter sw, DatabaseTable table)
        {
            var tableName = table.TableName;
            sw.WriteLine("--Create sequence table " + tableName);
            var sequence = new StringBuilder();
            var count = table.Columns.Count;
            bool hasSeq = false;
            for (int i = 0; i < count; i++)
            {
                var column = table.Columns[i];
                //Nếu là tự tăng thì thêm identity
                if (column.IsAutoNumber)
                {
                    var seqName = GetSeqName(tableName);
                    hasSeq = true;
                    sequence.AppendLine(GetDeleteExistSequence(seqName));
                    sequence.AppendLine("/");
                    sequence.AppendLine("CREATE SEQUENCE " + seqName + " start with 1;");
                }

            }

            if (hasSeq)
                sw.WriteLine(sequence);
        }

        private void DropSeq(StreamWriter sw, DatabaseTable table)
        {
            var tableName = table.TableName;
            sw.WriteLine("--Drop table " + tableName);
            var dropSequence = new StringBuilder();
            var count = table.Columns.Count;

            bool hasSeq = false;
            for (var i = 0; i < count; i++)
            {
                var column = table.Columns[i];
                //Nếu là tự tăng thì thêm identity
                if (!column.IsAutoNumber) continue;
                var seqName = GetSeqName(tableName);
                hasSeq = true;
                dropSequence.AppendLine(GetDeleteExistSequence(seqName));
                dropSequence.AppendLine("/");
            }

            if (hasSeq)
                sw.WriteLine(dropSequence);
        }

        private void DeleteTableSeq(StreamWriter sw, DatabaseTable table)
        {
            var tableName = table.TableName;
            var count = table.Columns.Count;

            bool hasSeq = false;
            for (var i = 0; i < count; i++)
            {
                var column = table.Columns[i];
                //Nếu là tự tăng thì thêm identity
                if (!column.IsAutoNumber) continue;
                hasSeq = true;
            }

            if (hasSeq)
                sw.WriteLine("DELETE FROM " + tableName + ";");
        }

        private void DropTable(StreamWriter sw, DatabaseTable table)
        {
            var tableName = table.TableName;
            sw.WriteLine("--Drop table " + tableName);
            var deleteTable = new StringBuilder();
            var dropSequence = new StringBuilder();
            var count = table.Columns.Count;
            deleteTable.AppendLine(GetDeleteExistTable(tableName));
            deleteTable.AppendLine("/");

            bool hasSeq = false;
            for (var i = 0; i < count; i++)
            {
                var column = table.Columns[i];
                //Nếu là tự tăng thì thêm identity
                if (!column.IsAutoNumber) continue;
                var seqName = GetSeqName(tableName);
                hasSeq = true;
                dropSequence.AppendLine(GetDeleteExistSequence(seqName));
                dropSequence.AppendLine("/");
            }

            sw.Write(deleteTable.ToString());
            if (hasSeq)
                sw.WriteLine(dropSequence);
        }

        private void CreateTable(StreamWriter sw, DatabaseTable table)
        {
            if (!OracleHelper.IsConectOracle)
            {
                CreateTable_Sql(sw, table);
                return;
            }
            var tableName = table.TableName;
            sw.WriteLine("--Create table " + tableName);

            var deleteTable = new StringBuilder();
            var sequence = new StringBuilder();
            var strInputParams = new StringBuilder();
            var strKey = new StringBuilder();
            var count = table.Columns.Count;
            deleteTable.AppendLine(GetDeleteExistTable(tableName));
            deleteTable.AppendLine("/");
            strInputParams.AppendLine("CREATE TABLE " + tableName);
            strInputParams.AppendLine("(");
            var listColums = new List<DatabaseColumn>();
            bool hasSeq = false;
            for (int i = 0; i < count; i++)
            {
                var column = table.Columns[i];
                //Nếu là tự tăng thì thêm identity
                if (column.IsAutoNumber)
                {
                    var seqName = GetSeqName(tableName);
                    hasSeq = true;
                    sequence.AppendLine(GetDeleteExistSequence(seqName));
                    sequence.AppendLine("/");
                    sequence.AppendLine("CREATE SEQUENCE " + seqName + " start with 1;");
                }
                var notNull = column.IsNull ? "NULL " : "NOT NULL ";
                // Parameters
                strInputParams.AppendLine("\t" + GetParameterCreate(column) + " " + notNull + ",");
                if (column.IsPK || column.IsFK)
                {
                    // Output select
                    listColums.Add(column);
                }
            }

            for (int i = 0; i < listColums.Count; i++)
            {
                var column = listColums[i];
                var ascEnd = "";
                //Đối với trường hợp có 2 key trở lên
                if (i != listColums.Count - 1) ascEnd = ",";
                //Dùng để insert ASC
                strKey.Append(column.Name + ascEnd);
            }

            sw.Write(deleteTable.ToString());
            // Parameters
            sw.WriteLine(strInputParams.ToString());
            var pkTable = "\tCONSTRAINT " + GetContrainName(tableName) + " PRIMARY KEY (" + strKey + ")";
            sw.WriteLine(pkTable);
            sw.WriteLine(");");
            sw.WriteLine();
            if (hasSeq)
                sw.WriteLine(sequence);
        }

        private void TranferDataTable(StreamWriter sw, DatabaseTable table)
        {
            sw.WriteLine();

            var strFields = new StringBuilder();
            var strValues = new StringBuilder();

            var count = table.Columns.Count;

            var i = 0;
            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                var end = ",";
                if (i == count - 1) end = "";
                // Fileds
                strFields.Append(column.Name + "" + end);
                // Values
                strValues.Append('"' + column.Name + '"' + end);
                i++;
            }

            sw.WriteLine("DELETE FROM " + table.TableName + ";");
            sw.WriteLine("INSERT INTO " + table.TableName + "(" + strFields + ")" + " SELECT " + strValues + " FROM " + '"' + table.TableName + '"' + ";");
        }

        private string GetSeqName(string tableName)
        {
            return string.Format("seq_{0}", tableName);
        }

        private string GetContrainName(string tableName)
        {
            return string.Format("pk_{0}", tableName);
        }

        private void TruncateTable(StreamWriter sw, DatabaseTable table)
        {
            if (!OracleHelper.IsConectOracle)
            {
                TruncateTable_Sql(sw, table);
                return;
            }
            string tableName = table.TableName;

            string pkTable = "DELETE " + tableName + ";";
            sw.WriteLine(pkTable);
            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                if (column.IsAutoNumber)
                {
                    var seqName = GetSeqName(tableName);
                    sw.WriteLine(GetDeleteExistSequence(seqName));
                    sw.WriteLine("CREATE SEQUENCE " + seqName + " start with 1;");
                }
            }
            sw.WriteLine("/");
        }

        private void CreateTable_Sql(StreamWriter sw, DatabaseTable table)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                CreateTable_MySql(sw, table);
                return;
            }
            string tableName = table.TableName;
            sw.WriteLine();

            var deleteTable = new StringBuilder();
            var headerInputs = new StringBuilder();
            var strInputParams = new StringBuilder();
            var strOutSelect = new StringBuilder();

            int count = table.Columns.Count;

            deleteTable.AppendLine(GetDeleteExistTable(tableName));

            //            headerInputs.AppendLine("SET ANSI_NULLS ON");
            //            headerInputs.AppendLine("GO");
            //            headerInputs.AppendLine("SET QUOTED_IDENTIFIER ON");
            //            headerInputs.AppendLine("GO");
            //            headerInputs.AppendLine("SET ANSI_PADDING ON");
            //            headerInputs.AppendLine("GO");

            strInputParams.AppendLine("BEGIN");
            strInputParams.AppendLine("CREATE TABLE [dbo].[" + tableName + "](\r\n");
            var listColums = new List<DatabaseColumn>();
            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                //Nếu là tự tăng thì thêm identity
                string stringIdentity = column.IsAutoNumber ? "IDENTITY(1, 1) " : " ";
                if (i == count - 1) end = "";

                string notNull = column.IsNull ? "NULL " : "NOT NULL ";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameterCreate(column) + " " + notNull + stringIdentity + end);

                if (column.IsPK || column.IsFK)
                {
                    // Output select
                    listColums.Add(column);
                }
            }

            for (int i = 0; i < listColums.Count; i++)
            {
                DatabaseColumn column = listColums[i];
                string ascEnd = "ASC";
                //Đối với trường hợp có 2 key trở lên
                if (i != listColums.Count - 1) ascEnd = "ASC,";
                //Dùng để insert ASC
                strOutSelect.AppendLine("\t[" + column.Name + "] " + ascEnd);
            }

            sw.WriteLine(headerInputs.ToString());
            sw.WriteLine(deleteTable.ToString());
            // Parameters
            sw.Write(strInputParams.ToString());

            string pkTable = "CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED ";
            sw.WriteLine(pkTable);
            sw.WriteLine("(");
            sw.Write(strOutSelect);
            sw.Write(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ");
            sw.WriteLine(") ON [PRIMARY] ");
            sw.WriteLine("END");
            sw.WriteLine("GO");
            sw.WriteLine();

        }

        private void CreateTable_MySql(StreamWriter sw, DatabaseTable table)
        {
            string tableName = table.TableName;
            sw.WriteLine();

            var deleteTable = new StringBuilder();
            var headerInputs = new StringBuilder();
            var strInputParams = new StringBuilder();

            int count = table.Columns.Count;

            deleteTable.AppendLine(GetDeleteExistTable(tableName));

            strInputParams.AppendLine("CREATE TABLE `" + tableName + "`(\r\n");
            var listColums = new List<DatabaseColumn>();
            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                //Nếu là tự tăng thì thêm identity
                string stringIdentity = column.IsAutoNumber ? " AUTO_INCREMENT " : " ";
                if (i == count - 1) end = ","; //Van them dau phay de dua primary key

                string notNull = column.IsNull ? "NULL " : "NOT NULL ";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameterCreate(column) + " " + notNull + stringIdentity + end);

                if (column.IsPK || column.IsFK)
                {
                    // Output select
                    listColums.Add(column);
                }
            }

            var pkTable = "PRIMARY KEY (";
            for (int i = 0; i < listColums.Count; i++)
            {
                DatabaseColumn column = listColums[i];
                if (i != listColums.Count - 1)
                    pkTable += "`" + column.Name + "`,";
                else pkTable += "`" + column.Name + "`";
            }

            pkTable += ")";

            sw.WriteLine(headerInputs.ToString());
            sw.WriteLine(deleteTable.ToString());
            // Parameters
            sw.Write(strInputParams.ToString());
            
            sw.WriteLine(pkTable);
            sw.WriteLine(");");
            sw.WriteLine();

        }

        private void TruncateTable_Sql(StreamWriter sw, DatabaseTable table)
        {
            string tableName = table.TableName;
            string pkTable = "Truncate table [" + tableName + "] ";
            sw.WriteLine(pkTable);
        }

        private void DropSp(StreamWriter sw, DatabaseTable table)
        {
            var insertSp = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Insert);
            var updateSp = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Update);
            var deleteSp = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Delete);
            var selectSp = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                 StoreProcedureType.Select);
            sw.WriteLine();
            sw.WriteLine("BEGIN EXECUTE IMMEDIATE 'DROP PROCEDURE " + insertSp + "'; EXCEPTION WHEN OTHERS THEN IF SQLCODE != -4043 THEN RAISE; END IF; END;");
            sw.WriteLine("/");
            sw.WriteLine("BEGIN EXECUTE IMMEDIATE 'DROP PROCEDURE " + updateSp + "'; EXCEPTION WHEN OTHERS THEN IF SQLCODE != -4043 THEN RAISE; END IF; END;");
            sw.WriteLine("/");
            sw.WriteLine("BEGIN EXECUTE IMMEDIATE 'DROP PROCEDURE " + deleteSp + "'; EXCEPTION WHEN OTHERS THEN IF SQLCODE != -4043 THEN RAISE; END IF; END;");
            sw.WriteLine("/");
            sw.WriteLine("BEGIN EXECUTE IMMEDIATE 'DROP PROCEDURE " + selectSp + "'; EXCEPTION WHEN OTHERS THEN IF SQLCODE != -4043 THEN RAISE; END IF; END;");
            sw.WriteLine("/");
        }

        private void AutoNumber(StreamWriter sw, DatabaseTable table)
        {
            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                // Output select
                if (column.IsAutoNumber)
                {
                    sw.WriteLine(table.TableName + ":" + column.Name);
                }
            }
        }

        private string GetDeleteExistProcedureSQL(string storeProcedures)
        {
            if (MySqlGenerator.IsConnectMySql)
                return "DROP PROCEDURE IF EXISTS " + storeProcedures + ";\r\n";

            return "if exists (select * from dbo.sysobjects where id = object_id(N'" + storeProcedures +
                   "') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure " + storeProcedures + "\r\nGO";
        }

        private void InsertSp(StreamWriter sw, DatabaseTable table)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                InsertSP_MySql(sw, table);
                return;
            }
            if (!OracleHelper.IsConectOracle)
            {
                InsertSP_Sql(sw, table);
                return;
            }

            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
            StoreProcedureType.Insert);
            sw.WriteLine();

            var strInputParams = new StringBuilder();
            var strFields = new StringBuilder();
            var strValues = new StringBuilder();
            var strGetSeq = new StringBuilder();

            strInputParams.AppendLine("(");
            var count = table.Columns.Count;

            var i = 0;
            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                var end = ",";
                var pType = column.IsAutoNumber ? "OUT" : "IN";
                if (i == count - 1) end = "";
                // Parameters
                strInputParams.AppendLine("\t" + GetParameter(column, pType) + end);
                // Fileds
                strFields.AppendLine("\t\t" + column.Name + "" + end);
                // Values
                strValues.AppendLine("\t\t" + column.SqlParameterName + end);

                // Output select
                if (column.IsAutoNumber)
                {
                    if (!TierGeneratorSettings.Instance.UseMaxKey)
                        strGetSeq.AppendLine("\tSELECT " + GetSeqName(table.TableName) + ".NEXTVAL INTO " + column.SqlParameterName + " FROM DUAL;");
                    else
                        strGetSeq.AppendLine("\tSELECT (NVL(MAX(" + column.Name + "),0)+1) INTO " + column.SqlParameterName + " FROM " + table.TableName + ";");
                }
                i++;
            }

            strInputParams.AppendLine(")");

            sw.WriteLine("CREATE OR REPLACE PROCEDURE " + spName);
            sw.WriteLine(strInputParams.ToString());
            sw.WriteLine("AS");
            sw.WriteLine("BEGIN");
            sw.WriteLine(strGetSeq);
            sw.WriteLine("\tINSERT INTO " + table.TableName + "");
            sw.WriteLine("\t(");
            // Fileds
            sw.WriteLine(strFields);
            sw.WriteLine("\t)");
            sw.WriteLine("\tVALUES");
            sw.WriteLine("\t(");
            // Values
            sw.WriteLine(strValues);
            sw.WriteLine("\t);");
            sw.WriteLine("END;");
            sw.WriteLine("/");
        }

        private void InsertSP_Sql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
            StoreProcedureType.Insert);
            
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strInputParams = new StringBuilder();
            var strFields = new StringBuilder();
            var strValues = new StringBuilder();
            var strOutSelect = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                string pType = column.IsAutoNumber ? "output" : "";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameterSql(column) + " " + pType + end);

                if (!column.IsAutoNumber)
                {
                    // Fileds
                    strFields.AppendLine("\t[" + column.Name + "]" + end);

                    // Values
                    strValues.AppendLine("\t" + column.SqlParameterName + end);
                }

                // Output select
                if (column.IsAutoNumber)
                {
                    strOutSelect.AppendLine("\tSELECT " + column.SqlParameterName + "=SCOPE_IDENTITY();");
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("INSERT [" + table.TableSchema + "].[" + table.TableName + "]");
            sw.WriteLine("(");

            // Fileds
            sw.WriteLine(strFields.ToString());

            sw.WriteLine(")");
            sw.WriteLine("VALUES");
            sw.WriteLine("(");

            // Values
            sw.WriteLine(strValues.ToString());

            sw.WriteLine(")");

            sw.WriteLine(strOutSelect.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");
        }

        private void SelectAllSp(StreamWriter sw, DatabaseTable table)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                SelectAllSP_MySql(sw, table);
                return;
            }
            if (!OracleHelper.IsConectOracle)
            {
                SelectAllSP_Sql(sw, table);
                return;
            }
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Select);
            sw.WriteLine();
            sw.WriteLine("CREATE OR REPLACE PROCEDURE " + spName + "(p_recordset OUT SYS_REFCURSOR)");
            // Parameters
            sw.WriteLine("AS");
            sw.WriteLine("BEGIN");
            sw.WriteLine("\tOPEN p_recordset FOR");
            sw.WriteLine("\t\tSELECT * FROM " + table.TableName + ";");
            sw.WriteLine("END;");
            sw.WriteLine("/");
        }

        private void SelectAllSP_Sql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.SelectAll);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));


            var strParams = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];


                // Select Parameters
                if (strParams.Length > 0) strParams.Append(", ");
                strParams.Append("[" + column.Name + "]");
            }

            sw.WriteLine("CREATE PROCEDURE " + spName);
            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("\tSELECT ");
            sw.WriteLine("\t\t" + strParams);
            sw.WriteLine("\tFROM [" + table.TableSchema + "].[" + table.TableName + "]");

            sw.WriteLine("");
            sw.WriteLine("GO");
        }

        private void UpdateSp(StreamWriter sw, DatabaseTable table)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                UpdateSP_MySql(sw, table);
                return;
            }
            if (!OracleHelper.IsConectOracle)
            {
                UpdateSP_Sql(sw, table);
                return;
            }
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Update);
            sw.WriteLine();

            var strInputParams = new StringBuilder();
            var strSet = new StringBuilder();
            var strWhere = new StringBuilder();


            int count = table.Columns.Count;
            var i = 0;
            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                var end = ",";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameter(column, "") + end);

                // Set Value
                if (!column.IsAutoNumber)
                {
                    if (strSet.Length > 0) strSet.Append(",\r\n");
                    strSet.Append("\t\t" + column.Name + " = " + column.SqlParameterName);
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t\t" + column.Name + " = " + column.SqlParameterName);
                }
                i++;
            }

            sw.WriteLine("CREATE OR REPLACE PROCEDURE " + spName);
            // Parameters
            sw.WriteLine("(");
            sw.WriteLine(strInputParams.ToString());
            sw.WriteLine(")");
            sw.WriteLine("AS");
            sw.WriteLine("BEGIN");
            sw.WriteLine("\tUPDATE " + table.TableName + "");
            sw.WriteLine("\tSET");
            // Set Values
            sw.WriteLine(strSet.ToString());
            // where clause
            sw.WriteLine("\tWHERE ");
            // Where
            sw.WriteLine(strWhere + ";");
            sw.WriteLine("END;");
            sw.WriteLine("/");
        }


        private void UpdateSP_Sql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Update);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strInputParams = new StringBuilder();
            var strSet = new StringBuilder();
            var strWhere = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameterSql(column) + end);

                // Set Value
                if (!column.IsAutoNumber)
                {
                    if (strSet.Length > 0) strSet.Append(",\r\n");
                    strSet.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("UPDATE [" + table.TableSchema + "].[" + table.TableName + "]");
            sw.WriteLine("SET");

            // Set Values
            sw.WriteLine(strSet.ToString());

            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString());


            sw.WriteLine("");
            sw.WriteLine("GO");
        }

        private void DeleteByPrimaryKeySp(StreamWriter sw, DatabaseTable table)
        {
            if (MySqlGenerator.IsConnectMySql)
            {
                DeleteByPrimaryKeySP_MySql(sw, table);
                return;
            }
            if (!OracleHelper.IsConectOracle)
            {
                DeleteByPrimaryKeySP_Sql(sw, table);
                return;
            }
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Delete);
            sw.WriteLine();

            var strInputParams = new StringBuilder();
            var strWhere = new StringBuilder();

            foreach (var column in table.Columns.OrderBy(x => x.Name))
            {
                // Parameters
                if (column.IsPK)
                {
                    if (strInputParams.Length > 0) strInputParams.Append(",\r\n");
                    strInputParams.Append("\t" + GetParameter(column, ""));
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t" + column.Name + " = " + column.SqlParameterName);
                }
            }
            sw.WriteLine("CREATE OR REPLACE PROCEDURE " + spName);
            // Parameters
            sw.WriteLine("(");
            sw.WriteLine(strInputParams.ToString());
            sw.WriteLine(")");
            sw.WriteLine("AS");
            sw.WriteLine("BEGIN");
            sw.WriteLine("\tDELETE FROM " + table.TableName + "");
            // where clause
            sw.WriteLine("\tWHERE ");
            // Where
            sw.WriteLine(strWhere + ";");
            sw.WriteLine("END;");
            sw.WriteLine("/");
        }

        private void DeleteByPrimaryKeySP_Sql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.DeleteByPrimaryKey);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strInputParams = new StringBuilder();
            var strWhere = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];

                // Parameters
                if (column.IsPK)
                {
                    if (strInputParams.Length > 0) strInputParams.Append(",\r\n");
                    strInputParams.Append("\t" + GetParameterSql(column));
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("DELETE FROM [" + table.TableSchema + "].[" + table.TableName + "]");
            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");
        }

        #region MySql store procedure
        private void SelectAllSP_MySql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.SelectAll);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strParams = new StringBuilder();
            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];


                // Select Parameters
                if (strParams.Length > 0) strParams.Append(", ");
                strParams.Append("`" + column.Name + "`");
            }

            sw.WriteLine("CREATE PROCEDURE " + spName + "()");
            sw.WriteLine("BEGIN");
            sw.WriteLine("");

            sw.WriteLine("\tSELECT ");
            sw.WriteLine("\t\t" + strParams);
            sw.WriteLine("\tFROM `" + table.TableSchema + "`.`" + table.TableName + "`;");

            sw.WriteLine("END;");
        }

        private void DeleteByPrimaryKeySP_MySql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.DeleteByPrimaryKey);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strInputParams = new StringBuilder();
            var strWhere = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];

                // Parameters
                if (column.IsPK)
                {
                    if (strInputParams.Length > 0) strInputParams.Append(",\r\n");
                    strInputParams.Append("\t" + GetParameterMySql(column));
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t`" + column.Name + "` = " + column.SqlParameterName);
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName + " (");

            // Parameters
            sw.WriteLine(strInputParams.ToString() + ")");

            sw.WriteLine("BEGIN");
            sw.WriteLine("");

            sw.WriteLine("DELETE FROM `" + table.TableSchema + "`.`" + table.TableName + "`");
            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString() + ";");

            sw.WriteLine("END;");
            sw.WriteLine("");
        }

        private void UpdateSP_MySql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
                StoreProcedureType.Update);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));


            var strInputParams = new StringBuilder();
            var strSet = new StringBuilder();
            var strWhere = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameterMySql(column) + end);

                // Set Value
                if (!column.IsAutoNumber)
                {
                    if (strSet.Length > 0) strSet.Append(",\r\n");
                    strSet.Append("\t`" + column.Name + "` = " + column.SqlParameterName);
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t`" + column.Name + "` = " + column.SqlParameterName);
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName + " (");

            // Parameters
            sw.WriteLine(strInputParams.ToString() + ")") ;
            
            sw.WriteLine("BEGIN");
            sw.WriteLine("");

            sw.WriteLine("UPDATE `" + table.TableSchema + "`.`" + table.TableName + "`");
            sw.WriteLine("SET");

            // Set Values
            sw.WriteLine(strSet.ToString());

            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString() + ";");

            sw.WriteLine("END;");
            sw.WriteLine("");
        }

        private void InsertSP_MySql(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName,
            StoreProcedureType.Insert);

            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            var strInputParams = new StringBuilder();
            var strFields = new StringBuilder();
            var strValues = new StringBuilder();
            var strOutSelect = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                string pType = column.IsAutoNumber ? "out " : "";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + pType + GetParameterMySql(column) + " " + end);

                if (!column.IsAutoNumber)
                {
                    // Fileds
                    strFields.AppendLine("\t`" + column.Name + "`" + end);

                    // Values
                    strValues.AppendLine("\t" + column.SqlParameterName + end);
                }

                // Output select
                if (column.IsAutoNumber)
                {
                    strOutSelect.AppendLine("\tSET " + column.SqlParameterName + "=LAST_INSERT_ID();");
                }
            }

            sw.WriteLine("CREATE PROCEDURE " + spName + " (");

            // Parameters
            sw.WriteLine(strInputParams.ToString() + ")");

            sw.WriteLine("BEGIN");
            sw.WriteLine("");

            sw.WriteLine("INSERT `" + table.TableSchema + "`.`" + table.TableName + "`");
            sw.WriteLine("(");

            // Fileds
            sw.WriteLine(strFields.ToString());

            sw.WriteLine(")");
            sw.WriteLine("VALUES");
            sw.WriteLine("(");

            // Values
            sw.WriteLine(strValues.ToString());

            sw.WriteLine(");");

            sw.WriteLine(strOutSelect.ToString());

            sw.WriteLine("END;");
            sw.WriteLine("");
        }

        private string GetParameterMySql(DatabaseColumn column)
        {
            string parameter = column.SqlParameterName + " " + column.DataType;
            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                {
                    string size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                    parameter += "(" + size + ")";
                    break;
                }
                case "decimal":
                {
                    parameter += "(" + column.NumericPrecision + "," + column.NumericScale + ")";
                    break;
                }
            }


            if (column.IsNull)
                parameter += " /* = null */";

            return parameter;
        }
        #endregion

        #endregion

        #endregion
    }
}
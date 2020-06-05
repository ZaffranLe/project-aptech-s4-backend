using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.DatabaseSchema;
using CommonicationMemory.Properties;
using MySql.Data.MySqlClient;

namespace CommonicationMemory.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Oracle.ManagedDataAccess.Client;

    public class BusinessLayerGenerator
    {
        #region Properties

        public string BusinessLayerRootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace +
                       Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace + ".Business";

            }
        }
        public string PropertiesPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "Properties";

            }
        }
        public string DataLayerPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "EntitySql";

            }
        }

        public string EntityPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "Entity";

            }
        }

        public string DataLayer2Path
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "DataLayer";

            }
        }
        public string ValidationPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "Validation";

            }
        }
        public string DAOPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "DAO";
            }
        }
        public string CommonPath
        {
            get
            {
                return BusinessLayerRootPath +
                       Path.DirectorySeparatorChar +
                       "Common";

            }
        }

        public string FoudationLink { get; set; }

        #endregion

        #region Public Methods

        public void Generate()
        {
            GenerateEntitesSql();
            if (TierGeneratorSettings.Instance.GenProcess)
                GenerateProcessEntity();
        }

        #endregion

        #region Private Methods

        #region Data Layer



        private void GenerateEntitesSql()
        {
            var database = TierGeneratorSettings.Instance.Database;

            string forderEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar;
            if (!Directory.Exists(forderEntity))
                Directory.CreateDirectory(forderEntity);
            var file = forderEntity + "GetSqlFile.cs";
            var stringBuilder = new StringBuilder();
            var stringTmp = "if (entityName.Equals({0}.EntityName())) { return new {0}Sql(); }";

            foreach (DatabaseTable table in database.Tables)
            {
                if (!table.IsSelected) continue;
                stringBuilder.AppendLine(stringTmp.Replace("{0}", table.TableName));

                #region Generate DataLayer

                CreateFileEntity.GenerateEntites(table, BusinessLayerRootPath);
                CreateFileController.GenerateControllerApi(table, BusinessLayerRootPath);
                GenerateFileDal(table);

                #endregion
            }

            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(stringBuilder);
            }
        }

        private void GenerateProcessEntity()
        {
            var database = TierGeneratorSettings.Instance.Database;

            #region Generate DataLayer

            string forderUpdate = BusinessLayerRootPath + Path.DirectorySeparatorChar + "\\Update";
            if (!Directory.Exists(forderUpdate))
                Directory.CreateDirectory(forderUpdate);

            string forderEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar + "\\GetList";
            if (!Directory.Exists(forderEntity))
                Directory.CreateDirectory(forderEntity);

            string forderRequest = BusinessLayerRootPath + Path.DirectorySeparatorChar + "\\Request";
            if (!Directory.Exists(forderRequest))
                Directory.CreateDirectory(forderRequest);

            string forderResponse = BusinessLayerRootPath + Path.DirectorySeparatorChar + "\\Response";
            if (!Directory.Exists(forderResponse))
                Directory.CreateDirectory(forderResponse);

            foreach (DatabaseTable table in database.Tables)
            {
                if (!table.IsSelected) continue;

                string forderSub = forderEntity + "\\GetList" + table.TableName + "Request";
                if (!Directory.Exists(forderSub))
                    Directory.CreateDirectory(forderSub);

                string forderSubUpdate = forderUpdate + "\\Update" + table.TableName + "Request";
                if (!Directory.Exists(forderSubUpdate))
                    Directory.CreateDirectory(forderSubUpdate);

                #region Create Request

                var fileRequest = forderRequest + "\\GetList" + table.TableName + "Request.cs";
                var stringBuild = new StringBuilder();
                //using (StreamWriter sw = new StreamWriter(fileRequest))
                {
                    stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                    stringBuild.AppendLine("namespace ElectricShop.Message.Request.DataProvider");
                    stringBuild.AppendLine("{");
                    stringBuild.AppendLine("    public class GetList" + table.TableName + "Request : Request");
                    stringBuild.AppendLine("    {");
                    stringBuild.AppendLine("        public GetList" + table.TableName + "Request()");
                    stringBuild.AppendLine("            : base(MessageType.DataProvider)");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("            IsRunOnHoliday = true;");
                    stringBuild.AppendLine("            IsNotPersist = true;");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("        public RequestType Target { get; set; }");
                    stringBuild.AppendLine("    }");
                    stringBuild.AppendLine("}");
                }
                using (StreamWriter sw = new StreamWriter(fileRequest))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                stringBuild.Clear();

                var fileUpdateRequest = forderRequest + "\\Update" + table.TableName + "Request.cs";
                //using (StreamWriter sw = new StreamWriter(fileUpdateRequest))
                {
                    stringBuild.AppendLine("using System.Collections.Generic;");
                    stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                    stringBuild.AppendLine("using ElectricShop.Entity.Entities;");
                    stringBuild.AppendLine("namespace ElectricShop.Message.Request.Risk");
                    stringBuild.AppendLine("{");
                    stringBuild.AppendLine("    public class Update" + table.TableName + "Request : Request");
                    stringBuild.AppendLine("    {");
                    stringBuild.AppendLine("        public Update" + table.TableName + "Request()");
                    stringBuild.AppendLine("            : base(MessageType.Risk)");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("            List" + table.TableName + " = new List<" + table.TableName + ">();");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("        public RequestType RequestType { get; set; }");
                    stringBuild.AppendLine("        public List<" + table.TableName + "> List" + table.TableName + " { get; set; }");
                    stringBuild.AppendLine("        public override void Dispose()");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("            List" + table.TableName + " = null;");
                    stringBuild.AppendLine("            TemporaryTopic = null;");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("    }");
                    stringBuild.AppendLine("}");
                }
                using (StreamWriter sw = new StreamWriter(fileUpdateRequest))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                stringBuild.Clear();

                #endregion

                #region Create Response

                var fileResponse = forderResponse + "\\GetList" + table.TableName + "Response.cs";
                //using (StreamWriter sw = new StreamWriter(fileResponse))
                {
                    stringBuild.AppendLine("using System.Collections.Generic;");
                    stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                    stringBuild.AppendLine("using ElectricShop.Entity.Entities;");
                    stringBuild.AppendLine("namespace ElectricShop.Message.Response.DataProvider");
                    stringBuild.AppendLine("{");
                    stringBuild.AppendLine("    public class GetList" + table.TableName + "Response : Response");
                    stringBuild.AppendLine("    {");
                    stringBuild.AppendLine("        public GetListUserInfoResponse()");
                    stringBuild.AppendLine("            : base(MessageType.DataProvider)");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("        public List<" + table.TableName + "> List" + table.TableName + " { get; set; }");
                    stringBuild.AppendLine("    }");
                    stringBuild.AppendLine("}");
                }
                using (StreamWriter sw = new StreamWriter(fileResponse))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                stringBuild.Clear();

                var fileUpdateResponse = forderResponse + "\\Update" + table.TableName + "Response.cs";
                //using (StreamWriter sw = new StreamWriter(fileUpdateResponse))
                {
                    stringBuild.AppendLine("using Language.Languages;");
                    stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                    stringBuild.AppendLine("namespace ElectricShop.Message.Response.Risk");
                    stringBuild.AppendLine("{");
                    stringBuild.AppendLine("    public class Update" + table.TableName + "Response : Response");
                    stringBuild.AppendLine("    {");
                    stringBuild.AppendLine("        public Update" + table.TableName + "Response()");
                    stringBuild.AppendLine("            : base(MessageType.Risk)");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("        public ResourcesKeyEnum ErrorCode { get; set; }");
                    stringBuild.AppendLine("        public override void Dispose()");
                    stringBuild.AppendLine("        {");
                    stringBuild.AppendLine("        }");
                    stringBuild.AppendLine("    }");
                    stringBuild.AppendLine("}");
                }
                using (StreamWriter sw = new StreamWriter(fileUpdateResponse))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                stringBuild.Clear();

                #endregion

                #region Create Stateless

                var fileStateLess = forderSub + "\\Flow_GetList" + table.TableName + "Request.stateless";
                using (StreamWriter sw = new StreamWriter(fileStateLess))
                {
                    var fileState = Resources.Stateless;
                    fileState = fileState.Replace("%EntityName%", table.TableName);
                    sw.Write(fileState);
                }

                var fileUpdateStateLess = forderSubUpdate + "\\Flow_Update" + table.TableName + "Request.stateless";
                using (StreamWriter sw = new StreamWriter(fileUpdateStateLess))
                {
                    var fileState = Resources.SateUpdate;
                    fileState = fileState.Replace("%EntityName%", table.TableName);
                    sw.Write(fileState);
                }

                #endregion

                #region Create Stateless

                var fileStateLessCs = forderSub + "\\Flow_GetList" + table.TableName + "Request.cs";
                using (StreamWriter sw = new StreamWriter(fileStateLessCs))
                {
                    var fileStateCs = Resources.Flow;
                    fileStateCs = fileStateCs.Replace("%EntityName%", table.TableName);
                    sw.Write(fileStateCs);
                }

                var fileUpdateStateLessCs = forderSubUpdate + "\\Flow_Update" + table.TableName + "Request.cs";
                using (StreamWriter sw = new StreamWriter(fileUpdateStateLessCs))
                {
                    var fileState = Resources.FlowUpdate;
                    fileState = fileState.Replace("%EntityName%", table.TableName);
                    sw.Write(fileState);
                }

                #endregion

                #region Create Create file cs

                var fileProcessCs = forderSub + "\\Process_GetList" + table.TableName + "Request.cs";
                using (StreamWriter sw = new StreamWriter(fileProcessCs))
                {
                    var fileCs = Resources.Process;
                    fileCs = fileCs.Replace("%EntityName%", table.TableName);
                    sw.Write(fileCs);
                }

                var fileUpdateProcessCs = forderSubUpdate + "\\Process_Update" + table.TableName + "Request.cs";

                using (StreamWriter sw = new StreamWriter(fileUpdateProcessCs))
                {
                    var fileCs = Resources.ProcessUpdate;
                    fileCs = fileCs.Replace("%EntityName%", table.TableName);
                    fileCs = fileCs.Replace("%EntityKey%", GetKeyTable(table));
                    sw.Write(fileCs);
                }

                #endregion
            }

            #endregion
        }

        private string GetKeyTable(DatabaseTable table)
        {
            var isMutilKey = table.Columns.Count(x => x.IsPK) > 1;
            var key = table.TableName + "Keys";
            if (!isMutilKey)
            {
                var keyColumn = table.Columns.FirstOrDefault(x => x.IsPK);
                if (keyColumn != null)
                    key = keyColumn.Name;
            }
            return key;
        }

        private void GenerateFileDal(DatabaseTable table)
        {
            string fileText = Resources.DataLayer_EntitySql;

            if (MySqlGenerator.IsConnectMySql)
            {
                //Neu generator MySql thi thay doi text
                fileText = fileText.Replace("using Oracle.ManagedDataAccess.Client;", "using MySql.Data.MySqlClient;");
                fileText = fileText.Replace("OracleCommand", "MySqlCommand");

                fileText = fileText.Replace("namespace ElectricShop.DatabaseDAL.EntitySql",
                    "namespace ElectricShop.DatabaseDAL.EntityMySql");
                fileText = fileText.Replace("using ElectricShop.DatabaseDAL.Common;", "using ElectricShop.DatabaseDAL.CommonMySql;");
                fileText = fileText.Replace("EntityBaseSql", "EntityBaseMySql");
            }
            else if (!OracleHelper.IsConectOracle)
            {
                //Neu generator Sql thi thay doi text
                fileText = fileText.Replace("using Oracle.ManagedDataAccess.Client;", "using System.Data.SqlClient;");
                fileText = fileText.Replace("OracleCommand", "SqlCommand");
            }
            
            
            var insert = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName, StoreProcedureType.Insert);
            fileText = fileText.Replace("$storeProceduceNameInsert$", insert);
            var update = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName, StoreProcedureType.Update);
            fileText = fileText.Replace("$storeProceduceNameUpdate$", update);
            var delete = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName, StoreProcedureType.Delete);
            if (!OracleHelper.IsConectOracle)
            {
                delete = delete.Replace("_Delete", "_DeleteByPrimaryKey");
            }
            fileText = fileText.Replace("$storeProceduceNameDelete$", delete);
            var select = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableName, StoreProcedureType.Select);
            if (!OracleHelper.IsConectOracle)
            {
                select = select.Replace("_Select", "_SelectAll");
            }
            fileText = fileText.Replace("$storeProceduceNameSelect$", select);

            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            fileText = fileText.Replace(ProjectTokens.ClassName, table.ClassName);

            fileText = fileText.Replace(ProjectTokens.TableName, table.TableName);
            fileText = fileText.Replace(ProjectTokens.TableSchema, table.TableSchema);
            fileText = fileText.Replace(ProjectTokens.SpPrefix, TierGeneratorSettings.Instance.StoreProcedurePrefix);

            #region Insert Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlInsertParameter, GetParameterForInsertOrUpdate(table, true));
            fileText = fileText.Replace(ProjectTokens.EntitySqlGetReturnedValue, GetOutputParameterForInsert(table));
            fileText = fileText.Replace(ProjectTokens.EntitySqlGetReturnedValueCheck, GetOutputParameterForInsertCheck(table));

            #endregion

            #region Update Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlUpdateParameter, GetParameterForInsertOrUpdate(table, false));

            #endregion

            #region Select By Primary Key

            if (OracleHelper.IsConectOracle)
            {
                StringBuilder textCommandSelect = new StringBuilder();
                textCommandSelect.AppendLine(" public override OracleCommand GetSqlCommandForSelect(string entityName)");
                textCommandSelect.AppendLine("{");
                textCommandSelect.AppendLine("    var sqlCommand = new OracleCommand { CommandType = CommandTypeProcedure };");
                textCommandSelect.AppendLine("    if (!string.IsNullOrWhiteSpace(entityName))");
                textCommandSelect.AppendLine("    {");
                textCommandSelect.AppendLine("        if (entityName.Equals($CLASS_NAME$.EntityName()))");
                textCommandSelect.AppendLine("        {");
                textCommandSelect.AppendLine("            sqlCommand.Parameters.Add(new OracleParameter(\"p_recordset\", OracleDbType.RefCursor, 50, ParameterDirection.Output));");
                textCommandSelect.AppendLine("        }");
                textCommandSelect.AppendLine("    }");
                textCommandSelect.AppendLine("    return sqlCommand;");
                textCommandSelect.AppendLine("}");

                fileText = fileText.Replace(ProjectTokens.EntitySqlSelectByPkParameter, GetParameterForPrimaryKey(table));
            }
            else if (MySqlGenerator.IsConnectMySql)
            {
                //Neu la MySql
                fileText = fileText.Replace(ProjectTokens.EntitySqlSelectByPkParameter, GetParameterForPrimaryKey(table));
            }
            else
            {
                //Neu la sql
                fileText = fileText.Replace(ProjectTokens.EntitySqlSelectByPkParameter, GetParameterForPrimaryKey(table));
            }

            #endregion

            #region Populate Object From Reader

            fileText = fileText.Replace(ProjectTokens.EntitySqlPopulateBusinessObjectParameter, GetPopulateObjectFromReader(table));

            #endregion

            FileWriter.WriteFile(DataLayerPath, table.ClassName + "Sql.cs", fileText);

            //Lưu vào thư mục foudation nếu có
            if (!string.IsNullOrEmpty(ConfigGlobal.SettingConfig.Setting_FoudationLink) && ConfigGlobal.SettingConfig.Setting_CheckGenByForder)
            {
                string fileCs = ConfigGlobal.SettingConfig.Setting_FoudationLink + "\\DatabaseDAL\\EntitySql\\" + table.ClassName + "Sql.cs";
                if (MySqlGenerator.IsConnectMySql)
                {
                    //Neu la MySql thi dua vao thu muc khac
                    fileCs = ConfigGlobal.SettingConfig.Setting_FoudationLink + "\\DatabaseDAL\\EntityMySql\\" + table.ClassName + "Sql.cs";
                }
                if (File.Exists(fileCs))
                {
                    //Tồn tại mới lưu
                    File.Delete(fileCs);
                    using (var sw = new StreamWriter(fileCs))
                    {
                        sw.WriteLine(fileText);
                        sw.Close();
                    }
                }
            }
        }

        private string GetParameterForInsertOrUpdate(DatabaseTable table, bool isInsert)
        {
            if (!OracleHelper.IsConectOracle)
            {
                //Neu la sql
                return GetParameterForInsertOrUpdateSql(table, isInsert);
            }

            //Neu la oracle
            var strToReturn = new StringBuilder();
            var format = "\t\t\t\tsqlCommand.Parameters.Add(new OracleParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, {5}, {6}, \"\", DataRowVersion.Proposed, {4}));";
            var formatBool = "\t\t\t\tsqlCommand.Parameters.Add(new OracleParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, {5}, {6}, \"\", DataRowVersion.Proposed, {4} ? 1 : 0));";

            var isMultilKeys = IsMultiKeys(table.Columns);

            foreach (DatabaseColumn column in table.Columns.OrderBy(x => x.Name))
            {

                string dir = "Input";
                if (isInsert)
                {
                    dir = column.IsAutoNumber ? "Output" : "Input";
                }
                string dbType = GetOracleDbType(column.DataType, column.NumericPrecision, column.NumericScale, column.ColumnSize ?? 0);
                if (column.IsPK && isMultilKeys && TierGeneratorSettings.Instance.GenObjectKeys)
                {
                    if (column.DotNetDataTypeName == "Boolean")
                        strToReturn.AppendLine(string.Format(formatBool, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + table.TableName + "Keys." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                    else
                        strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + table.TableName + "Keys." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                }
                else
                {
                    if (column.DotNetDataTypeName == "Boolean")
                        strToReturn.AppendLine(string.Format(formatBool, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                    else
                        strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                }
            }

            return strToReturn.ToString();

        }

        private string GetParameterForInsertOrUpdateSql(DatabaseTable table, bool isInsert)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "\t\t\t\tsqlCommand.Parameters.Add(new SqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            if (MySqlGenerator.IsConnectMySql)
            {
                format = "\t\t\t\tsqlCommand.Parameters.Add(new MySqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            }

            foreach (DatabaseColumn column in table.Columns)
            {

                string dir = "Input";
                if (isInsert)
                {
                    dir = column.IsAutoNumber ? "Output" : "Input";
                }
                string dbType = GetSqlDbType(column.DataType);
                strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName));

            }

            return strToReturn.ToString();
        }

        private bool IsMultiKeys(IEnumerable<DatabaseColumn> listColumns)
        {
            return listColumns.Count(x => x.IsPK) > 1;
        }

        private string GetParameterForPrimaryKey(DatabaseTable table)
        {
            if (!OracleHelper.IsConectOracle)
            {
                //Neu la sql
                return GetParameterForPrimaryKeySql(table);
            }

            //Neu la oracle
            StringBuilder strToReturn = new StringBuilder();
            string format     = "\t\t\t\tsqlCommand.Parameters.Add(new OracleParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, {5}, {6}, \"\", DataRowVersion.Proposed, {4}));";
            string formatBool = "\t\t\t\tsqlCommand.Parameters.Add(new OracleParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, {5}, {6}, \"\", DataRowVersion.Proposed, {4} ? 1 : 0));";
            var isMultilKeys = IsMultiKeys(table.Columns);
            foreach (DatabaseColumn column in table.Columns.OrderBy(x => x.Name))
            {
                if (column.IsPK)
                {
                    string dir = "Input";

                    string dbType = GetOracleDbType(column.DataType, column.NumericPrecision, column.NumericScale, column.ColumnSize ?? 0);
                    if (isMultilKeys && TierGeneratorSettings.Instance.GenObjectKeys)
                    {
                        if (column.DotNetDataTypeName == "Boolean")
                            strToReturn.AppendLine(string.Format(formatBool, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + table.TableName + "Keys." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                        else
                            strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + table.TableName + "Keys." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                    }
                    else
                    {
                        if (column.DotNetDataTypeName == "Boolean")
                            strToReturn.AppendLine(string.Format(formatBool, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                        else
                            strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName, GetNumberScale(dbType, column.NumericPrecision), GetNumberScale(dbType, column.NumericScale)));
                    }
                }
            }

            return strToReturn.ToString();

        }
        private string GetParameterForPrimaryKeySql(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "\t\t\t\tsqlCommand.Parameters.Add(new SqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            if (MySqlGenerator.IsConnectMySql)
            {
                format = "\t\t\t\tsqlCommand.Parameters.Add(new MySqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            }

            foreach (DatabaseColumn column in table.Columns)
            {
                if (column.IsPK)
                {
                    string dir = "Input";

                    string dbType = GetSqlDbType(column.DataType);
                    strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName));
                }
            }

            return strToReturn.ToString();

        }

        private string GetNumberScale(string dbType, string numberLength)
        {
            if (dbType.Equals("OracleDbType.Decimal", StringComparison.InvariantCultureIgnoreCase))
                return numberLength;
            return "0";
        }

        private string GetOutputParameterForInsert(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "{0}.{1} = {2}.Parse(sqlCommand.Parameters[\"{3}\"].Value.ToString());";
            foreach (DatabaseColumn column in table.Columns.OrderBy(x => x.Name))
            {
                if (!column.IsAutoNumber) continue;
                strToReturn.AppendLine(string.Format(format, "businessObject", column.PropertyName, column.CSharpDataTypeName, column.SqlParameterName));

            }

            return strToReturn.ToString();

        }

        private string GetOutputParameterForInsertCheck(DatabaseTable table)
        {
            var isAuto = table.Columns.Any(x => x.IsAutoNumber);
            return isAuto ? "" : "return baseEntity;";
        }

        private string GetPopulateObjectFromReader(DatabaseTable table)
        {
            if (!OracleHelper.IsConectOracle)
                return GetPopulateObjectFromReaderSql(table); //Neu la sql
            StringBuilder strToReturn = new StringBuilder();
            string className = table.ClassName;
            string formatForNUll = "\t\t\t\tif (!dataReader.IsDBNull(GetIndex({0}.{0}Fields.{1}.ToString())))";
            string assignFormat = "\t\t\t\tbusinessObject.{0} = dataReader.Get{1}(GetIndex({2}.{2}Fields.{3}.ToString()));";
            string assignFormatBoolean = "\t\t\t\t\tbusinessObject.{0} = tmp{1} != 0;";

            string assignSpecialFormat = "\t\t\t\tbusinessObject.{0} = ({1})dataReader.Get{2}(GetIndex({3}.{3}Fields.{4}.ToString()));";

            List<string> listBool = null;
            if (OracleHelper.DicBoolFields != null)
            {
                if (OracleHelper.DicBoolFields.ContainsKey(table.TableName))
                    listBool = OracleHelper.DicBoolFields[table.TableName];
            }

            var isMultilKeys = IsMultiKeys(table.Columns);
            foreach (DatabaseColumn column in table.Columns.OrderBy(x => x.Name))
            {
                strToReturn.AppendLine("");

                if (column.IsNull)
                {

                    strToReturn.AppendLine(string.Format(formatForNUll, className, column.PropertyName));

                    strToReturn.AppendLine("\t\t\t\t{");
                    strToReturn.Append("\t");
                }

                string srlType = column.DotNetDataTypeName;
                if (srlType == "Byte[]" || srlType == "Object" || srlType == "Int16")
                {
                    if ((listBool != null && listBool.Contains(column.Name) || column.CSharpDataTypeName.Equals("bool", StringComparison.InvariantCultureIgnoreCase)) && OracleHelper.IsConectOracle)
                    {
                        var assignFormatTmp = "\t\t\t\tvar tmp{1} = dataReader.GetInt16(GetIndex({0}.{0}Fields.{1}.ToString()));";
                        assignFormatTmp = string.Format(assignFormatTmp, className, column.PropertyName);
                        strToReturn.AppendLine(assignFormatTmp);
                        if (isMultilKeys && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys)
                        {
                            var boolValue = string.Format(assignFormatBoolean, table.TableName + "Keys." + column.PropertyName, column.PropertyName);
                            strToReturn.AppendLine(boolValue);
                        }
                        else
                        {
                            var boolValue = string.Format(assignFormatBoolean, column.PropertyName, column.PropertyName);
                            strToReturn.AppendLine(boolValue);
                        }
                        if (column.IsNull)
                        {
                            strToReturn.AppendLine("\t\t\t\t}");
                        }
                        continue;
                    }

                    string convertType = (srlType == "Int16") ? column.CSharpDataTypeName : "System." + srlType;
                    string sType = (srlType == "Int16") ? "Int16" : "Value";
                    if (isMultilKeys && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys)
                    {
                        strToReturn.AppendLine(string.Format(assignSpecialFormat, table.TableName + "Keys." + column.PropertyName, convertType, sType, className, column.PropertyName));
                    }
                    else
                    {
                        strToReturn.AppendLine(string.Format(assignSpecialFormat, column.PropertyName, convertType, sType, className, column.PropertyName));
                    }
                }
                else
                {
                    if (srlType == "Boolean")
                    {
                        var assignFormatTmp = "\t\t\t\tvar tmp{1} = dataReader.GetInt16(GetIndex({0}.{0}Fields.{1}.ToString()));";
                        assignFormatTmp = string.Format(assignFormatTmp, className, column.PropertyName);
                        strToReturn.AppendLine(assignFormatTmp);
                        if (isMultilKeys && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys)
                        {
                            var boolValue = string.Format(assignFormatBoolean, table.TableName + "Keys." + column.PropertyName, column.PropertyName);
                            strToReturn.AppendLine(boolValue);
                        }
                        else
                        {
                            var boolValue = string.Format(assignFormatBoolean, column.PropertyName, column.PropertyName);
                            strToReturn.AppendLine(boolValue);
                        }
                    }
                    else
                    {
                        if (listBool != null && listBool.Contains(column.Name) && OracleHelper.IsConectOracle)
                        {
                            var assignFormatTmp = "\t\t\t\tvar tmp{1} = dataReader.GetInt16(GetIndex({0}.{0}Fields.{1}.ToString()));";
                            assignFormatTmp = string.Format(assignFormatTmp, className, column.PropertyName);
                            strToReturn.AppendLine(assignFormatTmp);
                            if (isMultilKeys && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys)
                            {
                                var boolValue = string.Format(assignFormatBoolean, table.TableName + "Keys." + column.PropertyName, column.PropertyName);
                                strToReturn.AppendLine(boolValue);
                            }
                            else
                            {
                                var boolValue = string.Format(assignFormatBoolean, column.PropertyName, column.PropertyName);
                                strToReturn.AppendLine(boolValue);
                            }
                            if (column.IsNull)
                            {
                                strToReturn.AppendLine("\t\t\t\t}");
                            }
                            continue;
                        }
                        if (srlType == "Single")
                            srlType = "Float";
                        if (isMultilKeys && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys)
                        {
                            strToReturn.AppendLine(string.Format(assignFormat, table.TableName + "Keys." + column.PropertyName, srlType, className, column.PropertyName));
                        }
                        else
                        {
                            strToReturn.AppendLine(string.Format(assignFormat, column.PropertyName, srlType, className, column.PropertyName));
                        }
                    }
                }

                if (column.IsNull)
                {
                    strToReturn.AppendLine("\t\t\t\t}");
                }

            }

            return strToReturn.ToString();
        }

        private string GetPopulateObjectFromReaderSql(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();
            string className = table.ClassName;
            string formatCheckIndex = "\t\t\tif (GetIndex({0}.{0}Fields.{1}.ToString()) != -1)";
            string formatForNUll = "\t\t\t\tif (!dataReader.IsDBNull(GetIndex({0}.{0}Fields.{1}.ToString())))";
            string assignFormat = "\t\t\t\tbusinessObject.{0} = dataReader.Get{1}(GetIndex({2}.{2}Fields.{0}.ToString()));";
            string assignSpecialFormat = "\t\t\t\tbusinessObject.{0} = ({1})dataReader.Get{2}(GetIndex({3}.{3}Fields.{0}.ToString()));";

            foreach (DatabaseColumn column in table.Columns)
            {
                strToReturn.AppendLine("");

                if(!column.IsPK)
                    strToReturn.AppendLine(string.Format(formatCheckIndex, className, column.PropertyName));
                if (column.IsNull)
                {
                    strToReturn.AppendLine(string.Format(formatForNUll, className, column.PropertyName));
                    strToReturn.AppendLine("\t\t\t\t{");
                    strToReturn.Append("\t");
                }

                string srlType = column.DotNetDataTypeName;
                if (srlType == "Byte[]" || srlType == "Object" || srlType == "Int16")
                {
                    string convertType = (srlType == "Int16") ? column.CSharpDataTypeName : "System." + srlType;
                    string sType = (srlType == "Int16") ? "Int16" : "Value";
                    strToReturn.AppendLine(string.Format(assignSpecialFormat, column.PropertyName, convertType, sType, className));
                }
                else
                {
                    if (srlType == "Single")
                        srlType = "Float";

                    strToReturn.AppendLine(string.Format(assignFormat, column.PropertyName, srlType, className));
                }

                if (column.IsNull)
                {
                    strToReturn.AppendLine("\t\t\t\t}");
                }

            }

            return strToReturn.ToString();
        }

        private string GetOracleDbType(string sqlType, string nguyen, string thaphan, long size)
        {
            OracleDbType dbType;
            int phanNguyen;
            int.TryParse(nguyen, out phanNguyen);
            int thapPhan;
            int.TryParse(thaphan, out thapPhan);

            #region SWTICH CASE

            switch (sqlType.ToLower())
            {
                case "bigint":
                    {
                        dbType = OracleDbType.Int64;
                        break;
                    }
                case "binary":
                    {
                        dbType = OracleDbType.Raw;
                        break;
                    }
                case "bit":
                    {
                        dbType = OracleDbType.Int16;
                        break;
                    }
                case "char":
                    {
                        dbType = OracleDbType.Char;
                        break;
                    }
                case "smalldatetime":
                case "date":
                case "datetime":
                    {
                        dbType = OracleDbType.Date;
                        break;
                    }
                case "decimal":
                    {
                        dbType = OracleDbType.Decimal;
                        break;
                    }
                case "float":
                    {
                        dbType = OracleDbType.Single;
                        break;
                    }
                case "image":
                    {
                        dbType = OracleDbType.LongRaw;
                        break;
                    }
                case "int":
                    {
                        dbType = OracleDbType.Int32;
                        break;
                    }
                case "money":
                    {
                        dbType = OracleDbType.Int64;
                        break;
                    }
                case "nchar":
                    {
                        dbType = OracleDbType.NChar;
                        break;
                    }
                case "clob":
                case "ntext":
                case "text":
                    {
                        dbType = OracleDbType.Clob;
                        break;
                    }
                case "numeric":
                    {
                        dbType = OracleDbType.Decimal;
                        break;
                    }
                case "nvarchar2":
                case "nvarchar":
                    {
                        if (size >= 2147483647)
                            dbType = OracleDbType.Clob;
                        else
                            dbType = OracleDbType.NVarchar2;
                        break;
                    }
                case "real":
                    {
                        dbType = OracleDbType.Single;
                        break;
                    }
                case "smallint":
                    {
                        dbType = OracleDbType.Int16;
                        break;
                    }
                case "smallmoney":
                    {
                        dbType = OracleDbType.Int32;
                        break;
                    }
                case "timestamp":
                    {
                        dbType = OracleDbType.TimeStamp;
                        break;
                    }
                case "tinyint":
                    {
                        dbType = OracleDbType.Int16;
                        break;
                    }
                case "varchar":
                    {
                        dbType = OracleDbType.Varchar2;
                        break;
                    }
                case "xml":
                    {
                        dbType = OracleDbType.XmlType;
                        break;
                    }
                case "varchar2":
                    {
                        dbType = OracleDbType.Varchar2;
                        break;
                    }
                case "number":
                    {
                        if (thapPhan == 0)
                        {
                            if (phanNguyen <= 5)
                                dbType = OracleDbType.Int16;
                            else if (phanNguyen > 5 && phanNguyen <= 10)
                                dbType = OracleDbType.Int32;
                            else
                                dbType = OracleDbType.Int64;
                        }
                        else
                            dbType = OracleDbType.Decimal;
                        break;
                    }
                case "varbinary":
                case "long raw":
                    dbType = OracleDbType.LongRaw;
                    break;
                case "raw":
                    dbType = OracleDbType.Raw;
                    break;
                case "blob":
                    dbType = OracleDbType.Blob;
                    break;
                default:
                    {
                        throw new Exception("Khong lay duoc type");
                    }
            }

            #endregion

            return "OracleDbType." + dbType.ToString();
        }

        private string GetSqlDbType(string sqlType)
        {
            SqlDbType dbType = SqlDbType.Text;

            #region SWTICH CASE

            switch (sqlType.ToLower())
            {
                case "bigint":
                    {
                        dbType = SqlDbType.BigInt;
                        break;
                    }
                case "binary":
                    {
                        dbType = SqlDbType.Binary;
                        break;
                    }
                case "bit":
                    {
                        dbType = SqlDbType.Bit;
                        break;
                    }
                case "char":
                    {
                        dbType = SqlDbType.Char;
                        break;
                    }
                case "datetime":
                    {
                        dbType = SqlDbType.DateTime;
                        break;
                    }
                case "decimal":
                    {
                        dbType = SqlDbType.Decimal;
                        break;
                    }
                case "float":
                    {
                        dbType = SqlDbType.Float;
                        break;
                    }
                case "image":
                    {
                        dbType = SqlDbType.Image;
                        break;
                    }
                case "int":
                    {
                        dbType = SqlDbType.Int;
                        break;
                    }
                case "money":
                    {
                        dbType = SqlDbType.Money;
                        break;
                    }
                case "nchar":
                    {
                        dbType = SqlDbType.NChar;
                        break;
                    }
                case "ntext":
                    {
                        dbType = SqlDbType.NText;
                        break;
                    }
                case "numeric":
                    {
                        dbType = SqlDbType.Decimal;
                        break;
                    }
                case "nvarchar":
                    {
                        dbType = SqlDbType.NVarChar;
                        break;
                    }
                case "real":
                    {
                        dbType = SqlDbType.Real;
                        break;
                    }
                case "smalldatetime":
                    {
                        dbType = SqlDbType.SmallDateTime;
                        break;
                    }
                case "smallint":
                    {
                        dbType = SqlDbType.SmallInt;
                        break;
                    }
                case "smallmoney":
                    {
                        dbType = SqlDbType.SmallMoney;
                        break;
                    }
                case "sql_variant":
                    {
                        dbType = SqlDbType.Variant;
                        break;
                    }
                case "text":
                    {
                        dbType = SqlDbType.Text;
                        break;
                    }
                case "timestamp":
                    {
                        dbType = SqlDbType.Timestamp;
                        break;
                    }
                case "tinyint":
                    {
                        dbType = SqlDbType.TinyInt;
                        break;
                    }
                case "uniqueidentifier":
                    {
                        dbType = SqlDbType.UniqueIdentifier;
                        break;
                    }
                case "varbinary":
                    {
                        dbType = SqlDbType.VarBinary;
                        break;
                    }
                case "varchar":
                    {
                        dbType = SqlDbType.VarChar;
                        break;
                    }
                case "xml":
                    {
                        dbType = SqlDbType.Xml;
                        break;
                    }
            }

            #endregion


            if (MySqlGenerator.IsConnectMySql)
            {
                //Neu la MySql
                if (dbType == SqlDbType.BigInt)
                    return "MySqlDbType.Int64";
                else if (dbType == SqlDbType.Int)
                    return "MySqlDbType.Int32";
                else
                {
                    return "MySqlDbType." + dbType.ToString();
                }
            }
            //Neu la Sql
            return "SqlDbType." + dbType.ToString();

        }

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonicationMemory.Common;
using MySql.Data.MySqlClient;

namespace CommonicationMemory.DatabaseSchema
{
    public class MySqlGenerator
    {
        private static string _connectionString = string.Empty;
        public static bool IsConnectMySql = false;

        #region MYSQL
        public static Database GetDataBaseSchemaMySql(string databaseServer, string catalog, string connectionString)
        {
            IsConnectMySql = true;
            _connectionString = connectionString;
            //_connectionString = _connectionString.Replace("CharSet=utf8;", "");
            Database database = new Database();
            database.ConnectionString = connectionString;
            database.DatabaseServer = databaseServer;
            database.Catalog = catalog;

            // Populate tables in it.
            PopulateTablesMySql(database, catalog);

            return database;
        }

        private static void PopulateTablesMySql(Database database, string cataLog)
        {
            MySqlConnection sqlConnection = new MySqlConnection(_connectionString);
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            var sqlString =
                "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES ";
            sqlString += " WHERE TABLE_SCHEMA = '" + cataLog + "' ";
            sqlString += " Order by TABLE_NAME ";

            sqlCommand.CommandText = sqlString;


            try
            {
                sqlConnection.Open();
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                var dic = TierGeneratorSettings.Instance.DicEntites;
                while (reader.Read())
                {
                    var queryCatalog = reader.GetString(1);
                    if (queryCatalog.ToLower() != cataLog.ToLower())
                        continue;
                    DatabaseTable databaseTable = new DatabaseTable();
                    databaseTable.DatabaseName = database.Catalog; //Luu ten cua Db de phan tach cau lenh sql
                    databaseTable.IsSelected = false;
                    databaseTable.TableSchema = reader.GetString(1);
                    databaseTable.TableName = reader.GetString(2);

                    //if (!databaseTable.TableName.ToLower().StartsWith("account"))
                    //    continue; //Test

                    databaseTable.TableType = reader.GetString(3);
                    if (dic.ContainsKey(databaseTable.TableName))
                    {
                        databaseTable.IsSelected = true;
                    }

                    //Chuyen ten table name cho giong voi entity
                    foreach (var entityName in dic.Keys)
                    {
                        if (entityName.ToLower() == databaseTable.TableName.ToLower())
                        {
                            databaseTable.TableName = entityName;
                            databaseTable.IsSelected = true;
                            break;
                        }
                    }

                    // Populate Column
                    PopulateTableColumnsMySql(databaseTable);


                    // Add into the list
                    if (database.Tables == null)
                        database.Tables = new List<DatabaseTable>();
                    database.Tables.Add(databaseTable);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

        }

        private static void PopulateTableColumnsMySql(DatabaseTable databaseTable)
        {

            MySqlConnection sqlConnection = new MySqlConnection(_connectionString);
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            //sqlCommand.CommandText = "SELECT * FROM " + databaseTable.TableName + " LIMIT 0";
            //sqlCommand.CommandText = "DESCRIBE " + databaseTable.TableName;
            //sqlCommand.CommandText = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" +
            //                         databaseTable.TableName + "'";
            sqlCommand.CommandText =
                "SELECT table_schema, table_name, column_name, column_key, column_type, ordinal_position," +
                " is_nullable, data_type, numeric_precision, numeric_scale, extra, column_default " +
                " FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + databaseTable.TableName + "' " +
                "  and Table_schema = '" + databaseTable.TableSchema + "'";
            //nghien cuu chuyen sang cau lenh sau
            //SELECT* FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'alertemail'

            try
            {
                //DataTable dtKeys = GetPrimaryKeyAndForeignKeyMySql(databaseTable.TableName);

                sqlConnection.Open();
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();

                var listTmp = new List<DatabaseColumn>();
                while (sqlReader.Read())
                {
                    DatabaseColumn databaseColumn = new DatabaseColumn();
                    try
                    {
                        var tableName = sqlReader.GetString("table_name");
                        if (tableName.ToLower() != databaseTable.TableName.ToLower())
                            continue;
                        //Moi 1 line la 1 colums
                        var columnName = sqlReader.GetString("column_name");
                        bool isSelect;
                        databaseColumn.Name = GetNameColumn(tableName, columnName, out isSelect);
                        databaseTable.IsSelected = isSelect;
                        if (columnName.ToLower() == "ispendingchanged" ||
                            columnName.ToLower() == "timechanged")
                        {

                        }
                        if (columnName.ToLower() == "note")
                        {

                        }
                        databaseColumn.DataType = sqlReader.GetString("data_type");
                        #region Dieu chinh datatype cho phu hop voi C#

                        if (databaseColumn.DataType == "tinyint")
                            databaseColumn.DataType = "bit";
                        #endregion

                        var isNullStr = sqlReader.GetString("is_nullable");
                        bool isNull = false;
                        if (isNullStr == "YES")
                            isNull = true;
                        string keyStr = sqlReader.GetString("column_key");
                        bool isPriKey = false;
                        if (!string.IsNullOrEmpty(keyStr))
                        {
                            if (keyStr.ToLower() == "pri") isPriKey = true;
                        }
                        string defaultStr = "";
                        if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("column_default"))) //Cot Default
                            defaultStr = sqlReader.GetString("column_default");
                        string exTra = sqlReader.GetString("extra");
                        bool isIndentity = false;
                        if (exTra == "auto_increment")
                            isIndentity = true;

                        databaseColumn.IsNull = isNull;
                        databaseColumn.CSharpDataTypeName = GetCSharpTypeNameMySql(databaseColumn.DataType, isNull);
                        databaseColumn.DotNetDataTypeName = GetDotNetTypeNameMySql(databaseColumn.DataType);
                        string num_prec = "";
                        if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("numeric_precision")))
                            num_prec = sqlReader.GetString("numeric_precision");
                        databaseColumn.NumericPrecision = num_prec;

                        string num_scale = "";
                        if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("numeric_scale")))
                            num_scale = sqlReader.GetString("numeric_scale");
                        databaseColumn.NumericScale = num_scale;

                        string maxLengStr = "";
                        //if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("character_maximum_length")))
                        //    maxLengStr = sqlReader.GetString("character_maximum_length");
                        //Do ko lay duoc thong tin cot character_maximum_length --> Lay thu cong bang chat chuoi

                        var column_type = sqlReader.GetString("column_type");
                        databaseColumn.ColumnSize = GetSizeMySql(column_type,
                            databaseColumn.NumericPrecision, databaseColumn.NumericScale, maxLengStr);
                        databaseColumn.OrdinalPosition = sqlReader.GetInt32("ordinal_position");

                        if (isIndentity)
                        {
                            databaseColumn.IsAutoNumber = true;
                        }
                        if (isPriKey)
                            databaseColumn.IsPK = true;
                        databaseColumn.IsFK = false;
                        databaseColumn.IsReadonly = false;

                        // Add into the list
                        listTmp.Add(databaseColumn);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                databaseTable.Columns = listTmp.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }


        }

        private static string GetNameColumn(string tableName, string columnName, out bool isSelect)
        {
            isSelect = false;
            //Do MySql ten bang chu thuong --> Doc entity hien tai config de chuyen thanh ten nhu entity
            foreach (var keyValue in TierGeneratorSettings.Instance.DicEntites)
            {
                if (keyValue.Key.ToLower() == tableName.ToLower())
                {
                    isSelect = true;
                    //Neu la bang can tim --> tim column
                    foreach (var keyValue2 in keyValue.Value)
                    {
                        if (columnName.ToLower() == keyValue2.Key.ToLower())
                            return keyValue2.Key;
                    }
                }
            }

            return columnName;
        }

        public static long? GetSizeMySql(string columnType, string numericPrecision, string numericScale, string maxLengthStr)
        {
            if (columnType.StartsWith("int"))
                return 4; //giong nhu sql hien tai
            if (columnType.StartsWith("bigint"))
                return 8; //giong nhu sql hien tai
            if (columnType.StartsWith("decimal"))
                return 17; //giong nhu sql hien tai
            if (columnType.StartsWith("text") || columnType.StartsWith("ntext") || columnType.StartsWith("longtext"))
                return 1073741823;
            if (columnType.StartsWith("datetime"))
                return 8; //giong nhu sql hien tai
            if (columnType.StartsWith("bit") || columnType.StartsWith("tinyint"))
                return 1; //giong nhu sql hien tai

            if (string.IsNullOrEmpty(numericPrecision) && string.IsNullOrEmpty(numericScale))
            {
                if (string.IsNullOrEmpty(maxLengthStr))
                {
                    //varchar(50) lay ra 50
                    //decimal(28,6) lay ra 28
                    var lengType = columnType;
                    if (columnType.StartsWith("varchar")) lengType = lengType.Replace("varchar", "");
                    if (columnType.StartsWith("nvarchar")) lengType = lengType.Replace("nvarchar", "");

                    lengType = lengType.Replace("(", "");
                    lengType = lengType.Replace(")", "");
                    var split = lengType.Trim().Split(",".ToCharArray());
                    long maxLengthTemp;
                    long.TryParse(split[0], out maxLengthTemp);
                    return maxLengthTemp;
                }

                long maxLength;
                long.TryParse(maxLengthStr, out maxLength);
                return maxLength;
            }

            long numPrec;
            long.TryParse(numericPrecision, out numPrec);
            long numScale;
            long.TryParse(numericScale, out numScale);

            return numPrec;
        }
        public static string GetCSharpTypeNameMySql(string csTypeName, bool isNull)
        {
            string typeName = csTypeName;

            if (csTypeName.StartsWith("varchar") ||
                csTypeName.StartsWith("nvarchar") ||
                csTypeName.StartsWith("longtext"))
            {
                typeName = "string";
            }
            else if (csTypeName.StartsWith("decimal"))
            {
                typeName = "decimal";
            }
            else if (csTypeName.StartsWith("bigint"))
            {
                typeName = "long";
            }
            else if (csTypeName.StartsWith("int"))
            {
                typeName = "int";
            }
            else if (csTypeName.StartsWith("datetime"))
            {
                typeName = "DateTime";
            }
            else if (csTypeName.StartsWith("tinyint") || csTypeName.StartsWith("bit"))
            {
                typeName = "bool";
            }

            else if (Type.GetType(csTypeName) == typeof(byte[]) || csTypeName.StartsWith("longblob"))
            {
                typeName = "byte[]";
            }
            else
            {
                Type type = Type.GetType(csTypeName);
                typeName = type.Name;
            }

            // Check for Null
            if (isNull && typeName != "string" && typeName != "byte[]" && typeName != "object")
            {
                typeName += "?";
            }

            if (string.IsNullOrWhiteSpace(typeName))
                throw new Exception("Loi doi voi type " + csTypeName);
            return typeName;
        }
        public static string GetDotNetTypeNameMySql(string csTypeName)
        {
            string typeName = csTypeName;
            if (csTypeName.StartsWith("varchar") ||
                csTypeName.StartsWith("nvarchar") ||
                csTypeName.StartsWith("ntext") ||
                csTypeName.StartsWith("longtext"))
            {
                typeName = "String";
            }
            else if (csTypeName.StartsWith("decimal"))
            {
                typeName = "Decimal";
            }
            else if (csTypeName.StartsWith("bigint"))
            {
                typeName = "Int64";
            }
            else if (csTypeName.StartsWith("int"))
            {
                typeName = "Int32";
            }
            else if (csTypeName.StartsWith("datetime"))
            {
                typeName = "DateTime";
            }
            else if (csTypeName.StartsWith("tinyint") || csTypeName.StartsWith("bit"))
            {
                typeName = "Boolean";
            }
            else if (csTypeName.StartsWith("longblob"))
            {
                typeName = "Byte[]";
            }

            return typeName;
        }
        #endregion
    }
}

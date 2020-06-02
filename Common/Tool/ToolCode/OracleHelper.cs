namespace CommonicationMemory
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using CommonicationMemory.Common;
    using Oracle.ManagedDataAccess.Client;

    public class OracleHelper
    {
        public static bool IsConectOracle;
        public static string ConnectionString;
        public static Dictionary<string, List<string>> DicAutoKey;
        public static Dictionary<string, List<string>> DicBoolFields;

        private OracleHelper() { }

        public static DataTable GetDt(string sql)
        {
            using (var cn = new OracleConnection(ConnectionString))
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetTableColumns(string tableName)
        {
            string s = string.Format("select user_tab_columns.column_name, user_tab_columns.data_type, user_tab_columns.char_length, user_tab_columns.nullable,(select user_cons_columns.column_name from user_constraints inner join user_cons_columns on user_constraints.constraint_name=user_cons_columns.constraint_name where user_constraints.table_name=user_tab_columns.table_name AND column_name=user_tab_columns.column_name AND constraint_type='P') as PK from user_tab_columns WHERE table_name='{0}'", tableName);
            var dt = GetDt(s);
            dt.Columns.Add("IS_SELECTED", typeof(bool));
            dt.Columns["IS_SELECTED"].DefaultValue = true;

            return dt;
        }

        public static Dictionary<string, List<DatabaseColumn>> GetAllTableColumns()
        {
            string s = string.Format("select user_tab_columns.DATA_SCALE, user_tab_columns.DATA_PRECISION, user_tab_columns.data_length, user_tab_columns.column_name, user_tab_columns.data_type, user_tab_columns.char_length, user_tab_columns.nullable,(select user_cons_columns.column_name from user_constraints inner join user_cons_columns on user_constraints.constraint_name=user_cons_columns.constraint_name where user_constraints.table_name=user_tab_columns.table_name AND column_name=user_tab_columns.column_name AND constraint_type='P') as PK, table_name from user_tab_columns");
            var dic = new Dictionary<string, List<DatabaseColumn>>();
            DataTable dt = GetDt(s);
            dt.Columns.Add("IS_SELECTED", typeof(bool));
            dt.Columns["IS_SELECTED"].DefaultValue = true;
            var dicFields = TierGeneratorSettings.Instance.DicEntites;

            foreach (DataRow col in dt.Rows)
            {
                long lengthSize;
                var columnName = col["COLUMN_NAME"].ToString();
                long.TryParse(col["CHAR_LENGTH"].ToString(), out lengthSize);
                if (lengthSize <= 0)
                    long.TryParse(col["data_length"].ToString(), out lengthSize);

                var nguyen = col["DATA_PRECISION"].ToString();
                var thapPhan = col["DATA_SCALE"].ToString();
                var tableName = col["table_name"].ToString();
                foreach (var dicField in dicFields)
                {
                    if (dicField.Key.Equals(tableName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        tableName = dicField.Key;
                        var dicTmp = dicField.Value;
                        foreach (var fields in dicTmp)
                        {
                            if (!fields.Key.Equals(columnName, StringComparison.InvariantCultureIgnoreCase)) continue;
                            columnName = fields.Key;
                            break;
                        }
                    }
                }
                var isAutoKey = false;
                if (DicAutoKey.ContainsKey(tableName.Trim()))
                {
                    var list = DicAutoKey[tableName.Trim()];
                    if (list.Any(x => x.Equals(columnName.Trim())))
                        isAutoKey = true;
                }
                var isNull = col["NULLABLE"].ToString() != "N";
                var dotNetType = ConvertDotNeTypeName(col["DATA_TYPE"].ToString(), nguyen, thapPhan);
                var cSharpType = ConvertCSharpTypeName(dotNetType, nguyen, thapPhan);
                var sqlType = ConvertMsSqlTypeName(col["DATA_TYPE"].ToString());
                var databaseColumn = new DatabaseColumn
                                         {
                                             IsPK = col["PK"].ToString().Length > 0,
                                             ColumnSize = GetLength(lengthSize, sqlType),
                                             DataType = sqlType,
                                             IsNull = isNull,
                                             Name = columnName,
                                             NumericPrecision = nguyen,
                                             NumericScale = thapPhan,
                                             DotNetDataTypeName = dotNetType,
                                             CSharpDataTypeName = cSharpType,
                                             IsAutoNumber = isAutoKey,
                                         };
                if (dic.ContainsKey(tableName))
                    dic[tableName].Add(databaseColumn);
                else
                    dic[tableName] = new List<DatabaseColumn> { databaseColumn };
            }
            return dic;
        }

        private static long? GetLength(long lengthSize, string sqlType)
        {
            if (sqlType.Equals("LONG RAW", StringComparison.InvariantCultureIgnoreCase) ||
                sqlType.Equals("RAW", StringComparison.InvariantCultureIgnoreCase))
                return 32760;
            if (sqlType.Equals("CLOB", StringComparison.InvariantCultureIgnoreCase)||
                sqlType.Equals("BLOB", StringComparison.InvariantCultureIgnoreCase))
                return 2147483647;
            return lengthSize;
        }

        private static string ConvertCSharpTypeName(string csTypeName, string nguyen, string thaphan)
        {
            var typeName = csTypeName;
            int phanNguyen;
            int.TryParse(nguyen, out phanNguyen);
            int thapPhan;
            int.TryParse(thaphan, out thapPhan);

            switch (typeName)
            {
                case "Int32":
                case "BIT":
                case "TINYINT":
                case "SMALLINT":
                case "INTEGER":
                case "INT":
                    return "int";
                case "Int16":
                    {
                        if (phanNguyen <= 3)
                            return "bool";
                        return "int";
                    }
                case "BIGINT":
                case "Int64":
                    return "long";
                case "Decimal":
                case "SMALLMONEY":
                case "MONEY":
                case "DECIMAL":
                case "NUMBER":
                case "NUMERIC":
                case "decimal":
                    return "decimal";
                case "REAL":
                case "FLOAT":
                    return "Float";
                case "SMALLDATETIME":
                case "DATETIME":
                case "DATE":
                case "DateTime":
                    return "DateTime";
                case "TIMESTAMP":
                    return "RAW";
                case "DATETIMEOFFSET":
                    return "TIMESTAMP";
                case "VARCHAR2":
                case "NVARCHAR2":
                case "CHAR":
                case "NCHAR":
                case "NVARCHAR":
                case "VARCHAR":
                case "VARCHAR(MAX)":
                case "TEXT":
                case "NTEXT":
                case "CLOB":
                case "String":
                case "string":
                    return "string";
                case "IMAGE":
                case "BLOB":
                case "VARBINARY":
                case "BINARY":
                case "RAW":
                case "LONG RAW":
                case "Byte[]":
                case "byte[]":
                    return "byte[]";
                case "UNIQUEIDENTIFIER":
                    return "RAW(16)";
                default:
                    return "long";
            }
        }

        private static string ConvertMsSqlTypeName(string typeName)
        {
            switch (typeName.ToUpper())
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
                    return "NCHAR";
                case "VARCHAR":
                    return "VARCHAR2";
                case "VARCHAR(MAX)":
                    return "CLOB";
                case "TEXT":
                    return "CLOB";
                case "NTEXT":
                    return "CLOB";
                case "BINARY":
                    return "RAW";
                case "VARBINARY":
                    return "RAW";
                case "IMAGE":
                    return "LONG RAW";
                case "BIT":
                    return "NUMBER(3)";
                case "UNIQUEIDENTIFIER":
                    return "RAW(16)";
                default:
                    return typeName;
            }
        }

        private static string ConvertDotNeTypeName(string typeName, string nguyen, string thaphan)
        {
            int phanNguyen;
            int.TryParse(nguyen, out phanNguyen);
            int thapPhan;
            int.TryParse(thaphan, out thapPhan);

            switch (typeName.ToUpper())
            {
                case "BIT":
                case "TINYINT":
                case "SMALLINT":
                    return "Int16";
                case "INTEGER":
                case "INT":
                    return "Int32";
                case "BIGINT":
                    return "Int64";
                case "SMALLMONEY":
                case "MONEY":
                case "DECIMAL":
                    return "Decimal";
                case "NUMBER":
                case "NUMERIC":
                    {
                        if (thapPhan == 0)
                        {
                            if (phanNguyen <= 5)
                                return "Int16";
                            if (phanNguyen > 5 && phanNguyen <= 10)
                                return "Int32";
                            return "Int64";
                        }
                        return "Decimal";
                    }
                case "REAL":
                case "FLOAT":
                    return "Float";
                case "SMALLDATETIME":
                case "DATETIME":
                case "DATE":
                    return "DateTime";
                case "TIMESTAMP":
                    return "RAW";
                case "DATETIMEOFFSET":
                    return "TIMESTAMP";
                case "VARCHAR2":
                case "NVARCHAR2":
                case "CHAR":
                case "NCHAR":
                case "NVARCHAR":
                case "VARCHAR":
                case "VARCHAR(MAX)":
                case "TEXT":
                case "NTEXT":
                case "CLOB":
                    return "String";
                case "IMAGE":
                case "BLOB":
                case "VARBINARY":
                case "BINARY":
                case "RAW":
                case "LONG RAW":
                    return "Byte[]";
                case "UNIQUEIDENTIFIER":
                    return "RAW(16)";
                default:
                    return typeName;
            }
        }
    }
}

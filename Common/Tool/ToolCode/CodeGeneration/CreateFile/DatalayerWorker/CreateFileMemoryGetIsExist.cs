using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker
{
    public class CreateFileMemoryGetIsExist
    {
        public void CreateFile(string folderPath, string fileName, List<DatabaseTable> listTable)
        {
            try
            {
                listTable.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));
                var stringBuild = new StringBuilder(); 
                var headerFile = new StringBuilder();
                headerFile.AppendLine(@"using System;
                                        using System.Collections.Generic;
                                        using System.Globalization;
                                        using System.Linq;
                                        using Anotar.NLog;
                                        using QuantEdge.Entity.Keys;
                                        using QuantEdge.Common.Enum;
                                        using QuantEdge.Entity.Entities;
                                        using QuantEdge.Lib.Broadcast;
                                        ");
                headerFile.AppendLine("namespace QuantEdge.Lib.Memory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");


                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("public partial class MemoryInfo : Memory");
                headerclassBuilder.AppendLine("{");
                var endclassBuilder = new StringBuilder();
                endclassBuilder.AppendLine("}");

                stringBuild.Append(headerFile.ToString());
                stringBuild.Append(headerclassBuilder.ToString());

                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        string tableName = table.TableName;

                        string keyName = "";
                        string keyValue = "";
                        int count = table.Columns.Count;
                        var listColums = new List<DatabaseColumn>();
                        for (int i = 0; i < count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsPK || column.IsFK)
                            {
                                listColums.Add(column);
                            }
                        }

                        if (listColums.Count == 1)
                        {
                            //TH có 1 key thì key là kiểu dữ liệu (string, long,..)
                            var column = listColums[0];
                            keyName = GetKeyName(column);
                            //Lay keyValue
                            keyValue = column.Name;
                        }
                        else if (listColums.Count > 1)
                        {
                            //Trường hợp lớn hơn 1 key  (GoldPositionMemberKeys)
                            keyName = tableName + "Keys";
                            keyValue = tableName + "Keys";
                        }

                        //chuyển chữ cái đầu của keyValue thành chữ thường
                        string preKey = keyValue.Substring(0, 1);
                        string nexKey = keyValue.Substring(1);

                        keyValue = preKey.ToLower() + nexKey;
                        stringBuild.AppendLine(FunctionBuild_GetMemory(tableName, keyName, keyValue).ToString());
                    }
                }

                //End insert func
                stringBuild.Append(endclassBuilder.ToString());
                stringBuild.Append(endFile.ToString());
                stringBuild.AppendLine();

                //Lưu vào thư mục build
                using (var sw = new StreamWriter(folderPath + fileName))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
                //Lưu vào thư mục foudation nếu có
                if (!string.IsNullOrEmpty(ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase) && ConfigGlobal.SettingConfig.Setting_CheckGenByForder)
                {
                    string fileCs = ConfigGlobal.SettingConfig.Setting_MemoryWorkerBase + "\\" + fileName;
                    if (File.Exists(fileCs))
                    {
                        //Tồn tại mới lưu
                        File.Delete(fileCs);
                        using (var sw = new StreamWriter(fileCs))
                        {
                            sw.WriteLine(stringBuild.ToString());
                            sw.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private StringBuilder FunctionBuild_GetMemory(string tableName, string keyName, string keyValue)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //public static bool IsExistAccountTransactionTemp(string accountTransactionTempId)
                //{
                //    if(DicAccountTransactionTemp.ContainsKey(accountTransactionTempId))
                //        return true;
                //    return false;
                //}

                functionBuild.AppendLine("public static bool IsExist" + tableName + "(" + keyName + " " + keyValue + ")");
                functionBuild.AppendLine("{");
                functionBuild.AppendLine("if (Dic" + tableName + ".ContainsKey(" + keyValue + "))");
                functionBuild.AppendLine("return true;");
                functionBuild.AppendLine("return false;");
                functionBuild.AppendLine("}");
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }

        private string GetKeyName(DatabaseColumn column)
        {
            if (OracleHelper.IsConectOracle)
                return column.CSharpDataTypeName;
            string keyName = "";
            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    {
                        keyName = "string";
                        break;
                    }
                case "bigint":
                    {
                        keyName = "long";
                        break;
                    }
                case "number":
                    {
                        keyName = "long";
                        break;
                    }
            }

            if (string.IsNullOrEmpty(keyName))
                keyName = column.DataType;

            if (keyName.Equals("datetime"))
                return "DateTime";
            return keyName.ToLower();
        }
    }
}

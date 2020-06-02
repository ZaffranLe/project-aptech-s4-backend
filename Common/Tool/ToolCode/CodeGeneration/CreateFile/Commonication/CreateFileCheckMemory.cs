using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.Commonication
{
    public class CreateFileCheckMemory
    {
        public void CreateFileCompare(StreamWriter sw, DatabaseTable table)
        {
            string tableName = table.TableName;
            sw.WriteLine();

            var headerFile = new StringBuilder();
            headerFile.AppendLine(@"using System;
                                    using System.Collections.Generic;
                                    using System.Linq;
                                    using Anotar.NLog;
                                    using ElectricShop.Entity;
                                    using ElectricShop.Common.Enum;
                                    using ElectricShop.Entity.Entities;
                                    using ElectricShop.Lib.Memory;
                                    using ElectricShop.Worker.CommunicationMemory.Utils;");
            headerFile.AppendLine("namespace ElectricShop.Worker.CommunicationMemory.ProcessCheck.FunctionCheckMemory");
            headerFile.AppendLine("{");
            var endFile = new StringBuilder();
            endFile.AppendLine("}");

            var headerclassBuilder = new StringBuilder();
            headerclassBuilder.AppendLine("public class CheckMemory" + tableName);
            headerclassBuilder.AppendLine("{");
            var endclassBuilder = new StringBuilder();
            endclassBuilder.AppendLine("}");


            sw.WriteLine(headerFile.ToString());
            sw.WriteLine(headerclassBuilder.ToString());
            //Insert func ở đây
            #region Lấy keyName
            string keyName = "";
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
                DatabaseColumn column = listColums[0];
                keyName = column.Name;
            }
            else if (listColums.Count > 1)
            {
                //Trường hợp lớn hơn 1 key  (GoldPositionMemberKeys)
                keyName = tableName + "Keys";
            }
            #endregion

            sw.WriteLine(FunctionBuild_CheckInfo(tableName, keyName));
            sw.WriteLine(FunctionBuild_CompareInfo(table));

            //End insert func
            sw.WriteLine(endclassBuilder.ToString());
            sw.WriteLine(endFile.ToString());
            sw.WriteLine();
        }

        private string GetTextFile(string fileName)
        {
            string textFunction = "";
            try
            {
                string fileFuncTemplate = GetAppPath() + "\\CodeGeneration\\FuncBuild\\" + fileName.Trim();
                if (File.Exists(fileFuncTemplate))
                {
                    //Nếu có template func xử lý thì insert luôn func
                    var streamReader = new StreamReader(fileFuncTemplate);
                    textFunction = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return textFunction;
        }

        private StringBuilder FunctionBuild_CheckInfo(string tableName, string keyName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                string textFunction = GetTextFile("CheckInfo.txt");
                textFunction = textFunction.Replace("TABLE_NAME", tableName);
                textFunction = textFunction.Replace("KEY_NAME", keyName);
                functionBuild.AppendLine(textFunction);
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }

        private StringBuilder FunctionBuild_CompareInfo(DatabaseTable table)
        {
            var functionBuild = new StringBuilder();
            try
            {
                string tableName = table.TableName;

                functionBuild.AppendLine("private static string CompareInfo(" + tableName +
                    " valueDatabase, " + tableName + " valueWorker, out string valueFail, out string keyValue)");
                functionBuild.AppendLine("{");
                functionBuild.AppendLine("valueFail = \"\";");
                functionBuild.AppendLine("keyValue = \"\";");

                int count = table.Columns.Count;
                var listColums = new List<DatabaseColumn>();
                var listColumsNotKey = new List<DatabaseColumn>();
                for (int i = 0; i < count; i++)
                {
                    DatabaseColumn column = table.Columns[i];
                    string columnName = column.Name;

                    if (column.IsPK || column.IsFK)
                    {
                        listColums.Add(column);
                    }
                    else
                    {
                        listColumsNotKey.Add(column);
                    }
                }

                if (tableName.Equals("UserRoleGroup"))
                {

                }

                //return string.Format("SymbolId and DatabaseValue ={0}. WorkerValue={1}", valueDatabase.SymbolId, valueWorker.SymbolId);
                if (listColums.Count == 1)
                {
                    string columnName = listColums[0].Name;
                    functionBuild.AppendLine("keyValue += \"" + columnName + " = \" + valueWorker." + columnName + "+\"__\";");
                    functionBuild.AppendLine("if (valueWorker." + columnName + " != valueDatabase." + columnName + ")");
                    functionBuild.AppendLine("{");
                    functionBuild.AppendLine("valueFail = string.Format(\"DatabaseValue ={0}. WorkerValue={1}\", valueDatabase." + columnName + ", valueWorker." + columnName + ");");
                    functionBuild.AppendLine("return \"" + columnName + "\";");
                    functionBuild.AppendLine("}");
                }
                else if (listColums.Count > 1)
                {
                    foreach (var column in listColums)
                    {
                        string columnName = column.Name;
                        string keyName = tableName + "Keys";
                        columnName = keyName + "." + columnName;

                        functionBuild.AppendLine("keyValue += \"" + columnName + " = \" + valueWorker." + columnName + "+\"__\";");
                        functionBuild.AppendLine("if (valueWorker." + columnName + " != valueDatabase." + columnName + ")");
                        functionBuild.AppendLine("{");
                        functionBuild.AppendLine("valueFail = string.Format(\"DatabaseValue ={0}. WorkerValue={1}\", valueDatabase." + columnName + ", valueWorker." + columnName + ");");
                        functionBuild.AppendLine("return \"" + columnName + "\";");
                        functionBuild.AppendLine("}");
                    }
                }

                foreach (var column in listColumsNotKey)
                {
                    if (column.CSharpDataTypeName == "byte[]" ||
                        column.CSharpDataTypeName == "byte[]?")
                    {
                        continue; //Bỏ qua check trường byte
                    }
                    if (column.Name.Equals("IndexRow") &&
                        column.IsAutoNumber)
                    {
                        continue; //Bỏ qua trường index row tự tăng của linkedtransaction
                    }

                    string columnName = column.Name;

                    //Nếu là trường giá hoặc decimal thì routing 6 kí tự
                    //if (column.CSharpDataTypeName == "decimal" ||
                    //    column.CSharpDataTypeName == "decimal?")
                    //{
                    //    if (CreateFileMemoryGetByField.IsNullValue(column, tableName))
                    //    {
                    //        //if (valueWorker.IMLimit.HasValue)
                    //        //valueWorker.IMLimit = Math.Round(valueWorker.IMLimit.Value, 6);
                    //        functionBuild.AppendLine("if (valueWorker." + columnName + ".HasValue)");
                    //        functionBuild.AppendLine("valueWorker." + columnName + " = Math.Round(valueWorker." + columnName + ".Value, " + column.NumericScale + ");");
                    //    }
                    //    else
                    //    {
                    //        //valueWorker.AvgBuyNet = Math.Round(valueWorker.AvgBuyNet, 6);
                    //        functionBuild.AppendLine("valueWorker." + columnName + " = Math.Round(valueWorker." + columnName + ", " + column.NumericScale + ");");
                    //    }
                    //}

                    if (column.CSharpDataTypeName == "DateTime" ||
                        column.CSharpDataTypeName == "DateTime?")
                    {
                        //Sử dụng hàm check time
                        //if (EntityCommandUtils.CheckTime(valueWorker.TimeChanged, valueDatabase.TimeChanged))
                        functionBuild.AppendLine("if (EntityCommandUtils.CheckTime(valueWorker." + columnName + ", valueDatabase." + columnName + "))");
                        functionBuild.AppendLine("{");
                        functionBuild.AppendLine("valueFail = string.Format(\"DatabaseValue ={0}. WorkerValue={1}\", valueDatabase." + columnName + ", valueWorker." + columnName + ");");
                        functionBuild.AppendLine("return \"" + columnName + "\";");
                        functionBuild.AppendLine("}");
                    }
                    else if (column.CSharpDataTypeName == "decimal" ||
                        column.CSharpDataTypeName == "decimal?")
                    {
                        //Sử dụng hàm check decimal
                        //if (EntityCommandUtils.CheckTime(valueWorker.TimeChanged, valueDatabase.TimeChanged))
                        functionBuild.AppendLine("if (EntityCommandUtils.CheckDecimal(valueWorker." + columnName + ", valueDatabase." + columnName + "))");
                        functionBuild.AppendLine("{");
                        functionBuild.AppendLine("valueFail = string.Format(\"DatabaseValue ={0}. WorkerValue={1}\", valueDatabase." + columnName + ", valueWorker." + columnName + ");");
                        functionBuild.AppendLine("return \"" + columnName + "\";");
                        functionBuild.AppendLine("}");
                    }
                    else
                    {
                        functionBuild.AppendLine("if (valueWorker." + columnName + " != valueDatabase." + columnName + ")");
                        functionBuild.AppendLine("{");
                        functionBuild.AppendLine("valueFail = string.Format(\"DatabaseValue ={0}. WorkerValue={1}\", valueDatabase." + columnName + ", valueWorker." + columnName + ");");
                        functionBuild.AppendLine("return \"" + columnName + "\";");
                        functionBuild.AppendLine("}");
                    }

                }

                functionBuild.AppendLine("return \"\";");
                functionBuild.AppendLine("}");
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }

        private static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.Commonication
{
    public class CreateFileDicMemory
    {
        public void CreateFile(StreamWriter sw, List<DatabaseTable> listTable)
        {
            try
            {
                listTable.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));
                sw.WriteLine();
                var headerFile = new StringBuilder();
                headerFile.AppendLine(@"using System.Collections.Generic;
                                        using QuantEdge.Entity.Entities;
                                        ");
                headerFile.AppendLine("namespace CommunicationMemory.Memory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");

                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("public class DicMemory");
                headerclassBuilder.AppendLine("{");
                var endclassBuilder = new StringBuilder();
                endclassBuilder.AppendLine("}");


                sw.WriteLine(headerFile.ToString());
                sw.WriteLine(headerclassBuilder.ToString());
                //Insert func ở đây

                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        string tableName = table.TableName;

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
                            keyName = GetKeyName(column);
                        }
                        else if (listColums.Count > 1)
                        {
                            //Trường hợp lớn hơn 1 key  (GoldPositionMemberKeys)
                            keyName = tableName + "Keys";
                        }

                        sw.WriteLine(FunctionBuild_DicMemory(tableName, keyName).ToString());
                    }
                }

                //End insert func
                sw.WriteLine(endclassBuilder.ToString());
                sw.WriteLine(endFile.ToString());
                sw.WriteLine();
            }
            catch (Exception ex)
            {

            }
        }

        private StringBuilder FunctionBuild_DicMemory(string tableName, string keyName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //Đối với table thường thì keyName là kiểu dữ liệu (string, long,..)
                //Đối với table nhiều khóa thì keyName là objectKey (GoldPositionMemberKeys)

                functionBuild.AppendLine("public static Dictionary<" + keyName + ", "
                    + tableName + "> Dic" + tableName +
                    " = new Dictionary<" + keyName + ", "
                    + tableName + ">();");
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
            }

            if (string.IsNullOrEmpty(keyName))
                keyName = column.DataType;

            if (keyName.Equals("datetime"))
                return "DateTime";
            return keyName.ToLower();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CommonicationMemory.Common;

namespace CommonicationMemory.CodeGeneration.CreateFile.Commonication
{
    public class CreateFileMemorySetRequest
    {
        public void CreateFile(StreamWriter sw, List<DatabaseTable> listTable)
        {
            try
            {
                sw.WriteLine();
                var headerFile = new StringBuilder();
                headerFile.AppendLine(@"using System;
                                        using System.Collections.Generic;
                                        using System.Linq;
                                        using CommunicationMemory.ReaderDatabase;
                                        using QuantEdge.Common.Enum;
                                        using QuantEdge.Entity;
                                        using QuantEdge.Entity.Entities;
                                        using QuantEdge.Lib.Memory;
                                        ");
                headerFile.AppendLine("namespace QuantEdge.Worker.CommunicationMemory.LoadMemory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");

                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("internal partial class MemorySet : Memory");
                headerclassBuilder.AppendLine("{");
                var endclassBuilder = new StringBuilder();
                endclassBuilder.AppendLine("}");


                sw.WriteLine(headerFile.ToString());
                sw.WriteLine(headerclassBuilder.ToString());
                //Insert func ở đây
                sw.WriteLine(GetFuncInitDatabaseByTime());

                #region Tạo hàm RegisterMemory
                var headerclassBuilderRegisterMemory = new StringBuilder();
                headerclassBuilderRegisterMemory.AppendLine("private static List<string> RegisterMemory()");
                headerclassBuilderRegisterMemory.AppendLine("{");
                var endclassBuilderRegisterMemory = new StringBuilder();
                endclassBuilderRegisterMemory.AppendLine("}");

                sw.WriteLine(headerclassBuilderRegisterMemory.ToString());
                sw.WriteLine("return new List<string>");
                sw.WriteLine("{");
                //Tạo các hàm SetMemory ở đây
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        sw.WriteLine(table.TableName + ".EntityName(),");
                    }
                }
                sw.WriteLine("};");
                sw.WriteLine(endclassBuilderRegisterMemory.ToString());
                #endregion

                #region Tạo hàm ClearMemory
                var headerclassBuilderClearMemory = new StringBuilder();
                headerclassBuilderClearMemory.AppendLine("private static void ClearMemory()");
                headerclassBuilderClearMemory.AppendLine("{");
                var endclassBuilderClearMemory = new StringBuilder();
                endclassBuilderClearMemory.AppendLine("}");

                sw.WriteLine(headerclassBuilderClearMemory.ToString());
                //Tạo các hàm SetMemory ở đây
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        sw.WriteLine("Dic" + table.TableName + ".Clear();");
                    }
                }
                sw.WriteLine(endclassBuilderClearMemory.ToString());
                #endregion

                #region Tạo hàm SetMemory --> UpdateAndInsertEntity
                var headerclassBuilderSet = new StringBuilder();
                headerclassBuilderSet.AppendLine("private static void UpdateAndInsertEntity(BaseEntity entity)");
                headerclassBuilderSet.AppendLine("{");
                var endclassBuilderSet = new StringBuilder();
                endclassBuilderSet.AppendLine("}");

                sw.WriteLine(headerclassBuilderSet.ToString());

                //Tạo các hàm SetMemory ở đây
                sw.WriteLine("#region Bảng sinh key bằng tay");
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        if (table.TableName == "MarginManualStatus")
                        {

                        }
                        bool isAutoMemberAndIdentity = false;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsAutoNumber && column.IsIdentity)
                            {
                                isAutoMemberAndIdentity = true;
                                break;
                            }
                        }
                        if (isAutoMemberAndIdentity) continue;
                        string tableName = table.TableName;
                        sw.WriteLine(FunctionBuild_SetMemory(tableName).ToString());
                    }
                }
                sw.WriteLine("#endregion");

                sw.WriteLine("#region Bảng tự sinh Key");
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        if (table.TableName == "LinkedTransactionHist")
                        {

                        }
                        bool isAutoMemberAndIdentity = false;
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsAutoNumber && column.IsIdentity)
                            {
                                isAutoMemberAndIdentity = true;
                                break;
                            }
                        }
                        if (!isAutoMemberAndIdentity) continue;

                        string tableName = table.TableName;
                        sw.WriteLine(FunctionBuild_SetMemory(tableName).ToString());
                    }
                }
                sw.WriteLine("#endregion");

                sw.WriteLine(endclassBuilderSet.ToString());
                #endregion

                //End insert func
                sw.WriteLine(endclassBuilder.ToString());
                sw.WriteLine(endFile.ToString());
                sw.WriteLine();
            }
            catch (Exception ex)
            {

            }
        }

        private StringBuilder FunctionBuild_SetMemory(string tableName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //if (entity is ApprovalMember)
                //    SetMemory(entity as ApprovalMember);

                functionBuild.AppendLine("if (entity is " + tableName + ")");
                functionBuild.AppendLine("    SetMemory(entity as " + tableName + ");");
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }
        private StringBuilder GetFuncInitDatabaseByTime()
        {
            var functionBuild = new StringBuilder();
            try
            {
                string textFunction = GetTextFile("InitDatabaseByTime.txt");
                functionBuild.AppendLine(textFunction);
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
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

        private static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}

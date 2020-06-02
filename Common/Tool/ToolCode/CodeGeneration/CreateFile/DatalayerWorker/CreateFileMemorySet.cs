using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker
{
    public class CreateFileMemorySet
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
                                        using QuantEdge.Entity.Keys;
                                        using System.Linq;
                                        using QuantEdge.Common.Enum;
                                        using QuantEdge.Entity;
                                        using Anotar.NLog;
                                        using QuantEdge.Entity.Entities;
                                        using QuantEdge.Lib.Broadcast;
                                        ");
                headerFile.AppendLine("namespace QuantEdge.Lib.Memory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");
                stringBuild.Append(headerFile.ToString());

                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("public partial class MemorySet : Memory");
                headerclassBuilder.AppendLine("{");
                var endclassBuilder = new StringBuilder();
                endclassBuilder.AppendLine("}");
                stringBuild.Append(headerclassBuilder.ToString());

                #region Hàm SortAllDic
                var sortAllDic = new StringBuilder();
                sortAllDic.AppendLine("public static void SortAllDic()");
                sortAllDic.AppendLine("{");
                sortAllDic.AppendLine("try");
                sortAllDic.AppendLine("{");

                var clearDic = new StringBuilder();
                clearDic.AppendLine("public static void ClearAllDic()");
                clearDic.AppendLine("{");
                clearDic.AppendLine("try");
                clearDic.AppendLine("{");
                
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
                        var listColums = new List<DatabaseColumn>();
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            DatabaseColumn column = table.Columns[i];
                            if (column.IsPK || column.IsFK)
                            {
                                listColums.Add(column);
                            }
                        }

                        if (listColums.Count == 1)
                        {
                            //chỉ sắp xếp bảng 1 khóa
                            //DicMemberInfo = DicMemberInfo.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                            sortAllDic.AppendLine(
                                "Dic" + table.TableName + " = Dic" + table.TableName + ".OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);");
                        }
                        //DicMemberInfo.Clear();
                        clearDic.AppendLine("Dic"+table.TableName+".Clear();");
                    }
                }
                sortAllDic.AppendLine("}");
                sortAllDic.AppendLine("catch (Exception ex) { LogTo.Error(ex.ToString()); }");
                sortAllDic.AppendLine("}");

                clearDic.AppendLine("}");
                clearDic.AppendLine("catch (Exception ex) { LogTo.Error(ex.ToString()); }");
                clearDic.AppendLine("}");

                stringBuild.AppendLine(sortAllDic.ToString());
                stringBuild.AppendLine(clearDic.ToString());
                #endregion

                #region Tạo hàm SetMemory --> UpdateAndInsertEntity
                var headerclassBuilderSet = new StringBuilder();
                headerclassBuilderSet.AppendLine("private static void UpdateAndInsertEntity(BaseEntity entity)");
                headerclassBuilderSet.AppendLine("{");
                var endclassBuilderSet = new StringBuilder();
                endclassBuilderSet.AppendLine("}");

                stringBuild.Append(headerclassBuilderSet.ToString());
                stringBuild.AppendLine("if (!IsSubscribeEntity(entity.GetName()))");
                stringBuild.AppendLine("return;");

                //Tạo các hàm SetMemory ở đây
                stringBuild.AppendLine("#region Bảng sinh key bằng tay");
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
                        stringBuild.Append(FunctionBuild_SetMemory(tableName).ToString());
                    }
                }
                stringBuild.AppendLine("#endregion");

                stringBuild.AppendLine("#region Bảng tự sinh Key");
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
                        stringBuild.Append(FunctionBuild_SetMemory(tableName).ToString());
                    }
                }
                stringBuild.AppendLine("#endregion");

                stringBuild.Append(endclassBuilderSet.ToString());
                #endregion

                #region Tạo hàm RemoveMemory --> RemoveEntity
                var headerclassBuilderRemove = new StringBuilder();
                headerclassBuilderRemove.AppendLine("private static void RemoveEntity(BaseEntity entity)");
                headerclassBuilderRemove.AppendLine("{");
                var endclassBuilderRemove = new StringBuilder();
                endclassBuilderRemove.AppendLine("}");

                stringBuild.Append(headerclassBuilderRemove.ToString());
                stringBuild.AppendLine("if (!IsSubscribeEntity(entity.GetName()))");
                stringBuild.AppendLine("return;");

                stringBuild.AppendLine("#region Bảng sinh key bằng tay");
                //Tạo các hàm SetMemory ở đây
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
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
                        stringBuild.Append(FunctionBuild_RemoveMemory(tableName).ToString());
                    }
                }
                stringBuild.AppendLine("#endregion");

                stringBuild.AppendLine("#region Bảng tự sinh Key");
                foreach (var table in listTable)
                {
                    if (table.IsSelected)
                    {
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
                        stringBuild.Append(FunctionBuild_RemoveMemory(tableName).ToString());
                    }
                }
                stringBuild.AppendLine("#endregion");
                stringBuild.Append(endclassBuilderRemove.ToString());
                #endregion

                //End insert func
                stringBuild.Append(endclassBuilder);
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

        private bool _isFirstSet = true;
        private bool _isFirstRemove = true;
        private StringBuilder FunctionBuild_SetMemory(string tableName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //if (entity is ApprovalMember)
                //    SetMemory(entity as ApprovalMember);
                if(_isFirstSet)
                    functionBuild.AppendLine("if (entity is " + tableName + ")");
                else functionBuild.AppendLine("else if (entity is " + tableName + ")");
                functionBuild.AppendLine("    SetMemory(entity as " + tableName + ");");

                _isFirstSet = false;
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }
        private StringBuilder FunctionBuild_RemoveMemory(string tableName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //if (entity is ApprovalMember)
                //    RemoveMemory(entity as ApprovalMember);
                if(_isFirstRemove)
                    functionBuild.AppendLine("if (entity is " + tableName + ")");
                else functionBuild.AppendLine("else if (entity is " + tableName + ")");
                functionBuild.AppendLine("    RemoveMemory(entity as " + tableName + ");");

                _isFirstRemove = false;
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker
{
    public class CreateFileMemorySetAndRemove
    {
        public void CreateFile(string folderPath, string fileName, List<DatabaseTable> listTable)
        {
            try
            {
                listTable.Sort((l, r) => String.Compare(l.TableName, r.TableName, StringComparison.Ordinal));
                var stringBuild = new StringBuilder(); 
                var headerFile = new StringBuilder();
                headerFile.AppendLine(@"using ElectricShop.Entity.Entities;
                                        using ElectricShop.Entity.Keys;
                                        ");
                headerFile.AppendLine("namespace ElectricShop.Memory");
                headerFile.AppendLine("{");
                var endFile = new StringBuilder();
                endFile.AppendLine("}");


                var headerclassBuilder = new StringBuilder();
                headerclassBuilder.AppendLine("public partial class MemorySet : Memory");
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
                        if (tableName.Contains("FxDeal"))
                        {
                            
                        }

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
                            //TH có 1 key thì key là text của object đó ví dụ ClientOrderId của BrokerOrder
                            DatabaseColumn column = listColums[0];
                            keyName = column.Name;
                        }
                        else if (listColums.Count > 1)
                        {
                            //Trường hợp lớn hơn 1 key  (GoldPositionMemberKeys)
                            keyName = tableName + "Keys";
                        }

                        stringBuild.AppendLine(FunctionBuild_GetMemory(tableName, keyName, listColums).ToString());
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

        private StringBuilder FunctionBuild_GetMemory(string tableName, string keyName, List<DatabaseColumn> lstColumns)
        {
            var functionBuild = new StringBuilder();
            try
            {
                #region Function cũ (Comment lại)
                //ví dụ
                //internal static void SetMemory(BrokerOrder objectValue)
                //{
                //    DicBrokerOrder[objectValue.BrokerOrderId] = objectValue;
                //}
                //internal static void RemoveMemory(BrokerOrder objectValue)
                //{
                //    if (DicBrokerOrder.ContainsKey(objectValue.BrokerOrderId))
                //        DicBrokerOrder.Remove(objectValue.BrokerOrderId);
                //}
                //or
                //internal static void SetMemory(LinkedTransaction objectValue)
                //{
                //    DicLinkedTransaction[objectValue.LinkedTransactionKeys] = objectValue;
                //}
                //internal static void RemoveMemory(LinkedTransaction objectValue)
                //{
                //    if (DicLinkedTransaction.ContainsKey(objectValue.LinkedTransactionKeys))
                //        DicLinkedTransaction.Remove(objectValue.LinkedTransactionKeys);
                //}

                //functionBuild.AppendLine("internal static void SetMemory(" + tableName + " objectValue)");
                //functionBuild.AppendLine("{");
                //functionBuild.AppendLine("Dic" + tableName + "[objectValue." + keyName + "] = objectValue;");
                //functionBuild.AppendLine("}");
                //functionBuild.AppendLine("internal static void RemoveMemory(" + tableName + " objectValue)");
                //functionBuild.AppendLine("{");
                //functionBuild.AppendLine("if (Dic" + tableName + ".ContainsKey(objectValue." + keyName + "))");
                //functionBuild.AppendLine("Dic" + tableName + ".Remove(objectValue." + keyName + ");");
                //functionBuild.AppendLine("}");
                //functionBuild.AppendLine(); 
                #endregion


                //Hàm mới không phân biệt nhiều hay 1 khóa
                // internal static void SetMemory(UserAssigned objectValue)
                //{
                //    var key = new UserAssignedKeys
                //    {
                //        MemberId_Key = objectValue.MemberId_Key,
                //        MemberId_Value = objectValue.MemberId_Value,
                //    };
                //    DicUserAssigned[key] = objectValue;
                //}

                //Hàm mới phải tạo key
                if (keyName.EndsWith("Keys") && lstColumns.Count > 1)
                {
                    //update
                    functionBuild.AppendLine("internal static void SetMemory(" + tableName + " objectValue)");
                    functionBuild.AppendLine("{");

                    functionBuild.AppendLine("var key = new " + keyName);
                    functionBuild.AppendLine("{");
                    int count = 0;
                    foreach (var column in lstColumns)
                    {
                        count++;
                        if (count != lstColumns.Count) 
                            functionBuild.AppendLine(column.Name + " = " + "objectValue." + column.Name + ",");
                        else
                            functionBuild.AppendLine(column.Name + " = " + "objectValue." + column.Name);
                    }
                    functionBuild.AppendLine("};");

                    functionBuild.AppendLine("Dic" + tableName + "[key] = objectValue;");
                    functionBuild.AppendLine("}");

                    //remove
                    functionBuild.AppendLine("internal static void RemoveMemory(" + tableName + " objectValue)");
                    functionBuild.AppendLine("{");

                    functionBuild.AppendLine("var key = new " + keyName);
                    functionBuild.AppendLine("{");
                    count = 0;
                    foreach (var column in lstColumns)
                    {
                        count++;
                        if (count != lstColumns.Count)
                            functionBuild.AppendLine(column.Name + " = " + "objectValue." + column.Name + ",");
                        else
                            functionBuild.AppendLine(column.Name + " = " + "objectValue." + column.Name);
                    }
                    functionBuild.AppendLine("};");

                    functionBuild.AppendLine("if (Dic" + tableName + ".ContainsKey(key))");
                    functionBuild.AppendLine("Dic" + tableName + ".Remove(key);");
                    functionBuild.AppendLine("}");
                }
                else
                {
                    //Lưu như cũ
                    functionBuild.AppendLine("internal static void SetMemory(" + tableName + " objectValue)");
                    functionBuild.AppendLine("{");
                    functionBuild.AppendLine("string entityName = objectValue.GetName();");
                    functionBuild.AppendLine("// chua co thi khoi tao");
                    functionBuild.AppendLine("if (!DicMaxKeyEntity.ContainsKey(entityName))");
                    functionBuild.AppendLine("DicMaxKeyEntity[entityName] = 0;");
                    functionBuild.AppendLine("// co roi thi so sanh roi set max key vao dic");
                    functionBuild.AppendLine("if (DicMaxKeyEntity[entityName] < objectValue."+ keyName +")");
                    functionBuild.AppendLine("{");
                    functionBuild.AppendLine("DicMaxKeyEntity[entityName] = objectValue." + keyName + ";");
                    functionBuild.AppendLine("}");

                    functionBuild.AppendLine("Dic" + tableName + "[objectValue." + keyName + "] = objectValue;");
                    functionBuild.AppendLine("}");
                    functionBuild.AppendLine("internal static void RemoveMemory(" + tableName + " objectValue)");
                    functionBuild.AppendLine("{");
                    functionBuild.AppendLine("if (Dic" + tableName + ".ContainsKey(objectValue." + keyName + "))");
                    functionBuild.AppendLine("Dic" + tableName + ".Remove(objectValue." + keyName + ");");
                    functionBuild.AppendLine("}");
                }
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }
    }
}

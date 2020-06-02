using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration.CreateFile.DatalayerWorker
{
    public class CreateFileMemoryGetAll
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

                        stringBuild.AppendLine(FunctionBuild_GetMemory(tableName, keyName).ToString());
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

        private StringBuilder FunctionBuild_GetMemory(string tableName, string keyName)
        {
            var functionBuild = new StringBuilder();
            try
            {
                //ví dụ
                //public static List<AccountInfo> GetAllAccountInfo()
                //{
                //    return DicAccountInfo.Select(account => account.Value.Clone() as AccountInfo).ToList();
                //}

                functionBuild.AppendLine("public static List<" + tableName + "> GetAll" + tableName + "()");
                functionBuild.AppendLine("{");
                functionBuild.AppendLine("return Dic" + tableName + ".Select(obj => obj.Value.Clone() as " + tableName + ").ToList();");
                functionBuild.AppendLine("}");
                //functionBuild.AppendLine();
            }
            catch (Exception ex)
            {

            }
            return functionBuild;
        }
    }
}

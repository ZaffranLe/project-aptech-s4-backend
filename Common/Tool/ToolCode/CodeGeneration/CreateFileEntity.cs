using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonicationMemory.Common;
using CommonicationMemory.Config;
using CommonicationMemory.Properties;

namespace CommonicationMemory.CodeGeneration
{
    public class CreateFileEntity
    {
        public static void GenerateEntites(DatabaseTable table, string BusinessLayerRootPath)
        {
            string forderEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar + "Entities\\";
            if (!Directory.Exists(forderEntity))
                Directory.CreateDirectory(forderEntity);

            string forderKeyEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar + "Keys\\";
            if (TierGeneratorSettings.Instance.GenObjectKeys && !Directory.Exists(forderKeyEntity))
                Directory.CreateDirectory(forderKeyEntity);

            string file = forderEntity + table.ClassName + ".cs";
            string className = table.ClassName;

            #region Lấy thông tin Entity trong bảng hiện tại (Lấy comment và custom method)
            Dictionary<string, string> dicComment = null;
            Dictionary<string, string> dicFields = null;
            string customMethodString = "";
            string fileCs = ConfigGlobal.SettingConfig.Setting_FoudationLink + "\\Entity\\Entities\\" + className + ".cs";
            if (!string.IsNullOrEmpty(ConfigGlobal.SettingConfig.Setting_FoudationLink) && ConfigGlobal.SettingConfig.Setting_CheckGenByForder)
            {
                if (File.Exists(fileCs))
                {
                    using (StreamReader streamReader = new StreamReader(fileCs))
                    {
                        string text = streamReader.ReadToEnd();
                        streamReader.Close();
                        dicFields = FindFieldInEntity(text, out dicComment, out customMethodString);
                    }


                    //var streamReader = new StreamReader(fileCs);
                    //string text = streamReader.ReadToEnd();
                    //streamReader.Close();
                    //dicFields = FindFieldInEntity(text, out dicComment, out customMethodString);
                }
            }
            if (dicComment == null) dicComment = new Dictionary<string, string>();
            if (dicFields == null) dicFields = new Dictionary<string, string>();
            #endregion

            var stringBuild = new StringBuilder();
            //using (StreamWriter sw = new StreamWriter(file))
            {
                #region Header
                stringBuild.Append("using System;");
                stringBuild.AppendLine("using System.IO;");
                stringBuild.AppendLine("using System.Text;");
                stringBuild.AppendLine("using System.Data;");
                stringBuild.AppendLine("");

                stringBuild.AppendLine("namespace ElectricShop.Entity.Entities");
                stringBuild.AppendLine("{");
                stringBuild.AppendLine("\tpublic class " + className + ": BaseEntity");
                stringBuild.AppendFormat("\t{{");
                #endregion

                stringBuild.AppendLine("");
                #region Enumeration For Column Name

                stringBuild.AppendLine("\t\t#region InnerClass");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\tpublic enum " + className + "Fields");
                stringBuild.AppendLine("\t\t{");

                var listColums = new List<DatabaseColumn>();
                table.Columns = table.Columns.OrderBy(x => x.Name).ToList();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DatabaseColumn column = table.Columns[i];
                    string end = ",";
                    if (i == table.Columns.Count - 1) end = "";
                    stringBuild.AppendLine("\t\t\t" + column.PropertyName + end);

                    if (column.IsPK || column.IsFK)
                    {
                        listColums.Add(column); //Key
                    }
                }
                stringBuild.AppendLine("\t\t}");
                stringBuild.AppendLine("");

                //lưu thông tin public enum column key
                stringBuild.AppendLine("\t\tpublic enum " + className + "Key");
                stringBuild.AppendLine("\t\t{");
                for (int i = 0; i < listColums.Count; i++)
                {
                    if (i != listColums.Count - 1)
                        stringBuild.AppendLine("\t\t\t" + listColums[i].Name + ",");
                    else
                    {
                        stringBuild.AppendLine("\t\t\t" + listColums[i].Name);
                    }
                }
                stringBuild.AppendLine("\t\t}");

                //Tạo hàm lấy khóa nếu có nhiều keys
                if (listColums.Count > 1)
                {
                    stringBuild.Insert(0, "using ElectricShop.Entity.Keys;\r\n");

                    stringBuild.AppendLine("\t\tpublic " + className + "Keys Get" + className + "Keys()");
                    stringBuild.AppendLine("\t\t{");
                    stringBuild.AppendLine("\t\t\treturn new " + className + "Keys");
                    stringBuild.AppendLine("\t\t\t{");
                    for (int i = 0; i < listColums.Count; i++)
                    {
                        if (i != listColums.Count - 1)
                            stringBuild.AppendLine("\t\t\t\t" +listColums[i].Name + " = " + listColums[i].Name + ",");
                        else
                        {
                            stringBuild.AppendLine("\t\t\t\t" +listColums[i].Name + " = " + listColums[i].Name);
                        }
                    }
                    stringBuild.AppendLine("\t\t\t};");
                    stringBuild.AppendLine("\t\t}");
                }
                

                stringBuild.AppendLine("\t\t#endregion");

                #endregion

                stringBuild.AppendLine("");
                #region Contructor

                stringBuild.AppendLine("\t\t#region Contructor");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\tpublic " + className + "()");
                stringBuild.AppendLine("\t\t{");
                if (TierGeneratorSettings.Instance.GenObjectKeys)
                {
                    var key = GetKeyTable(table);
                    stringBuild.AppendLine("\t\t\t" + key + " = new " + key + "();");
                }
                stringBuild.AppendLine("\t\t}");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t#endregion");

                #endregion

                stringBuild.AppendLine("");
                #region Properties

                stringBuild.AppendLine("\t\t#region Properties");
                stringBuild.AppendLine("");

                var isMultiKey = table.Columns.Count(x => x.IsPK) > 1;
                if (isMultiKey && TierGeneratorSettings.Instance.GenObjectKeys)
                {
                    #region Tạo khóa nhiều fields
                    var key = GetKeyTable(table);
                    stringBuild.AppendLine("\t\tpublic " + key + "  " + key + " { get; set; }//Key");

                    string fileKey = forderKeyEntity + table.ClassName + "Keys.cs";
                    using (StreamWriter swKey = new StreamWriter(fileKey))
                    {
                        var fileTemp = Resources.Keys;
                        var stringPropertices = new StringBuilder();
                        var stringHashCode = new StringBuilder();
                        var stringEqual = new StringBuilder();

                        var i = 0;
                        var listKey = table.Columns.Where(x => x.IsPK).ToList();
                        foreach (var column in listKey.OrderBy(x => x.Name))
                        {
                            if (i == 0)
                            {
                                stringPropertices.AppendLine("public " + column.CSharpDataTypeName + "  " + column.PropertyName + " { get; set; }//Key");
                                stringHashCode.AppendLine("int result = " + column.PropertyName + ".GetHashCode();");
                            }
                            else
                            {
                                stringPropertices.AppendLine("\t\tpublic " + column.CSharpDataTypeName + "  " + column.PropertyName + " { get; set; }//Key");
                                stringHashCode.AppendLine("\t\t\t\tresult = (result * 397) ^ " + column.PropertyName + ".GetHashCode();");
                            }
                            if (i < listKey.Count - 1)
                                stringEqual.Append(column.PropertyName + " == item." + column.PropertyName + " && ");
                            else
                                stringEqual.Append(column.PropertyName + " == item." + column.PropertyName);
                            i++;
                        }
                        fileTemp = fileTemp.Replace("%KeysEquals%", stringEqual.ToString());
                        fileTemp = fileTemp.Replace("%KeysProperties%", stringPropertices.ToString());
                        fileTemp = fileTemp.Replace("%KeysHashCode%", stringHashCode.ToString());
                        fileTemp = fileTemp.Replace("%EntityName%", table.TableName);
                        swKey.Write(fileTemp);
                    }
                    #endregion
                }

                foreach (var column in table.Columns.OrderBy(x => x.Name))
                {
                    if (isMultiKey && column.IsPK && TierGeneratorSettings.Instance.GenObjectKeys) continue;
                    string comment = "";
                    if (dicComment.ContainsKey(column.PropertyName))
                        comment = " " + dicComment[column.PropertyName];
                    string dataType = column.CSharpDataTypeName;
                    if (dicFields.ContainsKey(column.PropertyName))
                    {
                        //Chuyển type thành type? 
                        if (dicFields[column.PropertyName].Replace("?", "") == dataType.Replace("?", ""))
                        {
                            dataType = dicFields[column.PropertyName]; //Nếu ko đổi kiểu dữ liệu thì dữ liệu theo như trong entity cũ
                        }
                    }

                    if (column.IsPK)
                    {
                        comment = " //Key " + comment.Replace("//Key", "").Trim();
                        stringBuild.AppendLine("\t\tpublic " + dataType + "  " + column.PropertyName +
                                               " { get; set; }" + comment);
                    }
                    else
                    {
                        stringBuild.AppendLine("\t\tpublic " + dataType + "  " + column.PropertyName + " { get; set; }" + comment);
                    }
                }

                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t#endregion");

                #endregion

                stringBuild.AppendLine("");
                #region Validation

                stringBuild.AppendLine("\t\t#region Validation");
                stringBuild.AppendLine("");

                stringBuild.AppendLine("\t\tpublic override bool IsValid()");
                stringBuild.AppendLine("\t\t{");

                foreach (DatabaseColumn column in table.Columns.OrderBy(x => x.Name))
                {
                    var text = CheckIsNotNull(table.ClassName, column);
                    if (!string.IsNullOrWhiteSpace(text)) stringBuild.AppendLine(text);
                    switch (column.DotNetDataTypeName.ToLower())
                    {
                        case "string":
                            text = CheckString(table.ClassName, column);
                            if (!string.IsNullOrWhiteSpace(text)) stringBuild.AppendLine(text);
                            break;
                    }

                }
                stringBuild.AppendLine("\t\t\treturn true;");
                stringBuild.AppendLine("\t\t}");

                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t#endregion");

                #endregion

                stringBuild.AppendLine("");
                #region EntityName, GetName

                stringBuild.AppendLine("\t\t#region EntityName, GetName");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\tpublic static string EntityName()");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine(string.Format("\t\t\treturn typeof({0}).Name;", table.TableName));
                stringBuild.AppendLine("\t\t}");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\tpublic override string GetName()");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\treturn EntityName();");
                stringBuild.AppendLine("\t\t}");


                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t#endregion");

                #endregion

                stringBuild.AppendLine("");
                #region Custom Method
                //Đọc file để lấy thông tin custom method
                //Ví dụ FxDeal thì có hàm IsLenhVangLai, IsLenhDelayTrade...
                if (string.IsNullOrEmpty(customMethodString))
                {
                    customMethodString = "#region Custom Method\r\n";
                    customMethodString += "#endregion\r\n";
                }
                stringBuild.AppendLine(customMethodString);

                #endregion

                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t}"); // END OF CLASS
                stringBuild.AppendLine("}"); // END OF NAME SPACE
            }

            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(stringBuild.ToString());
                sw.Close();
            }
            
            if (File.Exists(fileCs) && ConfigGlobal.SettingConfig.Setting_CheckGenByForder)
            {
                File.Delete(fileCs);
                using (var sw = new StreamWriter(fileCs))
                {
                    sw.WriteLine(stringBuild.ToString());
                    sw.Close();
                }
            }
        }

        public static Dictionary<string, string> FindFieldInEntity(string fileText,
            out Dictionary<string, string> dicComment, out string customMethodRegion)
        {
            var dicField = new Dictionary<string, string>();
            dicComment = new Dictionary<string, string>();
            customMethodRegion = "";
            try
            {
                if (fileText.Contains("FxDeal"))
                {

                }
                string[] splits = fileText.Split("\r\n".ToCharArray());
                int count = 0;
                bool isBeginCustomMethod = false;
                int countRegionCustomMethod = 0; //Đếm xem có bao nhiêu region cần phải đóng khi isBeginCustomMethod = true;

                if (splits.Length > 0)
                {
                    foreach (string split in splits)
                    {
                        #region Lưu custom method
                        if (string.IsNullOrEmpty(split)) continue;
                        if (split.ToLower().Contains("#region custom method"))
                        {
                            isBeginCustomMethod = true;
                            customMethodRegion = "\t\t#region Custom Method\r\n";
                            continue;
                        }
                        if (isBeginCustomMethod)
                        {
                            if (split.Contains("#region"))
                            {
                                //Mở thêm 1 region trong custom
                                countRegionCustomMethod++;
                            }
                            if (split.Contains("#endregion"))
                            {
                                //Đóng custom method
                                if (countRegionCustomMethod == 0)
                                    isBeginCustomMethod = false;
                                else
                                {
                                    countRegionCustomMethod--; //Giảm bớt 1 lần đóng region
                                }
                            }
                            customMethodRegion += split + "\r\n";
                            continue; //Ko check thông tin khác khi lưu custom method
                        }

                        #endregion

                        count = 0;
                        if (string.IsNullOrEmpty(split)) continue;
                        if (split.Contains("public") &&
                            split.Contains("get") &&
                            split.Contains("set"))
                        {
                            //Bỏ qua các trường không lưu database
                            if (split.Contains("NotSetDb")) continue;

                            if (split.Contains("?"))
                            {

                            }

                            //Xử lý lấy thông tin
                            string result = split.Replace("public ", "").Trim();
                            string[] splitResult = result.Split(" ".ToCharArray());
                            string tenBien = "";
                            string tenField = "";

                            foreach (var text in splitResult)
                            {
                                if (!string.IsNullOrWhiteSpace(text.Trim()))
                                {
                                    count++;
                                    if (count == 1)
                                    {
                                        //Đây là tên biến
                                        tenBien = text.Trim();
                                    }
                                    if (count == 2)
                                    {
                                        //Đây là tên entity field
                                        tenField = text.Trim();
                                        dicField[tenField] = tenBien; //AccountId string
                                    }
                                }
                            }

                            //Lấy thông tin comment
                            if (result.Contains("//"))
                            {
                                int index = result.IndexOf("//", StringComparison.Ordinal);
                                string comment = result.Substring(index, result.Length - index);
                                dicComment[tenField] = comment;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"error" + ex);
            }
            return dicField;
        }

        private static List<string> NotCheckTypeNull = new List<string> { "int32", "int64", "long", "int", "datetime" };
        
        private static string CheckIsNotNull(string entity, DatabaseColumn column)
        {
            if (column == null || column.IsNull) return string.Empty;
            if (NotCheckTypeNull.Contains(column.DotNetDataTypeName.ToLower())) return string.Empty;
            return "\t\t\t"
                    + "if (" + column.PropertyName + " == null)"
                    + "\r\n\t\t\t\t"
                    + "throw new NoNullAllowedException(\"Field: " + column.PropertyName + " in entity: " + entity + " is Null\");";
        }

        private static string CheckString(string entity, DatabaseColumn column)
        {
            var text = "";
            if (column.IsPK || column.IsFK)
            {
                text += "\t\t\t"
                    + "if (" + column.PropertyName + " != null && " + column.PropertyName + ".Trim() == String.Empty)"
                    + "\r\n\t\t\t\t"
                    + "throw new InvalidDataException(\"Field: " + column.PropertyName + " in entity: " + entity + " is Empty\");";
            }
            text += "\r\n";
            if (column.ColumnSize > 0)
            {
                text += "\t\t\t"
                    + "if (" + column.PropertyName + " != null && " + column.PropertyName + ".Length > " + column.ColumnSize + " )"
                    + "\r\n\t\t\t\t"
                    + "throw new InvalidDataException(\"Field: " + column.PropertyName + " in entity: " + entity + " is over-size: " + column.ColumnSize + ", value=\" + " + column.PropertyName + ");";
            }
            return text;
        }

        private static string GetKeyTable(DatabaseTable table)
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
    }
}

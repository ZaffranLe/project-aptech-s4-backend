﻿using System;
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
    public class CreateFileController
    {
        public static void GenerateEntites(DatabaseTable table, string BusinessLayerRootPath)
        {
            string forderEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar + "ControllerApi\\";
            if (!Directory.Exists(forderEntity))
                Directory.CreateDirectory(forderEntity);

            string file = forderEntity + table.ClassName + ".cs";
            string className = table.ClassName;

            #region Lấy thông tin Entity trong bảng hiện tại (Lấy comment và custom method)
            Dictionary<string, string> dicComment = null;
            Dictionary<string, string> dicFields = null;
            string customMethodString = "";
            string fileCs = ConfigGlobal.SettingConfig.Setting_FoudationLink + "\\ControllerApi\\" + className + "Controller.cs";
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
            #endregion

            var stringBuild = new StringBuilder();
            {
                #region Header
                stringBuild.Append("using System;");
                stringBuild.AppendLine("using System.Collections.Generic;");
                stringBuild.AppendLine("using System.Threading.Tasks;");
                stringBuild.AppendLine("using System.Web.Http;");
                stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                stringBuild.AppendLine("using ElectricShop.DatabaseDAL.Common;");
                stringBuild.AppendLine("using ElectricShop.Entity;");
                stringBuild.AppendLine("using ElectricShop.Entity.Entities;");
                stringBuild.AppendLine("using ElectricShop.Memory;");
                stringBuild.AppendLine("using ElectricShop.Models;");
                stringBuild.AppendLine("using ElectricShop.Utils;");

                stringBuild.AppendLine("namespace ElectricShop.Controllers");
                stringBuild.AppendLine("{");
                stringBuild.AppendLine("\tpublic class " + className + "Controller" + ": ApiController");
                stringBuild.AppendFormat("\t{{");
                #endregion

                stringBuild.AppendLine("");

                #region get all
                stringBuild.AppendLine("\t\tpublic async Task<IHttpActionResult> Get()");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), \"Sai token\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine($"\t\t\t\tvar lstData = MemoryInfo.GetAll{className}();");
                stringBuild.AppendLine("\t\t\t\tvar res = new RequestErrorCode(true, null, null);");
                stringBuild.AppendLine("\t\t\t\tres.ListDataResult.AddRange(lstData);");
                stringBuild.AppendLine("\t\t\t\treturn Ok(res);");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\tcatch (Exception ex)");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tLogger.Write(ex.ToString());");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\treturn BadRequest(\"Unknow\");");
                stringBuild.AppendLine("\t\t}");
                #endregion

                stringBuild.AppendLine("");
                #region get by id
                stringBuild.AppendLine("\t\tpublic async Task<IHttpActionResult> Get(int id)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), \"Sai token\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\tvar res = MemoryInfo.GetCustomer(id);");
                stringBuild.AppendLine("\t\t\t\treturn Ok(res);");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\tcatch (Exception ex)");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tLogger.Write(ex.ToString());");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\treturn BadRequest(\"Unknow\");");
                stringBuild.AppendLine("\t\t}");


                #endregion

                stringBuild.AppendLine("");
                #region Post
                stringBuild.AppendLine($"\t\tpublic async Task<IHttpActionResult> Post([FromBody]{table.ClassName} req)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tstring errorMessage = \"UnknowError\";");
                stringBuild.AppendLine("\t\t\t\tstring errorCode = ErrorCodeEnum.UnknownError.ToString();");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), \"Sai token\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Validate");
                stringBuild.AppendLine("\t\t\t\tif (!Validate(req, out errorCode, out errorMessage))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Tạo key");
                stringBuild.AppendLine("\t\t\t\tvar oldKey = Memory.Memory.GetMaxKey(req.GetName());");
                stringBuild.AppendLine("\t\t\t\tint newKey = oldKey + 1;");
                stringBuild.AppendLine("\t\t\t\t// set key");
                stringBuild.AppendLine("\t\t\t\treq.Id = newKey;");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Process");
                stringBuild.AppendLine("\t\t\t\t// update memory");
                stringBuild.AppendLine("\t\t\t\tMemorySet.UpdateAndInsertEntity(req);");
                stringBuild.AppendLine("\t\t\t\tUpdateEntitySql updateEntitySql = new UpdateEntitySql();");
                stringBuild.AppendLine("\t\t\t\tvar lstCommand = new List<EntityCommand>();");
                stringBuild.AppendLine("\t\t\t\tlstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });");
                stringBuild.AppendLine("\t\t\t\tbool isOkDone = updateEntitySql.UpdateDefault(lstCommand);");
                stringBuild.AppendLine("\t\t\t\tif (!isOkDone)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\tvar result = new RequestErrorCode(true);");
                stringBuild.AppendLine("\t\t\t\treturn Ok(result);");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\tcatch (Exception ex)");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tLogger.Write(ex.ToString());");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\treturn BadRequest(\"Unknow\");");
                stringBuild.AppendLine("\t\t}");


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

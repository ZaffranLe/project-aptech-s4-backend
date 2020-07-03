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
    public class CreateFileController
    {
        public static void GenerateControllerApi(DatabaseTable table, string BusinessLayerRootPath)
        {
            string forderEntity = BusinessLayerRootPath + Path.DirectorySeparatorChar + "ControllerApi\\";
            if (!Directory.Exists(forderEntity))
                Directory.CreateDirectory(forderEntity);

            string file = forderEntity + table.ClassName + "Controller.cs";
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
                stringBuild.AppendLine("using System;");
                stringBuild.AppendLine("using System.Net;");
                stringBuild.AppendLine("using System.Collections.Generic;");
                stringBuild.AppendLine("using System.Threading.Tasks;");
                stringBuild.AppendLine("using System.Web.Http;");
                stringBuild.AppendLine("using ElectricShop.Common.Enum;");
                stringBuild.AppendLine("using System.Web.Http.Cors;");
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
                stringBuild.AppendLine("\t\t[EnableCors(origins: \"*\", headers: \"*\", methods: \"*\")]");
                stringBuild.AppendLine("\t\tpublic async Task<IHttpActionResult> Get()");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tif (header.Authorization == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
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
                stringBuild.AppendLine("\t\t[EnableCors(origins: \"*\", headers: \"*\", methods: \"*\")]");
                stringBuild.AppendLine("\t\tpublic async Task<IHttpActionResult> Get(int id)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tif (header.Authorization == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine($"\t\t\t\tvar data = MemoryInfo.Get{className}(id);");
                stringBuild.AppendLine("\t\t\t\tvar res = new RequestErrorCode(true, null, null);");
                stringBuild.AppendLine("\t\t\t\tres.ListDataResult.Add(data);");
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
                stringBuild.AppendLine("\t\t[EnableCors(origins: \"*\", headers: \"*\", methods: \"*\")]");
                stringBuild.AppendLine($"\t\tpublic async Task<IHttpActionResult> Post([FromBody]{table.ClassName} req)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tstring errorMessage = \"UnknowError\";");
                stringBuild.AppendLine("\t\t\t\tstring errorCode = ErrorCodeEnum.UnknownError.ToString();");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tif (header.Authorization == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
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
                stringBuild.AppendLine("\t\t\t\treq.CreatedAt = DateTime.Now;");
                stringBuild.AppendLine("\t\t\t\treq.CreatedBy = userInfo.IdUserLogin;");
                stringBuild.AppendLine("\t\t\t\tUpdateEntitySql updateEntitySql = new UpdateEntitySql();");
                stringBuild.AppendLine("\t\t\t\tvar lstCommand = new List<EntityCommand>();");
                stringBuild.AppendLine("\t\t\t\tlstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });");
                stringBuild.AppendLine("\t\t\t\tbool isOkDone = updateEntitySql.UpdateDefault(lstCommand);");
                stringBuild.AppendLine("\t\t\t\tif (!isOkDone)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\t// update memory");
                stringBuild.AppendLine("\t\t\t\tMemorySet.UpdateAndInsertEntity(req);");
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
                #region put
                stringBuild.AppendLine("\t\t[EnableCors(origins: \"*\", headers: \"*\", methods: \"*\")]");
                stringBuild.AppendLine($"\t\tpublic async Task<IHttpActionResult> Put(int id,[FromBody]{table.ClassName} req)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tstring errorMessage = \"UnknowError\";");
                stringBuild.AppendLine("\t\t\t\tstring errorCode = ErrorCodeEnum.UnknownError.ToString();");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tif (header.Authorization == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Validate");
                stringBuild.AppendLine("\t\t\t\tif (!ValidateUpdate(req, out errorCode, out errorMessage))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Check exist");
                stringBuild.AppendLine($"\t\t\t\tvar obj = MemoryInfo.Get{table.ClassName}(id);");
                stringBuild.AppendLine("\t\t\t\tif (obj == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), \"Khong ton tai\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\treq.Id = obj.Id; // gan lai id de update");
                stringBuild.AppendLine("\t\t\t\t#region Process");
                stringBuild.AppendLine("\t\t\t\treq.UpdatedAt = DateTime.Now;");
                stringBuild.AppendLine("\t\t\t\treq.UpdatedBy = userInfo.IdUserLogin;");
                stringBuild.AppendLine("\t\t\t\tUpdateEntitySql updateEntitySql = new UpdateEntitySql();");
                stringBuild.AppendLine("\t\t\t\tvar lstCommand = new List<EntityCommand>();");
                stringBuild.AppendLine("\t\t\t\tlstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Update });");
                stringBuild.AppendLine("\t\t\t\tbool isOkDone = updateEntitySql.UpdateDefault(lstCommand);");
                stringBuild.AppendLine("\t\t\t\tif (!isOkDone)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\t// update memory");
                stringBuild.AppendLine("\t\t\t\tMemorySet.UpdateAndInsertEntity(req);");
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
                #region delete
                stringBuild.AppendLine("\t\t[EnableCors(origins: \"*\", headers: \"*\", methods: \"*\")]");
                stringBuild.AppendLine("\t\tpublic async Task<IHttpActionResult> Delete(int id)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tstring errorMessage = \"UnknowError\";");
                stringBuild.AppendLine("\t\t\t\tstring errorCode = ErrorCodeEnum.UnknownError.ToString();");
                stringBuild.AppendLine("\t\t\t\t#region token");
                stringBuild.AppendLine("\t\t\t\tvar header = Request.Headers;");
                stringBuild.AppendLine("\t\t\t\tif (header.Authorization == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\tvar token = header.Authorization.Parameter;");
                stringBuild.AppendLine("\t\t\t\tUserInfo userInfo;");
                stringBuild.AppendLine("\t\t\t\tif (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn StatusCode(HttpStatusCode.Unauthorized);");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Check exist");
                stringBuild.AppendLine($"\t\t\t\tvar obj = MemoryInfo.Get{table.ClassName}(id);");
                stringBuild.AppendLine("\t\t\t\tif (obj == null)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), \"Khong ton tai\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t// check role ");
                stringBuild.AppendLine("\t\t\t\tif (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.None))");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\treturn Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), \"Khong co quyen xoa\"));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t\t#region Process");
                stringBuild.AppendLine("\t\t\t\tUpdateEntitySql updateEntitySql = new UpdateEntitySql();");
                stringBuild.AppendLine("\t\t\t\tvar lstCommand = new List<EntityCommand>();");
                stringBuild.AppendLine("\t\t\t\tlstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(obj), EntityAction = EntityAction.Delete });");
                stringBuild.AppendLine("\t\t\t\tbool isOkDone = updateEntitySql.UpdateDefault(lstCommand);");
                stringBuild.AppendLine("\t\t\t\tif (!isOkDone)");
                stringBuild.AppendLine("\t\t\t\t{");
                stringBuild.AppendLine("\t\t\t\t\treturn Ok(new RequestErrorCode(false, errorCode, errorMessage));");
                stringBuild.AppendLine("\t\t\t\t}");
                stringBuild.AppendLine("\t\t\t\t#endregion");
                stringBuild.AppendLine("\t\t\t\t// update memory");
                stringBuild.AppendLine("\t\t\t\tMemorySet.RemoveEntity(obj);");
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
                #region Custom Function
                stringBuild.AppendLine("\t\t#region Validation");
                stringBuild.AppendLine($"\t\tprivate bool Validate({table.ClassName} obj, out string errorCode, out string errorMess)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\terrorCode = null;");
                stringBuild.AppendLine("\t\t\terrorMess = null;");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\tcatch (Exception ex)");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tLogger.Write(ex.ToString());");
                stringBuild.AppendLine("\t\t\t\tthrow;");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\treturn true;");
                stringBuild.AppendLine("\t\t}");
                stringBuild.AppendLine("");
                
                stringBuild.AppendLine($"\t\tprivate bool ValidateUpdate({table.ClassName} obj, out string errorCode, out string errorMess)");
                stringBuild.AppendLine("\t\t{");
                stringBuild.AppendLine("\t\t\terrorCode = null;");
                stringBuild.AppendLine("\t\t\terrorMess = null;");
                stringBuild.AppendLine("\t\t\ttry");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\tcatch (Exception ex)");
                stringBuild.AppendLine("\t\t\t{");
                stringBuild.AppendLine("\t\t\t\tLogger.Write(ex.ToString());");
                stringBuild.AppendLine("\t\t\t\tthrow;");
                stringBuild.AppendLine("\t\t\t}");
                stringBuild.AppendLine("\t\t\treturn true;");
                stringBuild.AppendLine("\t\t}");
                stringBuild.AppendLine("\t\t#endregion");
                stringBuild.AppendLine("");


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
    }
}

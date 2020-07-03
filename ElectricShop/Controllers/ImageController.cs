using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ElectricShop.Common.Enum;
using System.Web.Http.Cors;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class ImageController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                #region token
                var header = Request.Headers;
                if (header.Authorization == null)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion
                var lstImages = MemoryInfo.GetAllImage();

                var res = new RequestErrorCode(true, null, null);
                foreach (var image in lstImages)
                {
                    image.ImageUrl = AppGlobal.ElectricConfig.BaseUrl + image.ImageUrl;
                    res.ListDataResult.Add(image);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post()
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                if (header.Authorization == null)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion

                #region Tạo key
                var oldKey = Memory.Memory.GetMaxKey(Image.EntityName());
                int newKey = oldKey + 1;
                #endregion


                #region Luu vao forlder resource
                var httpRequest = HttpContext.Current.Request;
                Dictionary<HttpPostedFile, string> dicUpload = new Dictionary<HttpPostedFile, string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.png.");
                            return Ok(new RequestErrorCode(false, errorCode, message));
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");
                            return Ok(new RequestErrorCode(false, errorCode, message));
                        }
                        else
                        {
                            dicUpload[postedFile] = PasswordGenerator.GetRandomString(12, true) + extension;
                        }
                    }
                }
                #endregion

                #region Process
                var lstCommand = new List<EntityCommand>();
                List<Image> lstResponse = new List<Image>();
                foreach (var temp in dicUpload)
                {
                    var filePath = AppGlobal.ElectricConfig.FolderSaveImages + "/" + temp.Value;
                    var filePosted = temp.Key;
                    filePosted.SaveAs(filePath);
                    var data = new Image
                    {
                        CreatedAt = DateTime.Now,
                        CreatedBy = userInfo.IdUserLogin,
                        Id = newKey,
                        ImageUrl = temp.Value
                    };
                    lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(data), EntityAction = EntityAction.Insert });
                    // update memory
                    MemorySet.UpdateAndInsertEntity(data);

                    // gan lai response tra ve cho client
                    var objRes = data.Clone() as Image;
                    objRes.ImageUrl = AppGlobal.ElectricConfig.BaseUrl + temp.Value;
                    lstResponse.Add(objRes);
                    newKey++;
                }
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();


                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                var result = new RequestErrorCode(true);
                result.ListDataResult.AddRange(lstResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                if (header.Authorization == null)
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetImage(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion

                // check role 
                if (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.None))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen xoa"));
                }

                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(obj), EntityAction = EntityAction.Delete });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.RemoveEntity(obj);
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }

        #region Validation
        private bool Validate(Image obj, out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                throw;
            }
            return true;
        }

        private bool ValidateUpdate(Image obj, out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                throw;
            }
            return true;
        }
        #endregion


    }
}


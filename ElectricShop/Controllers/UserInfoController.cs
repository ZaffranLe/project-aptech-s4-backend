using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using ElectricShop.Common.Enum;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class UserInfoController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), "Sai token"));
                }


                #endregion
                var lstUserInfo = MemoryInfo.GetAllUserInfo();
                List<UserInfoResponse> lstUserRes = new List<UserInfoResponse>();
                foreach (var info in lstUserInfo)
                {
                    lstUserRes.Add(new UserInfoResponse
                    {
                        UserInfo = info,
                        ListImagesUrl = ImagesUtils.GetImagesUrl(userInfo.ImageId)
                    });
                }
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.AddRange(lstUserRes);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }
            return BadRequest("Unknow");
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]UserInfo req)
        {
            try
            {
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                #region token
                var header = Request.Headers;
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), "Sai token"));
                }

                if (userInfo == null || !Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.UpdateUser))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen"));
                }
                #endregion

                #region Validate
                if (!ValidateUpdate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetUserInfo(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion
                req.IdUserLogin = obj.IdUserLogin; // gan lai id de update
                req.UpdatedBy = userInfo.IdUserLogin;
                req.UpdatedAt = DateTime.Now;
                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Update });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(req);
                var result = new RequestErrorCode(true);
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
                var token = header.Authorization.Parameter;
                UserInfo userInfo;
                if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_InvalidToken.ToString(), "Sai token"));
                }
                if (userInfo == null || !Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.DeleteUser))
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen"));
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetUserInfo(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion

                // check role 
                if (userInfo == null || !Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.DeleteUser))
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
        private bool Validate(UserInfo obj, out string errorCode, out string errorMess)
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

        private bool ValidateUpdate(UserInfo obj, out string errorCode, out string errorMess)
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


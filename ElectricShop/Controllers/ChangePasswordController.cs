using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ChangePasswordController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]ChangePasswordReq req)
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
                // chi co admin moi co quyen tao tai khoan khac
                if(!Operator.HasPermision(userInfo.IdUserLogin,RoleDefinitionEnum.CreateUser))
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen tao user"));
                #endregion

                #region Validate
                if (!Validate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                #region Process
                // lay ra userInfo tuong ung
                var userChange = MemoryInfo.GetListUserLoginByField(req.Username, UserLogin.UserLoginFields.Username)
                    .FirstOrDefault();
                if (userChange == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_UserNotExist.ToString(), "Khong ton tai user"));
                }
                // check password 
                var oldPassEncrypt = PasswordGenerator.EncodePassword(req.Password);
                if (userChange.Password != oldPassEncrypt)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_PasswordWrong.ToString(), "Sai password"));
                }

                var newPassEncrypt = PasswordGenerator.EncodePassword(req.NewPassword);
                userChange.Password = newPassEncrypt;
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(userChange), EntityAction = EntityAction.Update });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(userChange);
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }

        #region Custom Function

        private bool Validate(ChangePasswordReq data , out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {
                if (data == null)
                {
                    errorCode = ErrorCodeEnum.DataInputWrong.ToString();
                    return false;
                }
                if (string.IsNullOrEmpty(data.Username))
                {
                    errorMess = "Username is null";
                    errorCode = ErrorCodeEnum.Error_UsernameIsNull.ToString();
                    return false;
                }

                
                if (data.Password == null )
                {
                    errorMess = "Password is null";
                    errorCode = ErrorCodeEnum.Error_PasswordIsNull.ToString();
                    return false;
                }
                if (data.Password == data.NewPassword)
                {
                    errorMess = "New Password is same password";
                    errorCode = ErrorCodeEnum.Error_NewPasswordIsSamePassword.ToString();
                    return false;
                }
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

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
    public class RegistryController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]UserRegistry req)
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
                
                #region Tạo object UserInfo va UserLogin
                var oldKey = Memory.Memory.GetMaxKey(UserLogin.EntityName());
                int newKey = oldKey + 1;
                var userLogin = new UserLogin
                {
                    Id = newKey,
                    Password = PasswordGenerator.EncodePassword(req.Password),
                    Username = req.Username,
                    UserStatus = (int)UserStatus.Active
                };
                var userInfoRegis = new UserInfo
                {
                    IdUserLogin = newKey,
                    Address = req.Address,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userInfo.IdUserLogin,
                    Email = req.Email,
                    ImageId = req.ImageId,
                    Name = req.Name,
                    Phone = req.Phone,
                    UpdatedBy = 0
                };
                #endregion
                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(userLogin), EntityAction = EntityAction.Insert });
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(userInfoRegis), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(userLogin);
                MemorySet.UpdateAndInsertEntity(userInfoRegis);
                var result = new RequestErrorCode(true);
                result.ListDataResult.Add(userInfoRegis);
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

        private bool Validate(UserRegistry userRegistry , out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {
                if (userRegistry == null)
                {
                    errorCode = ErrorCodeEnum.DataInputWrong.ToString();
                    return false;
                }
                if (string.IsNullOrEmpty(userRegistry.Username))
                {
                    errorMess = "Username is null";
                    errorCode = ErrorCodeEnum.Error_UsernameIsNull.ToString();
                    return false;
                }
                if (userRegistry.Username.Length < 5 || userRegistry.Username.Length > 32)
                {
                    errorMess = "Username Lenght invalid";
                    errorCode = ErrorCodeEnum.Error_UsernameInvalid.ToString();
                    return false;
                }

                if (!CheckUtils.ContainsUnicodeCharacter(userRegistry.Username))
                {
                    errorMess = "Username không được nhập Tiếng Việt có dấu, khoảng trắng, ký tự đặc biệt";
                    errorCode = ErrorCodeEnum.Error_UsernameIsNull.ToString();
                    return false;
                }

                if (!string.IsNullOrEmpty(userRegistry.Email))
                {
                    if (!CheckUtils.ValidateEmail(userRegistry.Email))
                    {
                        errorMess = "Email khong hop le";
                        errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
                        return false;
                    }
                }
                else
                {
                    errorMess = "Email khong duoc de trong";
                    errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
                    return false;
                }

                if (!string.IsNullOrEmpty(userRegistry.Phone))
                {
                    if (!CheckUtils.CheckValidMobile(userRegistry.Phone))
                    {
                        errorMess = "Phone khong hop le";
                        errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
                        return false;
                    }
                }
                else
                {
                    errorMess = "Phone khong duoc de trong";
                    errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
                    return false;
                }
                if (userRegistry.Name == null )
                {
                    errorMess = "Ten bat buoc phai nhap";
                    errorCode = ErrorCodeEnum.Error_NameIsNull.ToString();
                    return false;
                }

                // check trung
                var lstUserLogin =
                    MemoryInfo.GetListUserLoginByField(userRegistry.Username, UserLogin.UserLoginFields.Username);
                if (lstUserLogin.Count > 0)
                {
                    errorMess = "Đã tồn tại tài khoản";
                    errorCode = ErrorCodeEnum.Error_UsernameIsExist.ToString();
                    return false;
                }

                // check email
                var userInfoWithEmail = MemoryInfo.GetListUserInfoByField(userRegistry.Email, UserInfo.UserInfoFields.Email);
                if (userInfoWithEmail.Count > 0)
                {
                    errorMess = "Đã tồn tại tài email";
                    errorCode = ErrorCodeEnum.Error_EmailIsExist.ToString();
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

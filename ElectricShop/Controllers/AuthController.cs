using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Common.Enum;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;

namespace ElectricShop.Controllers
{
    public class AuthController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody]UserLogin req)
        {
            try
            {
                #region Validate
                string errorMessage = "UnknowError";
                string errorCode = ErrorCodeEnum.UnknownError.ToString();
                if (!Validate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // check ton tai tai khoan
                var userLogin = MemoryInfo.GetListUserLoginByField(req.Username, UserLogin.UserLoginFields.Username).FirstOrDefault(x => x.Username == req.Username);
                if (userLogin == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_UserNotExist.ToString(), "Khong ton tai tai khoan"));
                }
                var passEncrypt = PasswordGenerator.EncodePassword(req.Password);
                if (userLogin.Password != passEncrypt)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_PasswordWrong.ToString(), "Sai password"));
                }

                var userInfo = MemoryInfo.GetUserInfo(userLogin.Id);
                if (userInfo == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_UserinfoIsNull.ToString(), "Khong co thong tin Userinfo"));
                }

                #region Gen token va tra userInfo ve kem voi list quyen

                var lstPermission = MemoryInfo.GetListPermission(userInfo.IdUserLogin);
                var token = TokenManager.GenerateToken(userInfo, -1);
                var tokenRes = new TokenResponse(token,userInfo);
                tokenRes.ListPermission.AddRange(lstPermission);
                #endregion
                var result = new RequestErrorCode(true);
                result.ListDataResult.Add(tokenRes);
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

        private bool Validate(UserLogin userLogin , out string errorCode, out string errorMess)
        {
            errorCode = null;
            errorMess = null;
            try
            {
                if (string.IsNullOrEmpty(userLogin.Username))
                {
                    errorMess = "Username is null";
                    errorCode = ErrorCodeEnum.Error_UsernameIsNull.ToString();
                    return false;
                }

                if (!CheckUtils.ContainsUnicodeCharacter(userLogin.Username))
                {
                    errorMess = "Username không được nhập Tiếng Việt có dấu, khoảng trắng, ký tự đặc biệt";
                    errorCode = ErrorCodeEnum.Error_UsernameIsNull.ToString();
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Common.Enum;
using System.Web.Http.Cors;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class UserRoleController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]List<UserRoleReq> req)
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
                #endregion

                #region Validate
                if (!Validate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion


                #region Process
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                Dictionary<UserRole,RoleAction> dicAction = new Dictionary<UserRole, RoleAction>();
                foreach (var userRoleReq in req)
                {
                    var userRoleKeys = new UserRoleKeys
                    {
                        IdUserLogin = userRoleReq.UserId,
                        IdRole = userRoleReq.IdRole
                    };
                    EntityAction entityAction;
                    var userRole = MemoryInfo.GetUserRole(userRoleKeys);
                    if (userRoleReq.Action == (int) RoleAction.Remove)
                    {
                        if (userRole == null) continue; // neu khong co thi khong cho xoa
                        entityAction = EntityAction.Delete;
                        lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(userRole), EntityAction =  entityAction});
                        dicAction.Add(userRole,RoleAction.Remove);
                        continue;
                    }
                    if(userRole != null) continue; // neu co roi thi khong insert nua
                    entityAction = EntityAction.Insert;
                    var dataInsert = new UserRole
                    {
                        IdUserLogin=  userRoleReq.UserId,
                        IdRole =  userRoleReq.IdRole
                    };
                    lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(dataInsert), EntityAction = entityAction });
                    dicAction.Add(dataInsert, RoleAction.Add);
                }
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                foreach (var userRoleAction in dicAction)
                {
                    if (userRoleAction.Value == RoleAction.Add)
                    {
                        MemorySet.UpdateAndInsertEntity(userRoleAction.Key);
                    }
                    else
                    {
                        MemorySet.RemoveEntity(userRoleAction.Key);
                    }
                }
                // reload dicUserPermission
                AppGlobal.InitUserPermission("Reload vi gan lai permission cho Role");
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
        private bool Validate(List<UserRoleReq> obj, out string errorCode, out string errorMess)
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


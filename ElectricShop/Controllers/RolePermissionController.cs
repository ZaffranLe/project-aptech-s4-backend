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
    public class RolePermissionController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]List<RolePermissionReq> req)
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
                Dictionary<RolePermission,RoleAction> dicAction = new Dictionary<RolePermission, RoleAction>();
                foreach (var rolePermissionReq in req)
                {
                    var rolePermissionKeys = new RolePermissionKeys
                    {
                        IdPermission = rolePermissionReq.IdPermission,
                        IdRole = rolePermissionReq.IdRole
                    };
                    EntityAction entityAction;
                    var rolePermission = MemoryInfo.GetRolePermission(rolePermissionKeys);
                    if (rolePermissionReq.Action == (int) RoleAction.Remove)
                    {
                        if (rolePermission == null) continue; // neu khong co thi khong cho xoa
                        entityAction = EntityAction.Delete;
                        lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(rolePermission), EntityAction =  entityAction});
                        dicAction.Add(rolePermission,RoleAction.Remove);
                        continue;
                    }
                    if(rolePermission != null) continue; // neu co roi thi khong insert nua
                    entityAction = EntityAction.Insert;
                    var dataInsert = new RolePermission
                    {
                        IdPermission = rolePermissionReq.IdPermission,
                        IdRole =  rolePermissionReq.IdRole
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
                foreach (var rolePermissionAction in dicAction)
                {
                    if (rolePermissionAction.Value == RoleAction.Add)
                    {
                        MemorySet.UpdateAndInsertEntity(rolePermissionAction.Key);
                    }
                    else
                    {
                        MemorySet.RemoveEntity(rolePermissionAction.Key);
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
        private bool Validate(List<RolePermissionReq> obj, out string errorCode, out string errorMess)
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


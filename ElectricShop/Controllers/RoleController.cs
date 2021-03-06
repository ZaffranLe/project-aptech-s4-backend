﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class RoleController : ApiController
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
                var lstData = MemoryInfo.GetAllRole();
                var lstRes = new List<RoleResponse>();
                foreach (var role in lstData)
                {
                    var roleRes = new RoleResponse
                    {
                        Role = role,
                        ListPermission = new List<Permission>()
                    };
                    // lay ra lstPermission
                    var lstPermission = Memory.Memory.DicRoleMapping[role.Id];
                    roleRes.ListPermission = new List<Permission>();
                    roleRes.ListPermission.AddRange(lstPermission);
                    lstRes.Add(roleRes);
                }
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.AddRange(lstRes);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Get(int id)
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
                var res = MemoryInfo.GetCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Post([FromBody]Role req)
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

                #region Tao key
                var oldKey = Memory.Memory.GetMaxKey(req.GetName());
                int newKey = oldKey + 1;
                // set key
                req.Id = newKey;
                #endregion

                #region Process
                req.CreatedAt = DateTime.Now;
                req.CreatedBy = userInfo.IdUserLogin;
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
                // update memory
                MemorySet.UpdateAndInsertEntity(req);
                // reload memory role
                AppGlobal.InitUserPermission("Tao moi role");
                var result = new RequestErrorCode(true);
                result.ListDataResult.Add(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
            }
            return BadRequest("Unknow");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Role req)
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
                if (!ValidateUpdate(req, out errorCode, out errorMessage))
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetRole(id);
                if (obj == null)
                {
                    return Ok(new RequestErrorCode(false, ErrorCodeEnum.DataNotExist.ToString(), "Khong ton tai"));
                }
                #endregion
                req.Id = obj.Id; // gan lai id de update
                #region Process
                req.UpdatedAt = DateTime.Now;
                req.UpdatedBy = userInfo.IdUserLogin;
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
                Logger.Write(ex.ToString(),true);
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
                #endregion

                #region Check exist
                var obj = MemoryInfo.GetRole(id);
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
                // reload memory role
                AppGlobal.InitUserPermission("Tao moi role");
                var result = new RequestErrorCode(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(),true);
            }
            return BadRequest("Unknow");
        }

        #region Validation
        private bool Validate(Role obj, out string errorCode, out string errorMess)
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

        private bool ValidateUpdate(Role obj, out string errorCode, out string errorMess)
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


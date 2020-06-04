﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.ReaderDatabase;
using ElectricShop.Utils;

namespace ElectricShop.Controllers
{
    public class CustomerController : ApiController
    {
        
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
                
                var lstData = MemoryInfo.GetAllCustomer();
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.AddRange(lstData);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }

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
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }

        public async Task<IHttpActionResult> Post([FromBody]Customer req)
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

                #region Tạo key

                if (MemoryInfo.IsExistCustomer(req.Id))
                {
                    errorCode = ErrorCodeEnum.DataIsExist.ToString();
                    errorMessage = "Dữ liệu đã tồn tại";
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Process
                // update memory
                MemorySet.UpdateAndInsertEntity(req);
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
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

        public async Task<IHttpActionResult> Put([FromBody]Customer req)
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

                #region Tạo key

                if (MemoryInfo.IsExistCustomer(req.Id))
                {
                    errorCode = ErrorCodeEnum.DataIsExist.ToString();
                    errorMessage = "Dữ liệu đã tồn tại";
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion

                #region Process
                // update memory
                MemorySet.UpdateAndInsertEntity(req);
                UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                var lstCommand = new List<EntityCommand>();
                lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(req), EntityAction = EntityAction.Insert });
                bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                if (!isOkDone)
                {
                    return Ok(new RequestErrorCode(false, errorCode, errorMessage));
                }
                #endregion
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

        private bool Validate(Customer customer, out string errorCode, out string errorMess)
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

using System;
using System.Net;
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
	public class ImportReceiptController: ApiController
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
				var lstData = MemoryInfo.GetAllImportReceipt();
                var lstResponse = new List<ImportReceiptResponse>();
			    foreach (var importReceipt in lstData)
			    {
			        // lay them du lieu
			        var provider = MemoryInfo.GetProvider(importReceipt.IdProvider);
			        var employee = MemoryInfo.GetUserInfo(importReceipt.IdEmployee);
			        string providerName = provider?.Name;
			        string employeeName = employee?.Name;
                    lstResponse.Add(new ImportReceiptResponse
                    {
                        CreatedAt = importReceipt.CreatedAt,
                        CreatedBy = importReceipt.CreatedBy,
                        Date = importReceipt.Date,
                        EmployeeName = employeeName,
                        Id = importReceipt.Id,
                        IdEmployee = importReceipt.IdEmployee,
                        IdProvider = importReceipt.IdProvider,
                        ListProductId = importReceipt.ListProductId,
                        ProviderName = providerName,
                        TotalPrice = importReceipt.TotalPrice
                    });
			    }
				var res = new RequestErrorCode(true, null, null);
				res.ListDataResult.AddRange(lstResponse);
				return Ok(res);
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
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
				var data = MemoryInfo.GetImportReceipt(id);
			    if (data == null)
			        return Ok(new RequestErrorCode(true, null, null));
			    // lay them du lieu
			    var provider = MemoryInfo.GetProvider(data.IdProvider);
			    var employee = MemoryInfo.GetUserInfo(data.IdEmployee);
			    string providerName = provider?.Name;
			    string employeeName = employee?.Name;
                var res = new RequestErrorCode(true, null, null);
                res.ListDataResult.Add(new ImportReceiptResponse
			    {
			        CreatedAt = data.CreatedAt,
			        CreatedBy = data.CreatedBy,
			        Date = data.Date,
			        EmployeeName = employeeName,
			        Id = data.Id,
			        IdEmployee = data.IdEmployee,
			        IdProvider = data.IdProvider,
			        ListProductId = data.ListProductId,
			        ProviderName = providerName,
			        TotalPrice = data.TotalPrice
			    });
				return Ok(res);
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
			}
			return BadRequest("Unknow");
		}

		[EnableCors(origins: "*", headers: "*", methods: "*")]
		public async Task<IHttpActionResult> Post([FromBody]ImportReceipt req)
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

				#region Validate
				if (!Validate(req, out errorCode, out errorMessage))
				{
					return Ok(new RequestErrorCode(false, errorCode, errorMessage));
				}
				#endregion

				#region Tạo key
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
		public async Task<IHttpActionResult> Put(int id,[FromBody]ImportReceipt req)
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

				#region Validate
				if (!ValidateUpdate(req, out errorCode, out errorMessage))
				{
					return Ok(new RequestErrorCode(false, errorCode, errorMessage));
				}
				#endregion

				#region Check exist
				var obj = MemoryInfo.GetImportReceipt(id);
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
				var obj = MemoryInfo.GetImportReceipt(id);
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
		private bool Validate(ImportReceipt obj, out string errorCode, out string errorMess)
		{
			errorCode = null;
			errorMess = null;
			try
			{
			    if (obj == null)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        return false;
			    }

			    if (obj.Date == null || obj.Date == DateTime.MinValue || obj.Date == DateTime.MaxValue)
			    {
			        errorCode = ErrorCodeEnum.Error_DateIsNull.ToString();
			        errorMess = "Date is not null";
			        return false;
                }

			    if (obj.IdEmployee == 0)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "IdEmployee  is not null";
			        return false;
                }
			    if (obj.IdProvider == 0)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "IdProvider  is not null";
			        return false;
                }
			    if (string.IsNullOrEmpty(obj.ListProductId))
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "ListProductId  is not null";
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

		private bool ValidateUpdate(ImportReceipt obj, out string errorCode, out string errorMess)
		{
			errorCode = null;
			errorMess = null;
			try
			{
			    if (obj == null)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        return false;
			    }

			    if (obj.Date == null || obj.Date == DateTime.MinValue || obj.Date == DateTime.MaxValue)
			    {
			        errorCode = ErrorCodeEnum.Error_DateIsNull.ToString();
			        errorMess = "Date is not null";
			        return false;
			    }

			    if (obj.IdEmployee == 0)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "IdEmployee  is not null";
			        return false;
			    }
			    if (obj.IdProvider == 0)
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "IdProvider  is not null";
			        return false;
			    }
			    if (string.IsNullOrEmpty(obj.ListProductId))
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
			        errorMess = "ListProductId  is not null";
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


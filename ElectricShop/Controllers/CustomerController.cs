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
	public class CustomerController: ApiController
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
				var lstData = MemoryInfo.GetAllCustomer();
				var res = new RequestErrorCode(true, null, null);
				res.ListDataResult.AddRange(lstData);
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
				var data = MemoryInfo.GetCustomer(id);
				var res = new RequestErrorCode(true, null, null);
				res.ListDataResult.Add(data);
				return Ok(res);
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
			}
			return BadRequest("Unknow");
		}

	    [EnableCors(origins: "*", headers: "*", methods: "*")]
		public async Task<IHttpActionResult> Get(string phone)
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
				if (string.IsNullOrWhiteSpace(token) || !TokenManager.ValidateToken(token, out userInfo) || string.IsNullOrEmpty(phone))
				{
					return StatusCode(HttpStatusCode.Unauthorized);
				}
				#endregion
				var data = MemoryInfo.GetAllCustomer();
			    var response = data.Find(x => !string.IsNullOrEmpty(x.Phone) && x.Phone == phone);
				var res = new RequestErrorCode(true, null, null);
				res.ListDataResult.Add(response);
				return Ok(res);
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
			}
			return BadRequest("Unknow");
		}

		[EnableCors(origins: "*", headers: "*", methods: "*")]
		public async Task<IHttpActionResult> Post([FromBody]Customer req)
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
		public async Task<IHttpActionResult> Put(int id,[FromBody]Customer req)
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
				var obj = MemoryInfo.GetCustomer(id);
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
				var obj = MemoryInfo.GetCustomer(id);
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
		private bool Validate(Customer obj, out string errorCode, out string errorMess)
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

			    if (obj.Email == null || !CheckUtils.ValidateEmail(obj.Email))
			    {
			        errorMess = "Email khong hop le";
			        errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
			        return false;
                }

			    if (obj.Phone == null || !CheckUtils.CheckValidMobile(obj.Phone))
			    {
			        errorMess = "Phone khong hop le";
			        errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
			        return false;
                }

			    if (obj.Name == null)
			    {
			        errorMess = "Ten khach hang khong duoc de trong";
			        errorCode = ErrorCodeEnum.Error_CustomerNameIsNull.ToString();
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

		private bool ValidateUpdate(Customer obj, out string errorCode, out string errorMess)
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

			    if (obj.Email == null || !CheckUtils.ValidateEmail(obj.Email))
			    {
			        errorMess = "Email khong hop le";
			        errorCode = ErrorCodeEnum.ErrorEmailFormat.ToString();
			        return false;
			    }

			    if (obj.Phone == null || !CheckUtils.CheckValidMobile(obj.Phone))
			    {
			        errorMess = "Phone khong hop le";
			        errorCode = ErrorCodeEnum.ErrorPhoneFormat.ToString();
			        return false;
			    }

			    if (obj.Name == null)
			    {
			        errorMess = "Ten khach hang khong duoc de trong";
			        errorCode = ErrorCodeEnum.Error_CustomerNameIsNull.ToString();
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


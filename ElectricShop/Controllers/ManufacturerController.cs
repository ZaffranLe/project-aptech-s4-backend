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
	public class ManufacturerController: ApiController
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

			    // check role 
			    if (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.ViewListManufacturer))
			    {
			        return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen xem"));
			    }
                var lstData = MemoryInfo.GetAllManufacturer();
			    List<ManufacturerResponse> lstRes = new List<ManufacturerResponse>();
			    foreach (var manufacturer in lstData)
			    {
			        lstRes.Add(new ManufacturerResponse
                    {
			            Manufacturer = manufacturer,
			            ListImages = ImagesUtils.GetImagesUrl(manufacturer.ImageId)
			        });
			    }
			    var res = new RequestErrorCode(true, null, null);
			    res.ListDataResult.AddRange(lstRes);
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
				var data = MemoryInfo.GetManufacturer(id);
			    List<ManufacturerResponse> lstRes = new List<ManufacturerResponse>();
			    lstRes.Add(new ManufacturerResponse
			    {
			        Manufacturer = data,
			        ListImages = ImagesUtils.GetImagesUrl(data.ImageId)
			    });
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
		public async Task<IHttpActionResult> Post([FromBody]Manufacturer req)
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
			    // check role 
			    if (!Operator.HasPermision(userInfo.IdUserLogin, RoleDefinitionEnum.CreateManufacturer))
			    {
			        return Ok(new RequestErrorCode(false, ErrorCodeEnum.Error_NotHavePermision.ToString(), "Khong co quyen tao"));
			    }
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
			    if (string.IsNullOrEmpty(req.Name))
			    {
			        req.Name = $"ImportOrder{newKey}";
			    }
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
		public async Task<IHttpActionResult> Put(int id,[FromBody]Manufacturer req)
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
				var obj = MemoryInfo.GetManufacturer(id);
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
				var obj = MemoryInfo.GetManufacturer(id);
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
		private bool Validate(Manufacturer obj, out string errorCode, out string errorMess)
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

            }
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
				throw;
			}
			return true;
		}

		private bool ValidateUpdate(Manufacturer obj, out string errorCode, out string errorMess)
		{
			errorCode = null;
			errorMess = null;
			try
			{
			    if (obj == null || string.IsNullOrEmpty(obj.Name))
			    {
			        errorCode = ErrorCodeEnum.DataInputWrong.ToString();
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


using System;using System.Collections.Generic;
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
	public class RoleController: ApiController
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
				    return StatusCode(HttpStatusCode.Unauthorized);
                }
                #endregion
			    var lstPermission = MemoryInfo.GetListPermission(userInfo.IdUserLogin);
                var res = new RequestErrorCode(true, null, null);
				res.ListDataResult.AddRange(lstPermission);
				return Ok(res);
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
			}
			return BadRequest("Unknow");
		}
	}
}


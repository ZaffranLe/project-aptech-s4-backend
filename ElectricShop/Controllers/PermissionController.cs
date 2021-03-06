using System;using System.Collections.Generic;
using System.Linq;
using System.Net;
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
	public class PermissionController: ApiController
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

		        var lstPermission = MemoryInfo.GetAllPermission();
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


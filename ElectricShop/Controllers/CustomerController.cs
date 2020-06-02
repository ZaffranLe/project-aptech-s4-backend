using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ElectricShop.Const;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Controllers
{
    public class CustomerController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody]Customer req)
        {
            try
            {
               

                return Ok(req);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            Logger.Write("--------------------ErrorCodeEnum.Unknow---------------------------------");
            return BadRequest("Unknow");
        }
    }
}

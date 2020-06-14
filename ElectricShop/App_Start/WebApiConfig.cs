using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;

namespace ElectricShop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.EnableCors();
            // Web API configuration and services
            if (!AppGlobal.InitConfig())
            {
                Logger.Write("Khong init duoc config!",true);
                return;
            }
            if (!AppGlobal.InitMemory())
            {
                Logger.Write("Khong init duoc du lieu!");
                return;
            }
            if (!AppGlobal.InitUserPermission("Init Start Services"))
            {
                Logger.Write("Khong init duoc permission!");
                return;
            }
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

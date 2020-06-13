using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.ReaderDatabase;

namespace ElectricShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var entityQry = new EntityQuery
            {
                EntityName = Customer.EntityName(),
                QueryAction = EntityGet.GetAllValues
            };
            entityQry = ProcessReadDatabaseUtils.GetEntityQuery(entityQry);
            var listValue = new List<Customer>();
            if (entityQry.ReturnValue != null)
            {
                listValue = entityQry.ReturnValue.Select(baseEntity =>
                    baseEntity.GetEntity() as Customer).ToList();
            }
            return View();
        }
    }
}

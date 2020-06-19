using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BaseUtils;
using ElectricShop.Common.Enum;
using ElectricShop.DatabaseDAL.Common;
using ElectricShop.Entity;
using ElectricShop.Entity.Entities;
using ElectricShop.Memory;
using ElectricShop.Models;
using ElectricShop.Utils;
namespace ElectricShop.Controllers
{
    public class ConfirmOrderController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage  Get(string req)
        {
            try
            {
                var path = AppGlobal.GetAppPath() + AppGlobal.DEFAULT_FOLDER_CONFIG + "TemplateConfirmOrder.html";
                string Html = File.ReadAllText(path);
                if (req == null)
                {
                    Html = Html.Replace("#title", "Rất tiếc!");
                    Html = Html.Replace("#content", "Đã xảy ra sự cố trong quá trình tự động xác thực đơn hàng vui long liên hệ quản trị viên để xử lí.");
                }
                else
                {
                    var cfOrderObj = EmailUtils.StringToConfirmOrder(req);
                    if (cfOrderObj == null)
                    {
                        Html = Html.Replace("#title", "Rất tiếc!");
                        Html = Html.Replace("#content", "Đã xảy ra sự cố trong quá trình tự động xác thực đơn hàng vui long liên hệ quản trị viên để xử lí.");
                    }
                    else
                    {
                        var orderDetail = MemoryInfo.GetOrderDetail(cfOrderObj.OrderId);
                        if (orderDetail == null)
                        {
                            Html = Html.Replace("#title", "Rất tiếc!");
                            Html = Html.Replace("#content", "Đã xảy ra sự cố trong quá trình tự động xác thực đơn hàng vui long liên hệ quản trị viên để xử lí.");
                        }
                        else
                        {
                            // xu li cap nhap trang thai lenh
                            orderDetail.OrderStatus = OrderStatus.Approval.ToString();
                            UpdateEntitySql updateEntitySql = new UpdateEntitySql();
                            var lstCommand = new List<EntityCommand>();
                            lstCommand.Add(new EntityCommand { BaseEntity = new Entity.Entity(orderDetail), EntityAction = EntityAction.Update });
                            bool isOkDone = updateEntitySql.UpdateDefault(lstCommand);
                            if (!isOkDone)
                            {
                                Html = Html.Replace("#title", "Rất tiếc!");
                                Html = Html.Replace("#content", "Đã xảy ra sự cố trong quá trình tự động xác thực đơn hàng vui long liên hệ quản trị viên để xử lí.");
                            }
                            else
                            {
                                // update memory
                                MemorySet.UpdateAndInsertEntity(orderDetail);
                                Html = Html.Replace("#title", "Tuyệt vời!");
                                Html = Html.Replace("#content", "Xác nhận đơn hàng thành công");
                            }
                        }
                    }
                }
                StringBuilder sb = new StringBuilder(Html);
                var response = new HttpResponseMessage();
                response.Content = new StringContent(sb.ToString());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                response.ReasonPhrase = "success";
                // what code should i write here
                return response;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
            }

            return null;
        }

       

    }
}


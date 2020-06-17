using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NLog;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ElectricShop.Common.Config;
using ElectricShop.Entity.Entities;
using ElectricShop.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace ElectricShop
{
    public static class EmailUtils
    {
        private static ILogger _logger;
        private const string URL = "https://api.telegram.org/bot1294006627:AAGm4Y4UluShkNrCTkMfPWKoni62Cihaxew/sendMessage"; // token telegram
        private static readonly HttpClient client = new HttpClient();
        

        public static IRestResponse SendEmailNewOrder(OrderDetail orderDetail, Customer customer)
        {
            try
            {
                var path = AppGlobal.GetAppPath() + AppGlobal.DEFAULT_FOLDER_CONFIG + "TemplateEmail.html";
                if (!File.Exists(path))
                {
                    Logger.Write("Khong tim thay file emailTemplate", true);
                    throw new Exception("Khong tim thay file emailTemplate");
                }
                var pathBaoGia = AppGlobal.GetAppPath() + AppGlobal.DEFAULT_FOLDER_CONFIG + "TemplateBaogia.html";
                if (!File.Exists(pathBaoGia))
                {
                    Logger.Write("Khong tim thay file TemplateBaogia", true);
                    throw new Exception("Khong tim thay file TemplateBaogia");
                }

                var customerName = customer.Name ?? "Khách hàng";
                var fullText = File.ReadAllText(path);
                var fullTextBaoGia = File.ReadAllText(pathBaoGia);

                #region tao content bao gia

                 #endregion
                fullText = fullText.Replace("#Username", customerName);// ten khách hàng
                fullText = fullText.Replace("#timeorder", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")); // ngay dat
                fullText = fullText.Replace("#name", customerName);
                fullText = fullText.Replace("#phone", customer.Phone);
                fullText = fullText.Replace("#address", customer.Address);
                fullText = fullText.Replace("#contenttable", fullTextBaoGia);
                fullText = fullText.Replace("#totalmoney", "1000000");

                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api",
                        "6f1ba9d91af88c5aebb8ae1723ce66b1-a2b91229-123ec25c");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "musickid.vn", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "ElectricShop <Electric@musickid.vn>");
                request.AddParameter("to", customer.Email);
                request.AddParameter("subject", "Xác nhận đơn hàng");
                request.AddParameter("html", fullText);
                request.Method = Method.POST;
                return client.Execute(request);
            }
            catch (Exception e)
            {
                Logger.Write("Loi gui mail xac nhan don hang", true);
                Logger.Write(e.ToString(), true);
                throw;
            }
        }

    }
}
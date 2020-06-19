using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using NLog;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ElectricShop.Common.Config;
using ElectricShop.Entity.Entities;
using ElectricShop.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ElectricShop
{
    public static class EmailUtils
    {

        public static IRestResponse SendEmailNewOrder(OrderDetail orderDetail, Customer customer, Dictionary<int, Product> dicProductCount)
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
                StringBuilder tableContent = new StringBuilder();
                // tao table content
                int stt = 1;
                decimal totalPrice = 0;
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                foreach (var product in dicProductCount)
                {
                    string content = fullTextBaoGia.Replace("#stt", stt.ToString());
                    content = content.Replace("#productname", product.Value.Name);
                    content = content.Replace("#quantity", product.Key.ToString());
                    decimal unitPrice = product.Value.UnitPrice.HasValue ? product.Value.UnitPrice.Value : 0;
                    content = content.Replace("#cost", unitPrice.ToString("#,###", cul.NumberFormat));
                    decimal price = product.Key *  unitPrice;
                    totalPrice += price;
                    content = content.Replace("#money", price.ToString("#,###", cul.NumberFormat));
                    tableContent.Append(content);
                    stt++;
                }

                #region tao content bao gia

                 #endregion
                fullText = fullText.Replace("#Username", customerName);// ten khách hàng
                fullText = fullText.Replace("#timeorder", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")); // ngay dat
                fullText = fullText.Replace("#name", customerName);
                fullText = fullText.Replace("#phone", customer.Phone);
                fullText = fullText.Replace("#address", customer.Address);
                fullText = fullText.Replace("#contenttable", tableContent.ToString());
                fullText = fullText.Replace("#totalmoney", totalPrice.ToString("#,###", cul.NumberFormat));

                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api",AppGlobal.ElectricConfig.MailGunToken);
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

        public static string ObjectToString(object obj)
        {
            try
            {
                var stringJson = JsonConvert.SerializeObject(obj);
                JsonSerializerSettings theJsonSerializerSettings = new JsonSerializerSettings();

                theJsonSerializerSettings.TypeNameHandling = TypeNameHandling.None;
                return JsonConvert.SerializeObject(stringJson, theJsonSerializerSettings);
               
            }
            catch (Exception ex)
            {
                Logger.Write("Đã xảy ra lỗi trong quá trình mã hóa object to string: " +ex, true);
                throw;
            }
            return null;
        }

        public static object StringToObject(string base64String)
        {
            try
            {
                JsonSerializerSettings theJsonSerializerSettings = new JsonSerializerSettings();

                theJsonSerializerSettings.TypeNameHandling = TypeNameHandling.None;
                return JsonConvert.DeserializeObject<object>(base64String, theJsonSerializerSettings);
            }
            catch (Exception e)
            {
                Logger.Write("Đã xảy ra lỗi trong quá trình giải mã  string to object: " + e, true);
                throw;
            }
            return null;
        }
    }
}
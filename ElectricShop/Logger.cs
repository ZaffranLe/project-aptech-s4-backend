using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NLog;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElectricShop.Models;

namespace ElectricShop
{
    public class Logger
    {
        private static ILogger _logger;
        private const string URL = "https://api.telegram.org/bot1294006627:AAGm4Y4UluShkNrCTkMfPWKoni62Cihaxew/sendMessage"; // token telegram
        private static readonly HttpClient client = new HttpClient();
        

        public static async Task Write(string msg, bool isError = false)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                if (_logger == null)
                    _logger = LogManager.GetCurrentClassLogger();
                if (isError)
                {
                    if (AppGlobal.ElectricConfig == null || AppGlobal.ElectricConfig.IsSendLogToBot)
                    {
                        var values = new Dictionary<string, string>
                        {
                            { "chat_id", "-440392150" },
                            { "text", msg }
                        };

                        var content = new FormUrlEncodedContent(values);

                        var response = await client.PostAsync(URL, content);

                        var responseString = await response.Content.ReadAsStringAsync();
                    }
                    _logger.Error(msg);
                } 
                else
                    _logger.Info(msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
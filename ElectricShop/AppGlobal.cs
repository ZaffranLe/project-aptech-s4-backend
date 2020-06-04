using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Anotar.NLog;
using Antlr.Runtime.Misc;
using ElectricShop.Common.Config;
using ElectricShop.Entity;
using ElectricShop.Memory;
using ElectricShop.ReaderDatabase;
using Newtonsoft.Json;

namespace ElectricShop
{
    public class AppGlobal
    {
        private const string EXTENSION_FILE_NAME = ".qes"; // quant edge setting
        private const string DEFAULT_FOLDER_CONFIG = @"\Config\";
        public static ElectricConfig ElectricConfig { get; set; }
        public static bool InitMemory()
        {
            try
            {
                var lstEntityName = Memory.Memory.GetListEntityNameInit();
                List<EntityQuery> lstEntityQuery = lstEntityName.Select(x => EntityQuery.RequestAllByName(x)).ToList();
                lstEntityQuery = ProcessReadDatabaseUtils.GetListEntityQuery(lstEntityQuery);
                foreach (var entityQuery in lstEntityQuery)
                {
                    if (entityQuery.ReturnValue != null)
                    {
                        foreach (var entity in entityQuery.ReturnValue)
                        {

                            MemorySet.UpdateAndInsertEntity(entity.GetEntity());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString());
                return false;
            }
            return true;
        }

        public static bool InitConfig()
        {
            try
            {
                #region Lay WebapiCnf
                var path = GetAppPath() + DEFAULT_FOLDER_CONFIG + ElectricConfig.FileName() + EXTENSION_FILE_NAME;
                if (!File.Exists(path))
                {
                    var strMsg = "Not found file in path:" + path;
                    Logger.Write(strMsg);
                    throw new Exception(strMsg);
                }
                else Logger.Write("path  CashTranferWebAPi : " + path,true);
                var fullText = File.ReadAllText(path);



                ElectricConfig = JsonConvert.DeserializeObject<ElectricConfig>(fullText);
                if (ElectricConfig == null)
                {
                    Logger.Write("Not get ElectricConfig",true);
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
                Logger.Write("Get config CashTranferWebAPi success!");
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return false;
        }
        public static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.AppDomain.CurrentDomain.BaseDirectory; //Dung iss phai lay local path qua func nay
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }
    }
}
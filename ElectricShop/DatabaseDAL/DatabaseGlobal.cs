using Anotar.NLog;
using ElectricShop.Common;
using ElectricShop.DatabaseDAL.Common;

namespace ElectricShop.DatabaseDAL
{
    public class DatabaseGlobal
    {
        public static void ReInit()
        {
                CommonGlobalConfig.IsUseMySql = false; //Mac dinh dung Sql Server

            LogTo.Info("CommonGlobalConfig.IsUseMySql = " + CommonGlobalConfig.IsUseMySql);
        }
        public static bool IsUseMySql()
        {
            return CommonGlobalConfig.IsUseMySql;
        }

        public static string GetDatabaseNameWorking(bool isHist, bool isReport)
        {
            return EntityManager.Instance.GetNameDataWorking();
        }
    }
}

namespace CommonicationMemory.Config
{
    public class SettingConfig : IConfig
    {
        public string DataBase_SqlServer { get; set; }
        public string Database_Catalog { get; set; }
        public bool Database_SqlAuthentication { get; set; }
        public string Database_UserName { get; set; }
        public string Database_Password { get; set; }
        public string Setting_ProjectName { get; set; }
        public string Setting_ClassPrefix { get; set; }
        public string Setting_SpPrefix { get; set; }
        public string Setting_OutputDirectory { get; set; }
        public string Setting_FoudationLink { get; set; }
        public string Setting_MemoryWorkerBase { get; set; }
        public bool Setting_CheckGenByForder { get; set; }
        public string Setting_ConnectionString { get; set; }
        public string Setting_EntityPath { get; set; }

        public void Dispose()
        {
        }

        public static string FileName()
        {
            return typeof(SettingConfig).Name;
        }

        public string GetFileName()
        {
            return FileName();
        }

        

    }
}

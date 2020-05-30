namespace ElectricShop.Common.Config
{
    public class DatabaseConfig : IConfig
    {
        public string ConnectionString { get; set; }

        public void Dispose()
        {
            ConnectionString = null;
        }

        public static string FileName()
        {
            return typeof(DatabaseConfig).Name;
        }

        public string GetFileName()
        {
            return FileName();
        }

    }
}

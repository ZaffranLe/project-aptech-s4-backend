namespace ElectricShop.Common.Config
{
    public class ElectricConfig : IConfig
    {
        public string Secret { get; set; }
        public int Port { get; set; }
        public bool SendConfirmEmail { get; set; }

        public void Dispose()
        {
            Secret = null;
        }

        public static string FileName()
        {
            return typeof(ElectricConfig).Name;
        }

        public string GetFileName()
        {
            return FileName();
        }

    }
}

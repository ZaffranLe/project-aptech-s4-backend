namespace CommonicationMemory.Resources
{
    public class ServerInfo
    {
        private const string SERVER_NAME = "Visiontriding Database";
        private const string DB_CONFIG_DEFAULT =
            "Data Source=.;Initial Catalog=Exchange;User ID=lemon;Password=admin;Persist Security Info=True";

        private readonly object _lock = new object();
        private string _connectionString;

        public ServerInfo()
        {
            _connectionString = string.Empty;
        }

        public static ServerInfo Instance
        {
            get { return Nested.Instance; }
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly ServerInfo Instance = new ServerInfo();
        }

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                _connectionString = LoadFromFile();
            return _connectionString;
        }

        private string LoadFromFile()
        {
            try
            {
                lock (_lock)
                {
                    string path = ServerConfig.GetDBConfigPath();
                    return DBConfigUtils.GetDBConnectionString(path, SERVER_NAME);
                }
            }
            catch { }
            return DB_CONFIG_DEFAULT;
        }
    }
}
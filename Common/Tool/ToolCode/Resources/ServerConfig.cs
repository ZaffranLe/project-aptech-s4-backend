using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace CommonicationMemory.Resources
{
    public class ServerConfig
    {
        #region fields

        public const string DEFAULT_PATH = @"C:\QuantEdge\";
        private const string FILE_PATH = @"\Config\ServerConfig.xml";
        private const string DB_CONFIG_FILE = @"DBConfig.xml";
        private const string EXCHANGE_CONFIG_FILE = @"ExchangeConfig.xml";
        private const string BROKER_CONFIG_FILE = @"BrokerConfig.xml";

        private const string XML_NODE_CONFIG = "Config";
        private const string XML_NODE_SYSTEM = "System";
        private const string XML_NODE_NAME = "SystemName";
        private const string XML_NODE_TYPE = "Type";
        private const string XML_NODE_CLIENT = "Client";
        private const string XML_NODE_VERSION = "Version";
        private const string XML_NODE_SYSTEM_PATH = "SystemPath";
        private const string XML_NODE_EXPIRED = "Expired";
        private static readonly ILog log = LogManager.GetLogger(typeof (ServerConfig));

        private static string KEY =
            "License|QuantEdgeProvider;System|{0};SystemName|{1};Type|{2};Client|{3};Version|{4};SystemPath|{5};Expired|{6}";

        private static string FORMAT = "dd/MM/yyyy";
        internal static string DEFAULT_LICENSE_FILE = @"License.lic";

        private static string _system;
        private static string _systemName;
        private static string _systemType;
        private static string _version;
        private static string _client;
        private static DateTime _expireDate = DateTime.Now.Date;

        #endregion

        #region properties

        private static string _systemPath;

        public static string Client
        {
            get { return _client; }
        }

        public string System
        {
            get { return _system; }
        }

        public string SystemName
        {
            get { return _systemName; }
        }

        public string SystemType
        {
            get { return _systemType; }
        }

        public string Version
        {
            get { return _version; }
        }

        public string SystemPath
        {
            get { return _systemPath; }
        }

        public DateTime ExpireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }

        public string Date
        {
            get { return _expireDate.ToString(FORMAT); }
        }

        #endregion

        #region ctor

        public ServerConfig()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            _system = string.Empty;
            _systemName = string.Empty;
            _systemType = string.Empty;
            _version = string.Empty;
            _systemPath = DEFAULT_PATH;
            _expireDate = DateTime.Now.Date;
        }

        #endregion

        #region methods

        private static bool IsExpriredDate()
        {
            if (_expireDate.Date > DateTime.Now.Date)
                return false;
            return true;
        }

        private static bool CommpareData(List<KeyValue> list)
        {
            foreach (KeyValue keyValue in list)
            {
                switch (keyValue.Key)
                {
                    case XML_NODE_SYSTEM:
                        if (!keyValue.Value.Equals(_system))
                            return false;
                        break;
                    case XML_NODE_NAME:
                        if (!keyValue.Value.Equals(_systemName))
                            return false;
                        break;
                    case XML_NODE_TYPE:
                        if (!keyValue.Value.Equals(_systemType))
                            return false;
                        break;
                    case XML_NODE_CLIENT:
                        if (!keyValue.Value.Equals(_client))
                            return false;
                        break;
                    case XML_NODE_VERSION:
                        if (!keyValue.Value.Equals(_version))
                            return false;
                        break;
                    case XML_NODE_SYSTEM_PATH:
                        if (!keyValue.Value.Equals(_systemPath))
                            return false;
                        break;
                    case XML_NODE_EXPIRED:
                        _expireDate = DateTime.ParseExact(keyValue.Value, FORMAT, CultureInfo.InvariantCulture);
                        break;
                }
            }
            return true;
        }

        private static void LoadFromFile()
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().Location;
                path = path.Remove(path.LastIndexOf('\\')) + FILE_PATH;
                if (File.Exists(path))
                {
                    XmlDocument xmlDoc = XmlUtils.LoadXmlFile(path);
                    ReadXML(xmlDoc);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        private static KeyValue GetValue(string keyvalue)
        {
            try
            {
                string[] arr = keyvalue.Split('|');
                if (arr.Length < 2) return null;
                var value = new KeyValue();
                value.Key = arr[0];
                value.Value = arr[1];
                return value;
            }
            catch
            {
            }
            return null;
        }

        private static bool ReadXML(XmlDocument xmlDoc)
        {
            try
            {
                if (xmlDoc != null)
                {
                    XmlNode cfgNode = xmlDoc.SelectSingleNode(XML_NODE_CONFIG);
                    if (cfgNode != null)
                    {
                        XmlNode node = cfgNode.SelectSingleNode(XML_NODE_SYSTEM);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _system = node.InnerText.Trim();

                        node = cfgNode.SelectSingleNode(XML_NODE_NAME);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _systemName = node.InnerText.Trim();

                        node = cfgNode.SelectSingleNode(XML_NODE_TYPE);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _systemType = node.InnerText.Trim();

                        node = cfgNode.SelectSingleNode(XML_NODE_CLIENT);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _client = node.InnerText.Trim();

                        node = cfgNode.SelectSingleNode(XML_NODE_VERSION);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _version = node.InnerText.Trim();

                        node = cfgNode.SelectSingleNode(XML_NODE_SYSTEM_PATH);
                        if (node != null && !string.IsNullOrEmpty(node.InnerText)) _systemPath = node.InnerText.Trim();
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public static string GetServerPath()
        {
            try
            {
                LoadFromFile();
                if (!string.IsNullOrEmpty(_systemPath))
                    return _systemPath;
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return DEFAULT_PATH;
        }

        public static string GetLicenseFilePath()
        {
            try
            {
                LoadFromFile();
                if (!string.IsNullOrEmpty(_systemPath))
                    return _systemPath + DEFAULT_LICENSE_FILE;
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return DEFAULT_PATH + DEFAULT_LICENSE_FILE;
        }
        
        public static string GetDBConfigPath()
        {
            return GetServerPath() + DB_CONFIG_FILE;
        }

        public static string GetExchangeConfigPath()
        {
            return GetServerPath() + EXCHANGE_CONFIG_FILE;
        }

        public static string GetBrokerConfigPath()
        {
            return GetServerPath() + BROKER_CONFIG_FILE;
        }

        public bool LoadFromFile(string path)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                return ReadXML(xmlDoc);
            }
            catch
            {
            }
            return false;
        }

        public string GetKey()
        {
            return string.Format(KEY, _system, _systemName, _systemType, _client, _version, _systemPath, Date);
        }

        public static List<KeyValue> LoadFromString(string value)
        {
            var list = new List<KeyValue>();
            try
            {
                string[] arr = value.Split(';');
                if (arr.Length < 7) return list;
                KeyValue key;
                for (int i = 1; i < arr.Length; i++)
                {
                    key = GetValue(arr[i]);
                    if (key != null)
                        list.Add(key);
                }
            }
            catch
            {
            }
            return list;
        }

        public static ServerConfig CheckValidDataFile(string path)
        {
            var fileConfig = new ServerConfig();
            return fileConfig.LoadFromFile(path) ? fileConfig : null;
        }

        #endregion
    }

    public class KeyValue
    {
        public string Key;
        public string Value;
    }

    public class ServerType
    {
        public const string BROKER_SERVER_GATEWAY = "brokerservergateway";
        public const string EXCHANGE_CONNECTOR = "exchangeconnector";
        public const string PUBLIC_ORDER_GATEWAY = "publicordergateway";
        public const string BANKING_GATEWAY = "bankinggateway";
        public const string HISTORICAL_ENGINE = "historicalengine";
        public const string PHILLIP_FUTURES = "phillipfutures";
        public const string HSBC_CONNECTOR = "hsbcconnector";
        public const string EXCHANGE = "exchange";
        public const string EXCHANGE_GATEWAY = "exchangegateway";
    }
}
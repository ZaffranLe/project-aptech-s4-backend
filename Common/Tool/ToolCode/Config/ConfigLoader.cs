using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using fastJSON;

namespace CommonicationMemory.Config
{
    public class ConfigLoader : IDisposable
    {
        private const string EXTENSION_FILE_NAME = ".qes";// quant edge setting
        private const string DEFAULT_FOLDER_CONFIG = @"\Config\";
        public string Path { get; private set; }
        public Type ConfigType { get; private set; }
        private bool _useExtensions { get; set; } //Set gia tri nay cho da thua ke

        public event EventHandler ConfigFileChanged;
        private FileSystemWatcher _watcher;
        private bool _requireEncrypt = true;
        public IConfig Config { get; set; }

        public ConfigLoader(Type configType, bool require = true, bool useExtension = true)
        {
            ConfigType = configType;
            _requireEncrypt = require;
            _useExtensions = useExtension;
            ExtractPath();
            _watcher = new FileSystemWatcher
            {
                Path = Path.Remove(Path.LastIndexOf('\\')),
                /* Watch for changes in file only. */
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = Config.GetFileName() + EXTENSION_FILE_NAME,  // Only watch config file.
                EnableRaisingEvents = true,
            };
            // Add event handlers.
            _watcher.Changed += ProcessOnChanged;
        }

        private void ExtractPath()
        {
            if (ConfigType == null)
                throw new Exception("Null type");

            var path = GetAppPath() + DEFAULT_FOLDER_CONFIG + ConfigType.Name + EXTENSION_FILE_NAME;
            if (!File.Exists(path))
                throw new Exception("Not found file in path:" + path);
            var file = new FileInfo(path);
            Path = file.FullName;
            LoadConfig();
        }

        private static string GetAppPath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf('\\'));
            return path;
        }

        private bool LoadConfig()
        {
            try
            {
                var fullText = File.ReadAllText(Path);

#if !DEBUG
                if (_requireEncrypt)
                    fullText = EncryptUtils.Decrypt(fullText);
#endif

                JSON.Parameters = new JSONParameters()
                {
                    UsingGlobalTypes = false,//khong con toi uu hoa phan thuc hien chuyen du lieu
                    UseExtensions = _useExtensions, //Set gia tri nay cho da thua ke (mặc định true)
                    UseUTCDateTime = false,//mac dinh thoi gian dang set la UTC nen neu set lan nua thi se mat gia tri
                    SerializeNullValues = false, //khong serialize gia tri = null de giam thieu kich thuoc bo nho
                };
                Config = JSON.ToObject(fullText, ConfigType) as IConfig;
                if (Config != null)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        public bool SaveConfig()
        {
            try
            {
                _watcher.EnableRaisingEvents = false;
                JSON.Parameters = new JSONParameters()
                {
                    UsingGlobalTypes = false, //khong con toi uu hoa phan thuc hien chuyen du lieu
                    UseExtensions = _useExtensions, //Set gia tri nay cho da thua ke
                    UseUTCDateTime = false, //mac dinh thoi gian dang set la UTC nen neu set lan nua thi se mat gia tri
                    SerializeNullValues = false, //khong serialize gia tri = null de giam thieu kich thuoc bo nho
                };
                string fullText = JSON.ToJSON(Config);
                fullText = JSON.Beautify(fullText);
                if (fullText.StartsWith("{") && fullText.EndsWith("}"))
                    _requireEncrypt = false; //File nay dang ko su dung ma hoa
                if (!fullText.StartsWith("{") && !fullText.EndsWith("}"))
                    _requireEncrypt = true; //File nay dang su dung ma hoa

                //if (_requireEncrypt)
                //    fullText = EncryptUtils.Encrypt(fullText);

                File.WriteAllText(Path, fullText);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
            return false;
        }

        public bool SaveConfig(IConfig config)
        {
            try
            {
                Config = config;
                return SaveConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void ProcessOnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                _watcher.EnableRaisingEvents = false;
                //sau khi dung watcher thi se cho cap nhat noi dung
                var ok = LoadConfig();
                if (ok && ConfigFileChanged != null)
                    ConfigFileChanged(this, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        }

        public void Dispose()
        {
            try
            {
                _watcher.EnableRaisingEvents = false;
                _watcher = null;
                Path = null;
                ConfigType = null;
                if (Config != null)
                    Config.Dispose();
                Config = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

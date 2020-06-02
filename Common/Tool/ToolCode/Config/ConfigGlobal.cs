using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonicationMemory.Config
{
    public class ConfigGlobal
    {
        private static ConfigLoader _configLoader = null;
        public static SettingConfig SettingConfig;
        public static void Init()
        {
            _configLoader = new ConfigLoader(typeof(SettingConfig), false);
            SettingConfig = _configLoader.Config as SettingConfig;
        }

        public static void Save()
        {
            _configLoader.SaveConfig(SettingConfig);
        }
    }
}

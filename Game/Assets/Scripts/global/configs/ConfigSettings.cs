using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class ConfigSettings
    {
        public List<ConfigSettingItem> m_settings = new List<ConfigSettingItem>();
        public ConfigSettings()
        {
        }

        public void AddConfig(EnumConfigGenre type, string fileName)
        {
            string strConfigPath = "config/csv/";
            ConfigSettingItem pSettingItem = new ConfigSettingItem(type, strConfigPath + fileName);
            m_settings.Add(pSettingItem);
        }
    }
}

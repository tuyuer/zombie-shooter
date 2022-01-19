using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class ConfigSettingItem
    {
        public EnumConfigGenre m_genre;
        public string m_fileName;

        public ConfigSettingItem(EnumConfigGenre type, string fileName)
        {
            m_genre = type;
            m_fileName = fileName;
        }
    }
}

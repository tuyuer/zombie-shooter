using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy {

    public enum EnumConfigGenre
    {
        ENUM_CONFIG_SOUND,
        ENUM_CONFIG_COUNT,
    };

    public class ConfigManager
    {
        private Dictionary<EnumConfigGenre, CSVFileData> m_configList = new Dictionary<EnumConfigGenre, CSVFileData>();

        private static ConfigManager instance = null;
        public static ConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigManager();
                    instance.Initialize();
                }
                return instance;
            }
        }

        private ConfigManager()
        {

        }

        private void Initialize()
        {
            ConfigSettings pConfigSettings = new ConfigSettings();
            pConfigSettings.AddConfig(EnumConfigGenre.ENUM_CONFIG_SOUND, "sound.csv");
            LoadConfigs(pConfigSettings);
        }

        private void LoadConfigs(ConfigSettings configSettings)
        {
            Debug.Log("ConfigManager loadConfigs !!!");
            int nCount = configSettings.m_settings.Count;
            for (int i = 0; i < nCount; i++)
            {
                ConfigSettingItem pSettingItem = configSettings.m_settings[i];
                Debug.Log("load config : " + pSettingItem.m_fileName + " \n");
                CSVDataTable pDataTable = CSVProvider.ParserCsvWithFile(pSettingItem.m_fileName);
                CSVFileData pCSVFileData = new CSVFileData();
                int nRowCount = pDataTable.rows.Count;
                for (int rowIndex = 0; rowIndex < nRowCount; rowIndex++)
                {
                    CSVDataRow rowData = pDataTable.rows[rowIndex];
                    CSVRow csvRow = CopyRow(pSettingItem.m_genre);
                    csvRow.Parser(rowData, rowIndex);
                    pCSVFileData.AddRow(csvRow);
                }
                m_configList.Add(pSettingItem.m_genre, pCSVFileData);
            }
        }

        public CSVFileData LoadConfig(string strFilePath, CSVRow row)
        {
            CSVDataTable pDataTable = CSVProvider.ParserCsvWithFile(strFilePath);
            CSVFileData pCSVFileData = new CSVFileData();
            int nRowCount = pDataTable.rows.Count;
            for (int rowIndex = 0; rowIndex < nRowCount; rowIndex++)
            {
                CSVDataRow rowData = pDataTable.rows[rowIndex];
                CSVRow csvRow = row.copy();
                csvRow.Parser(rowData, rowIndex);
                pCSVFileData.AddRow(csvRow);
            }
            return pCSVFileData;
        }

        public CSVRow GetConfigByIndex(EnumConfigGenre configType, int index)
        {
            CSVFileData pCsvFileData = m_configList[configType];
            return pCsvFileData.GetRowByIndex(index);
        }

        public CSVRow GetConfigByKey(EnumConfigGenre configType, int keyCol1)
        {
            CSVFileData pCsvFileData = m_configList[configType];
            return pCsvFileData.GetRowByKey(keyCol1);
        }

        public CSVRow GetConfigByKey2(EnumConfigGenre configType, int keyCol1, int keyCol2)
        {
            int keyValue = (keyCol1 << 16) + keyCol2;
            CSVFileData pCsvFileData = m_configList[configType];
            return pCsvFileData.GetRowByKey(keyValue);
        }

        public CSVRow GetConfigByName(EnumConfigGenre configType, string name)
        {
            CSVFileData pCsvFileData = m_configList[configType];
            return pCsvFileData.GetRowByName(name);
        }

        public int GetConfigTypeByName(EnumConfigGenre configType,string name)
        {
            if (name.Length == 0)
                return 0;
            CSVFileData pCsvFileData = m_configList[configType];
            foreach (CSVRow row in pCsvFileData.m_csvRows)
            {
                if (row.m_keyStr.Equals(name))
                {
                    return row.KeyValue();
                }
            }
            return 0;
        }

        public int GetConfigCount(EnumConfigGenre configType)
        {
            CSVFileData pCsvFileData = m_configList[configType];
            if (pCsvFileData == null)
                return 0;
            return pCsvFileData.GetRowCount();
        }

        private CSVRow CopyRow(EnumConfigGenre type)
        {
            CSVRow csvRowRet = null;
            switch (type)
            {
                case EnumConfigGenre.ENUM_CONFIG_SOUND:
                    return new SoundConfig();
                default:
                    break;
            }
            return csvRowRet;
        }
    }

}
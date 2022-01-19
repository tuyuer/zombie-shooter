using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HitJoy
{
    public class SoundConfig : CSVRow
    {
        public int m_nType;
        public string m_strPath;

        public SoundConfig()
        {

        }

        public override void Parser(CSVDataRow rowData, int row)
        {
            m_nType = rowData.GetIntValue("SoundID");
            m_strPath = rowData.GetStringValue("File");
            SetKey(m_nType);
        }  
    }
}

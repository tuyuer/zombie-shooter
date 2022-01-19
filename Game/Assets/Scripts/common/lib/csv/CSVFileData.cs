using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class CSVFileData 
    {
        public List<CSVRow> m_csvRows = new List<CSVRow>();
        public CSVFileData()
        {

        }

        public void AddRow(CSVRow row)
        {
            m_csvRows.Add(row);
        }

        public CSVRow GetRowByIndex(int index)
        {
            return m_csvRows[index];
        }

        public CSVRow GetRowByKey(int keyValue)
        {
            foreach (CSVRow row in m_csvRows)
            {
                if (row.KeyValue() == keyValue)
                    return row;
            }
            return null;
        }

        public CSVRow GetRowByName(string name)
        {
            foreach (CSVRow row in m_csvRows)
            {
                if (row.m_keyStr.Equals(name))
                    return row;
            }
            return null;
        }

        public int GetRowCount()
        {
            return m_csvRows.Count;
        }
    }

}

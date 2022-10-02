using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace HitJoy
{
    public class CSVRow 
    {
        public string m_keyStr;
        private int m_keyInt;
        
        public CSVRow()
        {
            m_keyInt = 0;
        }

        public void SetKey(int key1)
        {
            m_keyInt = key1;
        }

        public void SetKeyStr(string keyStr)
        {
            m_keyStr = keyStr;
        }

        public void SetKey2(int key1,int key2)
        {
            m_keyInt = (key1<<16) + key2;
        }

        public int KeyValue() 
        {
            return m_keyInt;
        }

        public virtual void Parser(CSVDataRow rowData, int row)
        {

        }

        public virtual CSVRow copy()
        {
            Debug.Log("Error!!!!! can't call the base configitem copy.\n");
            return null;
        }

        public string[] parserStringArray(string strArray, int nArraySize)
        {
            if (strArray.Length < 3)
            {
                string[] strValues = new string[nArraySize];
                for(int i=0;i<nArraySize;i++){
                    strValues[i] = "0";
                }
                return strValues;
            }

            if (strArray.Substring(0, 1) != "[" || strArray.Substring(strArray.Length - 1, 1) != "]")
            {
                string[] strValues = new string[nArraySize];
                for(int i=0;i<nArraySize;i++){
                    strValues[i] = "0";
                }
                return strValues;
            }

            string strContent = strArray.Substring(1, strArray.Length - 2);
            string[] strRet = strContent.Split(',');
            if (strRet.Length != nArraySize)
            {
                string[] strValues = new string[nArraySize];
                for (int i = 0; i < nArraySize; i++)
                {
                    strValues[i] = "0";
                }
                return strValues;
            }
            return strRet;
        }
    }
}


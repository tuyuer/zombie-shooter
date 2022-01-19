using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HitJoy
{
    public class CSVDataRow 
    {
        public List<string> headers = new List<string>();
        public List<string> values = new List<string>();
        public CSVDataRow()
        {

        }

        public string GetValue(string header)
        {
            int nItemIndex = headers.IndexOf(header);
            if (nItemIndex == -1)
            {
                Debug.Log("header not found: " + header);
            }
            string itemValue = values[nItemIndex];
            return itemValue;
        }

        public int GetIntValue(string header)
        {
            int nItemIndex = headers.IndexOf(header);
            if (nItemIndex == -1)
            {
                Debug.Log("header not found: " + header);
            }
            string itemValue = values[nItemIndex];
            if (itemValue.Length == 0)
            {
                itemValue = "0";
            }
            return Convert.ToInt32(itemValue);
        }

		public float GetFloatValue(string header)
		{
			int nItemIndex = headers.IndexOf(header);
			if (nItemIndex == -1)
			{
				Debug.Log("header not found: " + header);
			}
			string itemValue = values[nItemIndex];
			if (itemValue.Length == 0)
			{
				itemValue = "0";
			}
			return Convert.ToSingle(itemValue);
		}

        public string GetStringValue(string header)
        {
            int nItemIndex = headers.IndexOf(header);
            if (nItemIndex == -1)
            {
                Debug.Log("header not found: " + header);
            }
            string itemValue = values[nItemIndex];
            return itemValue;
        }
    }

}

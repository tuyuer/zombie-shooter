using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;

namespace HitJoy
{
    public class CSVProvider
    {

        public static CSVDataTable ParserCsvWithFile(string csvFilePath)
        {
            CSVDataTable dt = null;
            //if (File.Exists(csvFilePath))
            //{

            TextAsset textAsset = Resources.Load(csvFilePath.Replace(".csv", "")) as TextAsset;
                string csvstr = textAsset.text;
//#if UNITY_EDITOR
//                string csvstr = File.ReadAllText(csvFilePath, Encoding.UTF8);
//#elif UNITY_IPHONE
//                string csvstr = File.ReadAllText(csvFilePath, Encoding.UTF8);
//#elif UNITY_ANDROID
                //WWW www = new WWW(csvFilePath);
                //while (!www.isDone) { }
                //string csvstr = www.text;
//#endif
                if (!string.IsNullOrEmpty(csvstr))
                {
                    dt = ParserCsvWithText(csvstr);
                }
            //}
            return dt;
        }


        public static CSVDataTable ParserCsvWithText(string csv) 
        {
            CSVDataTable dataTable = new CSVDataTable();
            if (!string.IsNullOrEmpty(csv))
            {
				string[] csvRows = csv.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string[] csvColumns = null;
                if (csvRows != null)
                {
                    if (csvRows.Length > 0)
                    {
                        for (int i = 0; i < csvRows.Length; i++)
                        {
                            if (csvRows[i] != null)
                            {
                                csvColumns = FromCsvLine(csvRows[i]);
                                if (i == 0)
                                {
                                    //header row
                                    for (int column = 0; column < csvColumns.Length; column++)
                                    {
                                        dataTable.headers.Add(csvColumns[column]);
                                    }
                                }
                                else
                                {
                                    CSVDataRow dataRow = new CSVDataRow();
                                    //value row
                                    for (int column = 0; column < dataTable.headers.Count; column++)
                                    {
                                        dataRow.headers.Add(dataTable.headers[column]);
                                        dataRow.values.Add(csvColumns[column]);
                                    }
                                    dataTable.rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            return dataTable;
        }

   
        /// <summary>
        /// 解析一行CSV数据
        /// </summary>
        /// <param name="csv">csv数据行</param>
        /// <returns></returns>
        public static string[] FromCsvLine(string csv)
        {
            if(csv.Substring(csv.Length - 1).Equals(","))
            {
                csv = csv + ",";
            }
            List<string> csvLiAsc = new List<string>();
            List<string> csvLiDesc = new List<string>();

            if (!string.IsNullOrEmpty(csv))
            {
                //顺序超找
                int lastIndex = 0;
                int quotCount = 0;
                //剩余的字符串
                string lstr = string.Empty;
                for (int i = 0; i < csv.Length; i++)
                {
                    if (csv[i] == '"')
                    {
                        quotCount++;
                    }
                    else if (csv[i] == ',' && quotCount % 2 == 0)
                    {
                        csvLiAsc.Add(ReplaceQuote(csv.Substring(lastIndex, i - lastIndex)));
                        lastIndex = i + 1;
                    }
                    if (i == csv.Length - 1 && lastIndex < csv.Length)
                    {
                        lstr = csv.Substring(lastIndex, i - lastIndex + 1);
                    }
                }
                if (!string.IsNullOrEmpty(lstr))
                {
                    //倒序超找
                    lastIndex = 0;
                    quotCount = 0;
                    string revStr = Reverse(lstr);
                    for (int i = 0; i < revStr.Length; i++)
                    {
                        if (revStr[i] == '"')
                        {
                            quotCount++;
                        }
                        else if (revStr[i] == ',' && quotCount % 2 == 0)
                        {
                            csvLiDesc.Add(ReplaceQuote(Reverse(revStr.Substring(lastIndex, i - lastIndex))));
                            lastIndex = i + 1;
                        }
                        if (i == revStr.Length - 1 && lastIndex < revStr.Length)
                        {
                            csvLiDesc.Add(ReplaceQuote(Reverse(revStr.Substring(lastIndex, i - lastIndex + 1))));
                            lastIndex = i + 1;
                        }

                    }
                    string[] tmpStrs = csvLiDesc.ToArray();
                    Array.Reverse(tmpStrs);
                    csvLiAsc.AddRange(tmpStrs);
                }
            }

            return csvLiAsc.ToArray();
        }

        public static string[] ParserArrayLine(string arrayLine)
        {
            arrayLine = arrayLine.Replace("[", "");
            arrayLine = arrayLine.Replace("]", "");
            return arrayLine.Split(',');
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Reverse(string str)
        {
            string revStr = string.Empty;
            foreach (char chr in str)
            {
                revStr = chr.ToString() + revStr;
            }
            return revStr;
        }
        /// <summary>
        /// 替换CSV中的双引号转义符为正常双引号,并去掉左右双引号
        /// </summary>
        /// <param name="csvValue">csv格式的数据</param>
        /// <returns></returns>
        private static string ReplaceQuote(string csvValue)
        {
            string rtnStr = csvValue;
            if (!string.IsNullOrEmpty(csvValue))
            {
                //首尾都是"
                Match m = Regex.Match(csvValue, "^\"(.*?)\"$");
                if (m.Success)
                {
                    rtnStr = m.Result("${1}").Replace("\"\"", "\"");
                }
                else
                {
                    rtnStr = rtnStr.Replace("\"\"", "\"");
                }
            }
            return rtnStr;

        }


    }
}

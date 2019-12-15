using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Software.Toolkit
{
    public static class CSVTool
    {
        #region SingleLine

        /// <summary>
        /// 写入CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">文件头</param>
        /// <param name="columnValue">单行值，用逗号隔开</param>
        public static void WriteCSV(string filePath, string header, string columnValue)
        {
            BaseWrite(filePath, header, columnValue);
        }

        /// <summary>
        /// 写入CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">文件头</param>
        /// <param name="row">单行值</param>
        public static void WriteCSV(string filePath, string header, DataRow row)
        {
            string result = "";
            foreach (object item in row.ItemArray)
            {
                if (item is DBNull)
                {
                    result += ",";
                }
                else
                {
                    result += item.ToString() + ",";
                }
            }

            BaseWrite(filePath, header, result);
        }

        /// <summary>
        /// 写入单行CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">列名</param>
        /// <param name="content">文件内容</param>
        private static void BaseWrite(string filePath, string header, string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                if (fs.Length == 0)
                {
                    sw.WriteLine(header);
                }

                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
        }

        #endregion

        #region MultiLine

        /// <summary>
        /// 写入CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">文件头</param>
        /// <param name="columnsValue">多行值，每行不同列值用逗号隔开</param>
        public static void WriteCSV(string filePath, string header, string[] columnsValue)
        {
            BaseWrite(filePath, header, columnsValue);
        }

        /// <summary>
        /// 写入CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">文件头</param>
        /// <param name="table">数据表</param>
        public static void WriteCSV(string filePath, string header, DataTable table)
        {
            List<string> contents = new List<string>();

            foreach (DataRow row in table.Rows)
            {
                string result = "";

                foreach (object item in row.ItemArray)
                {
                    if (item is DBNull)
                    {
                        result += ",";
                    }
                    else
                    {
                        result += item.ToString() + ",";
                    }
                }

                contents.Add(result);
            }

            BaseWrite(filePath, header, contents.ToArray());
        }

        /// <summary>
        /// 写入多行CSV
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="header">列名</param>
        /// <param name="contents">文件内容</param>
        private static void BaseWrite(string filePath, string header, string[] contents, int writeInterval = 19)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                if (fs.Length == 0)
                {
                    sw.WriteLine(header);
                }

                int times = 0;
                foreach (string content in contents)
                {
                    sw.WriteLine(content);

                    times++;
                    if (times > writeInterval)
                    {
                        sw.Flush();
                        times = 0;
                    }
                }

                sw.Flush();
                sw.Close();
            }
        }

        #endregion
    }
}

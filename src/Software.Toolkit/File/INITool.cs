using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Software.Toolkit
{
    public static class INITool
    {

        #region  外部引用

        /// <param name="section">小节名</param>
        /// <param name="key">项名</param>
        /// <param name="val">写入的数据</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>0表示失败，非0为成功</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <param name="section">小节名</param>
        /// <param name="key">项名</param>
        /// <param name="def">指定项未找到时返回的默认值</param>
        /// <param name="retVal">指定的字体缓冲区</param>
        /// <param name="size">装载到缓冲区的最大字符数</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>取得字符串缓冲区的长度</returns>
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">小节名</param>
        /// <param name="Key">项名</param>
        /// <param name="Size">缓冲区大小</param>
        /// <param name="iniFilePath">文件路径</param>
        public static T ReadIniData<T>(string Section, string Key, int Size, string iniFilePath, T defaultValue)
        {
            StringBuilder temp = new StringBuilder(Size);
            GetPrivateProfileString(Section, Key, null, temp, Size, iniFilePath);
            if (temp.Length == 0)
            {
                return defaultValue;
            }
            else
            {
                return (T)Convert.ChangeType(temp.ToString(), typeof(T));
            }
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">小节名</param>
        /// <param name="Key">项名</param>
        /// <param name="Value">项值</param>
        /// <param name="iniFilePath">文件路径</param>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            return WritePrivateProfileString(Section, Key, Value, iniFilePath) != 0;
        }

    }
}

using System;
using System.Text.RegularExpressions;
namespace Software.Main
{
    public class SNMatchStrategy
    {
        public static bool LengthMatch(string content, int length)
        {
            return content.Length == length;
        }

        public static bool HeadMatch(string content, string headPattern)
        {
            return Regex.IsMatch(content, $"^{headPattern}");
        }

        public static string CreatConstLength(string content, int length, char paddingChar = '0')
        {
            if (content.Length > length) throw new ArgumentOutOfRangeException("字符串长度超过设定值");

            if (content.Length != length)
            {
                content = content.PadLeft(length, paddingChar);
            }

            return content;
        }
    }
}

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Software.Toolkit
{
    public static class MD5
    {
        public static string EncryptionWithBase64(string content)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] inputBye = Encoding.UTF8.GetBytes(content);
            byte[] outputBye = md5.ComputeHash(inputBye);
            return Convert.ToBase64String(outputBye);
        }

        public static string Encryption(string content)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] inputBye = Encoding.UTF8.GetBytes(content);
            byte[] outputBye = md5.ComputeHash(inputBye);

            string str = BitConverter.ToString(outputBye);
            return string.Join("", str.Where(c => c != '-').ToArray());
        }
    }
}

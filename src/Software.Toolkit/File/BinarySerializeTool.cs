using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Software.Toolkit
{
    /// <summary>
    /// 二进制序列化工具
    /// </summary>
    public static class BinarySerializeTool
    {
        //注意：序列化需要在需序列化的对像前设置特性，如下所示
        //[Serializable]
        //private struct structTest { public byte one; public int two; public string three; }

        /// <summary>
        /// 序列化至TXT文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="source">序列化对象</param>
        /// <param name="mode">指定操作系统打开文件的方式</param>
        /// <param name="access">定义用于控制对文件的读访问、写访问或读/写访问的常数</param>
        /// <param name="share">包含用于控制其他 System.IO.FileStream 对象对同一文件可以具有的访问类型的常数</param>
        public static void SerializeToTxt(string path, object source, FileMode mode = FileMode.Create, FileAccess access = FileAccess.Write, FileShare share = FileShare.Read)
        {
            using (FileStream writeFileStream = new FileStream(path, mode, access, share))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(writeFileStream, source);
                writeFileStream.Close();
            }
        }

        /// <summary>
        /// 从TXT文件反序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="mode">指定操作系统打开文件的方式</param>
        /// <param name="access">定义用于控制对文件的读访问、写访问或读/写访问的常数</param>
        /// <param name="share">包含用于控制其他 System.IO.FileStream 对象对同一文件可以具有的访问类型的常数</param>
        public static object DeserializationFromTxt(string path, FileMode mode = FileMode.Open, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
        {
            using (FileStream readFileStream = new FileStream(path, mode, access, share))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                object temp = formatter.Deserialize(readFileStream);
                readFileStream.Close(); 
                return temp;
            }
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Software.Toolkit
{
    /// <summary>
    /// XML序列化工具
    /// </summary>
    public static class XMLSerializeTool
    {
        /// <summary>
        /// 从文件加载
        /// </summary>
        /// <param name="fileName">包含绝对路径的文件名</param>
        public static T Load<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocument.AppendChild(xmlDeclaration);

                XmlElement rootElement = xmlDocument.CreateElement(typeof(T).Name);
                xmlDocument.AppendChild(rootElement);

                xmlDocument.Save(fileName);
            }

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextReader reader = new XmlTextReader(fileStream);
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 序列化对象到文件
        /// </summary>
        /// <param name="fileName">包含绝对路径的文件名</param>
        /// <param name="targetObject">需保存的对象</param>
        public static void Save<T>(string fileName, T targetObject)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextWriter writer = new XmlTextWriter(fileStream, Encoding.UTF8) { Formatting = Formatting.Indented };
                serializer.Serialize(writer, targetObject);
            }
        }
    }
}
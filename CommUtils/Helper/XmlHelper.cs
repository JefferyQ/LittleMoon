using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CommUtils.Helper
{
    public class XmlHelper
    {
        /// <summary>
        /// 从指定文件加载xml(UTF-8)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string LoadXml(string fileName)
        {
            var streamReader = new StreamReader(fileName, Encoding.UTF8);
            var xmlStr = streamReader.ReadToEnd();
            streamReader.Dispose();
            return xmlStr;
        }

        /// <summary>
        /// 字符反序列化到对象(UTF-8)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr">XML字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlStr) where T : class
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlStr)))
            {
                var xmlSerializer = new XmlSerializer(typeof (T));
                return (T) xmlSerializer.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// 序列化对象,并返回xml字符串(UTF-8)
        /// </summary>
        /// <typeparam name="T">需要序列化的对象,必须声明[Serialize]特性</typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serialize<T>(T t) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                {
                    var xmlSerializer = new XmlSerializer(typeof (T));
                    xmlSerializer.Serialize(xmlTextWriter, t);
                    memoryStream.Position = 0;
                    using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 序列化对象,返回xml字符串(UTF-8)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="encoding"></param>
        /// <param name="removeNs">是否移除xml名称空间</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(T t, Encoding encoding, bool removeNs) where T : class
        {
            string xmlStr;
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlTextWriter = new XmlTextWriter(memoryStream, encoding))
                {
                    var xmlSerializer = new XmlSerializer(typeof (T));
                    if (removeNs)
                    {
                        var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                        xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                        xmlSerializer.Serialize(xmlTextWriter, t, xmlSerializerNamespaces);
                    }
                    else
                    {
                        xmlSerializer.Serialize(xmlTextWriter, t);
                    }
                    memoryStream.Position = 0;
                    using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        xmlStr = streamReader.ReadToEnd();
                    }
                }
            }
            return xmlStr;
        }

        /// <summary>
        /// 序列化对象,并输出到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="fileName"></param>
        public static void SerializeObjToXmlFile<T>(T t, string fileName) where T : class
        {
            using (var writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof (T));
                serializer.Serialize(writer, t);
            }
        }

        /// <summary>
        /// xml字符序列化到文件
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <param name="fileName"></param>
        public static void SerializeStrToXmlFile(string xmlStr, string fileName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            xmlDoc.Save(fileName);
        }
    }
}
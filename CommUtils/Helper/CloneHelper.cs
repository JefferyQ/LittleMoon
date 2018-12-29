using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommUtils.Helper
{
    public class CloneHelper
    {
        /// <summary>
        /// 深克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="realObject"></param>
        /// <returns></returns>
        public T Clone<T>(T realObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, realObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(objectStream);
            }
        }

        /// <summary>
        /// Json化深克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="realObject"></param>
        /// <returns></returns>
        public T CloneByJson<T>(T realObject)
        {
            var oldObject = JsonHelper.JsonHelper.ToJson(realObject);
            return JsonHelper.JsonHelper.FromJson<T>(oldObject);
        }
    }
}

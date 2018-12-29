using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace JsonHelper
{
    public static class JsonHelper
    {
        #region Method
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool HasNullIgnore)
        {
            if (HasNullIgnore)
                return JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson)
        {
            if (!string.IsNullOrEmpty(strJson))
                return JsonConvert.DeserializeObject<T>(strJson);
            return default(T);
        }
        /// <summary>
        /// 功能描述：将List转换为Json
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string ListToJson(IList<object> a)
        {

            DataContractJsonSerializer json = new DataContractJsonSerializer(a.GetType());
            string szJson = "";
            //序列化
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, a);
                szJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return szJson;
        }
        #endregion
    }
}

using System.ComponentModel;

namespace CommUtils.ExtensionMethod
{
    /// <summary>
    /// 字段属性扩展
    /// </summary>
    public static class FieldExtension<T>
    {
        /// <summary>
        /// 根据字段名称获取字段描述信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static string GetDescription(string name, T ent)
        {
            var properties = ent.GetType().GetProperties().ToList(m => m.Name == name);
            var dis = (DescriptionAttribute[]) properties[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
            return dis.Length > 0 ? dis[0].Description : string.Empty;
        }
    }
}

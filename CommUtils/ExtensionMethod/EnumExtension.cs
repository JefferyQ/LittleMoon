using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace CommUtils.ExtensionMethod
{
    public static class EnumExtension
    {
        private static readonly ConcurrentDictionary<Enum, string> ConcurrentDictionary =
            new ConcurrentDictionary<Enum, string>();

        /// <summary>
        /// 获取枚举描述特性值
        /// </summary>
        /// <param name="enumerationValue">枚举值</param>
        /// <returns>枚举值的描述</returns>
        public static string GetDescription(this Enum enumerationValue)
        {
            return ConcurrentDictionary.GetOrAdd(enumerationValue, key =>
            {
                var field = key.GetType().GetField(key.ToString());
                //如果field为null则应该是组合位域值，
                return field == null ? key.GetDescriptions() : GetDescription(field);
            });
        }

        /// <summary>
        /// 获取位域枚举的描述，多个按分隔符组合
        /// </summary>
        public static string GetDescriptions(this Enum enumerationValue, string separator = ",")
        {
            var names = enumerationValue.ToString().Split(',');
            var res = new string[names.Length];
            var type = enumerationValue.GetType();
            for (var i = 0; i < names.Length; i++)
            {
                var field = type.GetField(names[i].Trim());
                if (field == null) continue;
                res[i] = GetDescription(field);
            }
            return res.StrJoinBy(separator);
        }

        private static string GetDescription(MemberInfo field)
        {
            var att = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute) att).Description;
        }

        /// <summary>
        /// 字符串转为枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TEnum TryParse<TEnum>(this string str) where TEnum : struct
        {
            TEnum t;
            Enum.TryParse(str, out t);
            return t;
        }
    }
}
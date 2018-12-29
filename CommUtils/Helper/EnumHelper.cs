using System;
using System.Collections.Generic;
using System.Linq;

namespace CommUtils.Helper
{
    public static class EnumHelper
    {
        public static List<T> EnumToList<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 取泛型枚举
        /// </summary>
        /// <typeparam name="T">具体枚举类型</typeparam>
        /// <param name="source">源枚举</param>
        /// <returns>具体枚举类型</returns>
        public static T GetCustomEnum<T>(this Enum source) where T : Attribute
        {
            var sourceType = source.GetType();
            var sourceName = Enum.GetName(sourceType, source);
            var field = sourceType.GetField(sourceName);
            var attris = field.GetCustomAttributes(typeof(T), false);
            return attris.OfType<T>().FirstOrDefault();
        }
    }
}

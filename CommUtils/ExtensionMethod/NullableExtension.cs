namespace CommUtils.ExtensionMethod
{
    public static class NullableExtension
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T param)
        {
            return param == null;
        }

        /// <summary>
        /// 获取值，如果为空则返回值类型的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T GetValue<T>(this T? param) where T : struct
        {
            return param ?? default(T);
        }
    }
}
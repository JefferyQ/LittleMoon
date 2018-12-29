using System;

namespace CommUtils.Helper
{
    /// <summary>
    /// 时间日期帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// UTC转东八区
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime UtcToEight(this DateTime dateTime)
        {
            return dateTime.AddHours(8);
        }

        /// <summary>
        /// UTC转服务器本地时区时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime UtcToLocalDateTime(this DateTime dateTime)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(dateTime);
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="target">时间戳（秒）</param>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this DateTime target, long unixTimeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// 统一日期格式
        /// </summary>
        public static string ToDateFormat(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转为时分秒格式,忽略日期部分
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToTimeFormat(this DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }
    }
}
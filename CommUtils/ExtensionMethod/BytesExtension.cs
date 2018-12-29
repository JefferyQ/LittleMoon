using System.Text;

namespace CommUtils.ExtensionMethod
{
    public static class BytesExtension
    {
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Utf8ToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string AsciiToString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BigEndianUnicodeToString(this byte[] bytes)
        {
            return Encoding.BigEndianUnicode.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string DefaultToString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Utf32ToString(this byte[] bytes)
        {
            return Encoding.UTF32.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Utf7ToString(this byte[] bytes)
        {
            return Encoding.UTF7.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string UnicodeToString(this byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UnicodeToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
    }
}
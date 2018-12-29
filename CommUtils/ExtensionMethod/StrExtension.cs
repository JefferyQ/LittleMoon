using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommUtils.ExtensionMethod
{
    public static class StrExtension
    {
        /// <summary>
        /// 从此实例检索子字符串。子字符串从指定的字符串开始到指定的字符串结束。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string Substring(this string str, string startString, string endString,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (startString.IsNull()) throw new ArgumentNullException(nameof(startString));
            if (endString.IsNull()) throw new ArgumentNullException(nameof(endString));
            var startStringIndex = str.IndexOf(startString, stringComparison);
            var endStringIndex = str.IndexOf(endString, stringComparison);
            return str.Substring(
                startStringIndex + startString.Length,
                endStringIndex - startString.Length - startStringIndex);
        }

        /// <summary>
        /// 字符串转字节序列（UTF-8）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUtf8Bytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（ASCII）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToAsciiBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（BigEndianUnicode）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBigEndianUnicodeBytes(this string str)
        {
            return Encoding.BigEndianUnicode.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（Default）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToDefaultBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（UTF32）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUtf32Bytes(this string str)
        {
            return Encoding.UTF32.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（UTF7）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUtf7Bytes(this string str)
        {
            return Encoding.UTF7.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列（Unicode）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToUnicodeBytes(this string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }

        /// <summary>
        /// 字符串转字节序列
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str,Encoding encoding)
        {
            if (encoding == null) throw new NullReferenceException(nameof(encoding));
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 指示指定的字符串是 null 还是 Empty 字符串。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 连接指定数组的元素或集合的成员，在每个元素或成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string StrJoinBy<T>(this IEnumerable<T> strs, string separator)
        {
            return string.Join(separator, strs);
        }

        /// <summary>
        /// 返回字符串转换的整形，如果转换不了则默认返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            int i;
            int.TryParse(str, out i);
            return i;
        }

        /// <summary>
        /// 返回字符串转换的浮点，如果转换不了则默认返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            double i;
            double.TryParse(str, out i);
            return i;
        }

        /// <summary>
        /// 返回字符串转换的十进制小数，如果转换不了则默认返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str)
        {
            decimal i;
            decimal.TryParse(str, out i);
            return i;
        }

        /// <summary>
        /// 返回Base64加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 返回Base64解密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        /// <summary>
        /// 描述：判断源字段是否符合正则表达式
        /// </summary>
        /// <param name="source">源字段</param>
        /// <param name="regStr">关键字</param>
        /// <returns></returns>
        public static bool Check(this string source, string regStr)
        {
            return new Regex(regStr).IsMatch(source);
        }

        /// <summary>
        /// 描述：去除空格、回车
        /// </summary>
        /// <param name="source"></param>
        public static string TrimSpace(this string source)
        {
            var retult = string.Empty;
            if (!string.IsNullOrEmpty(source))
                retult = source.Trim().Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace(" ", "");
            return retult;
        }

        /// <summary>
        /// 转化为半角字符串（扩展方法）
        /// 2016-08-08
        /// </summary>
        /// <param name="source">要转化的字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToSbc(this string source) //single byte charactor
        {
            var c = source.ToCharArray();
            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288) //全角空格为12288，半角空格为32
                {
                    c[i] = (char) 32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375) //其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
                    c[i] = (char) (c[i] - 65248);
            }
            return new string(c);
        }

        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 根据指定进制返回整形（只按照字母以及数字转换）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static int ToIntByScale(this string source, Scale scale)
        {
            if (source.IsNullOrWhiteSpace())
                return 0;

            var intScale = (int) scale;
            var tempStr = new string(source.Where(char.IsLetterOrDigit).ToArray());

            //因为运算大小写敏感，如果小于等于36进制则需要转为小写字母
            if (intScale <= 36)
                tempStr = tempStr.ToLower();

            return tempStr.Select((t, i) => (int) (Chars.IndexOf(t)*Math.Pow(intScale, tempStr.Length - i - 1))).Sum();
        }

        /// <summary>
        /// 根据指定进制返回长整形（只按照字母以及数字转换）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static long ToLongByScale(this string source, Scale scale)
        {
            if (source.IsNullOrWhiteSpace())
                return 0;

            var intScale = (int) scale;
            var tempStr = new string(source.Where(char.IsLetterOrDigit).ToArray());

            //因为运算大小写敏感，如果小于等于36进制则需要转为小写字母
            if (intScale <= 36)
                tempStr = tempStr.ToLower();

            return tempStr.Select((t, i) => (long) (Chars.IndexOf(t)*Math.Pow(intScale, tempStr.Length - i - 1))).Sum();
        }

        public static byte[] SerializeUtf8(this string str)
        {
            return str == null ? null : Encoding.UTF8.GetBytes(str);
        }

        public static string DeserializeUtf8(this byte[] stream)
        {
            return stream == null ? null : Encoding.UTF8.GetString(stream);
        }

        /// <summary>
        /// 省略指定长度之后的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string OmitString(this string str, int length = 10)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            if (str.Length < length)
                return str;
            return str.Substring(0, length) + "...";
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        public static string FilterSpecialChars(this string orig)
        {
            //todo 改成正则
            if (orig == null) return null;
            StringBuilder sb = new StringBuilder();
            foreach (var c in orig)
            {
                if (c == '&' || c == '<' || c == '>') sb.Append(' ');
                else sb.Append(c);
            }
            return sb.ToString().Trim();
        }
    }
}
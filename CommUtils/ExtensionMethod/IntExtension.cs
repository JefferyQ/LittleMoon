using System.Collections.Generic;
using System.Text;

namespace CommUtils.ExtensionMethod
{
    public static class IntExtension
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 转换为对应进制的字符串
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static string ToScaleString(this int dec, Scale scale)
        {
            var stack = new Stack<int>();

            while (dec > 0)
            {
                stack.Push(dec%(int) scale);
                dec = dec/(int) scale;
            }

            var sb = new StringBuilder();

            if (scale <= Scale.Decimalism)
                while (stack.Count>0)
                    sb.Append(stack.Pop());
            else
                while (stack.Count > 0)
                    sb.Append(Chars[stack.Pop()]);

            return sb.ToString();
        }
    }

    /// <summary>
    /// 进制枚举
    /// </summary>
    public enum Scale
    {
        /// <summary>
        /// 二进制
        /// </summary>
        Binary = 2,

        /// <summary>
        /// 八进制
        /// </summary>
        Octal = 8,

        /// <summary>
        /// 十进制
        /// </summary>
        Decimalism = 10,

        /// <summary>
        /// 十六进制
        /// </summary>
        Hexadecimal = 16,

        /// <summary>
        /// 三十二进制
        /// </summary>
        Duotricemary = 32,

        /// <summary>
        /// 三十六进制
        /// </summary>
        ThirtySixAry = 36,

        /// <summary>
        /// 六十二进制
        /// </summary>
        SixtyTwoAry = 62
    }
}
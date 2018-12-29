using CommUtils.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommUtils.Helper
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 获取字符串中的数字
        /// 作者：李平波
        /// 日期：2014-12-06
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static decimal GetNumber(string source)
        {
            decimal result = 0;
            if (!string.IsNullOrEmpty(source))
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                source = Regex.Replace(source, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (!string.IsNullOrEmpty(source) && Regex.IsMatch(source, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = decimal.Parse(source);
                }
            }
            return result;
        }

        /// <summary>
        /// 描述：去掉字符串中的数字
        /// 作者：李平波
        /// 日期：2014-12-06
        /// 版本：V1.0
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string RemoveNumber(string source)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(source))
            {
                result = Regex.Replace(source, @"\d", "");
            }
            return result;
        }

        /// <summary>
        /// 从字符串中截取起始、结束符中的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="s">起始符</param>
        /// <param name="e">结束符</param>
        /// <returns></returns>
        public static string GetValue(string str, string s, string e)
        {
            var rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))",
                RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }

        /// <summary>
        /// 字符串中找出一个特定串所有出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="substr">子字符串</param>
        /// <param name="startPos">起始位置</param>
        /// <returns></returns>
        public static int[] GetSubStrCountInStr(string str, string substr, int startPos)
        {
            int foundPos;
            var foundItems = new List<int>();
            do
            {
                foundPos = str.IndexOf(substr, startPos, StringComparison.Ordinal);
                if (foundPos <= -1) continue;
                startPos = foundPos + 1;
                foundItems.Add(foundPos);
            } while (foundPos > -1 && startPos < str.Length);
            return foundItems.ToArray();
        }

        /// <summary>
        /// 字符串中查找闭合符号对应的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="bstr">左侧符号</param>
        /// <param name="estr">右侧符号</param>
        ///  <returns></returns>
        public static List<ClosSignLocationModel> GetClosSignLocation(string str, string bstr, string estr)
        {
            var result = new List<ClosSignLocationModel>();
            var bnumber = new Stack<int>();
            var layNumber = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if ((str.Length >= (i + bstr.Length)) && str.Substring(i, bstr.Length) == bstr)
                {
                    bnumber.Push(i);
                }
                if ((str.Length >= (i + estr.Length)) && str.Substring(i, estr.Length) == estr)
                {
                    var bPos = bnumber.Pop();

                    result.Add(new ClosSignLocationModel
                    {

                        BPos = bPos,
                        EPos = i,
                        InnerMostFlag = layNumber == 0
                    });
                    layNumber++;
                    if (!bnumber.Any())
                        layNumber = 0;
                }
            }
            return result;
        }



        /// <summary>
        /// 在输入字符的首尾补上中括号[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddSqrBrackets(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) ? string.Concat("[", str, "]") : str;
        }

        /// <summary>
        /// 在输入字符的首尾补上中括号【】
        /// </summary>
        /// <author>叶健威 2016-10-19 测试要求加上中文的符合更好标识</author>
        public static string AddSqrBracketsCn(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) ? string.Concat("【", str, "】") : str;
        }

        /// <summary>
        /// 在输入字符的首尾补上括号()
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddBrackets(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) ? string.Concat("(", str, ")") : str;
        }

        /// <summary>
        /// 根据输入的字符串判断是否为订单号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsOrderId(this string str)
        {
            return !string.IsNullOrEmpty(str) &&
                   new Regex("^(A|F){1}[0-9]{11}[a-zA-Z0-9]{4}$", RegexOptions.IgnoreCase).IsMatch(str);
        }

        /// <summary>
        /// 匹配调拨单号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsShiftOrderId(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) && Regex.IsMatch(str, @"^(?i)(SFT-)+");
        }

        /// <summary>
        /// 匹配PKG
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPkg(this string str)
        {
            return Regex.IsMatch(str, @"^PKG\d{11,12}$");
        }

        public static string[] TrySplit(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new string[0];
            var separator = new[] {",", ";", "，", "；", "\n", "\t", "\r", "\v"};
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string TryReplace(this string str, string oldStr, string newStr)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 空值返回 ""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsNull(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return str;
        }

        /// <summary>
        /// 替换空白符
        /// 包括回车、换行、tab制表符、双引号
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            const string whiteSpace = " ";
            return str.Replace("\n", whiteSpace)
                .Replace("\t", whiteSpace)
                .Replace("\r", whiteSpace)
                .Replace("\"", whiteSpace)
                .Replace("\v", whiteSpace);
        }

        /// <summary>
        /// 返回是否
        /// </summary>
        public static string ToBoolCn(this bool str)
        {
            return str ? "是" : "否";
        }

        /// <summary>
        /// 判断字符串中是否包含数字
        /// 作者：陈耀彬
        /// 日期：2016-10-31
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static bool IsContainNumber(this string source)
        {
            if (string.IsNullOrEmpty(source)) return false;
            // 正则表达式剔除非数字字符（不包含小数点.）
            source = Regex.Replace(source, @"[^\d.\d]", "");
            // 如果是数字，则转换为decimal类型
            return !string.IsNullOrEmpty(source) && Regex.IsMatch(source, @"^[+-]?\d*[.]?\d*$");
        }
    }
}
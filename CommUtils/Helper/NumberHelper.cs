#region

using System;
using System.Collections.Generic;

#endregion

namespace CommUtils.Helper
{
    public class NumberHelper
    {

        public static decimal DecimalTryParse(object p1, decimal minValue = 0)
        {
            decimal x1;
            decimal.TryParse(Convert.ToString(p1), out x1);
            return x1 < minValue ? minValue : x1;
        }

        /// <summary>
        ///     bool无法识别
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public static int IntTryParse(object p1, int minValue = 0)
        {
            int re;
            try
            {
                re = int.Parse(Convert.ToDecimal(p1).ToString());
            }
            catch
            {
                re = 0;
            }
            return re < minValue ? minValue : re;
        }

        public static string DefaultZero(string p1)
        {
            if (string.IsNullOrEmpty(p1))
            {
                return "0";
            }
            return p1;
        }

        /// <summary>
        ///     分割多个 I:18 4 O:5,5,5,3
        /// </summary>
        /// <param name="quantities"></param>
        /// <param name="totalBoxes"></param>
        /// <param name="compareType"></param>
        /// <returns></returns>
        public static IEnumerable<int> Split(int quantities, int totalBoxes, CompareType compareType = CompareType.LEQ)
        {
            var tempQuantities = quantities;
            var avg = 0;
            switch (compareType)
            {
                case CompareType.LEQ:
                    avg = Convert.ToInt16(Math.Floor(Convert.ToDecimal(tempQuantities) / totalBoxes));
                    break;
                case CompareType.GEQ:
                    avg = Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(tempQuantities) / totalBoxes));
                    break;
                case CompareType.EQ:
                case CompareType.NEQ:
                case CompareType.L:
                case CompareType.G:
                    throw new ArgumentOutOfRangeException("compareType", compareType, null);
            }
            if (avg <= 0)
                avg = 1;
            var lstResult = new List<int>();
            for (var i = 0; i < totalBoxes; i++)
            {
                if (tempQuantities - avg >= 0 && lstResult.Count != totalBoxes - 1)
                {
                    lstResult.Add(avg);
                }
                else if (tempQuantities > 0)
                {
                    lstResult.Add(tempQuantities);
                }
                else
                {
                    lstResult.Add(0);
                }
                tempQuantities -= avg;
            }
            if (lstResult.Count != totalBoxes)
                throw new ArgumentException("拆错了");
            return lstResult;
        }

        public static string Round(string num, int size = 2)
        {
            if (string.IsNullOrEmpty(num))
            {
                return "";
            }
            decimal result;
            if (decimal.TryParse(num, out result))
            {
                return Math.Round(result, size).ToString();
            }
            return num;
        }
        
        /// <summary>
        /// 默认值为：1
        /// </summary>
        public static int DefaultOne => 1;
    }


    /// <summary>
    ///     比较运算符
    /// </summary>
    public enum CompareType
    {
        /// <summary>
        ///     小于或等于
        /// </summary>
        LEQ,

        /// <summary>
        ///     大于或等于 
        /// </summary>
        GEQ,

        /// <summary>
        ///     等于
        /// </summary>
        EQ,

        /// <summary>
        ///     不等于
        /// </summary>
        NEQ,

        /// <summary>
        ///     小于
        /// </summary>
        L,

        /// <summary>
        ///     大于
        /// </summary>
        G
    }
}
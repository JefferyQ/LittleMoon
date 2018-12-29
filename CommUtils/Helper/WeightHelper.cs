using System;

namespace CommUtils.Helper
{
    /// <summary>
    /// 重量单位助手
    /// </summary>
    public class WeightHelper
    {
        /// <summary>
        /// 描述：重量取整
        /// 作者：李平波
        /// 日期：2014-12-05
        /// 版本：V1.0
        /// </summary>
        /// <param name="weight">原始重量</param>
        /// <param name="param">转换参数</param>
        /// <returns></returns>
        public static double RoundWeight(double weight, double param)
        {
            if (param <= 0 || weight <= 0) //如果转换参数为0，则返回本身重量
                return weight;
            double result;
            //整数部分
            var integerPart = Math.Floor(weight);
            //小数部分
            var decimalPart = weight - integerPart;
            var shang = decimalPart/param;
            var integerShang = Math.Floor(shang); //商的整数部分
            if (shang == integerShang)
            {
                result = integerPart + shang*param;
            }
            else
            {
                result = integerPart + (integerShang + 1)*param;
            }
            return result;
        }
    }
}
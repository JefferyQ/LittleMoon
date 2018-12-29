using System.Text.RegularExpressions;

namespace CommUtils.Helper
{
    /// <summary>
    /// 地址邮编中的特殊符号处理
    /// </summary>
    public static class AddressOrZipHelp
    {
        /// <summary>
        /// 地址、邮编转换为标准的比对值
        /// </summary>
        /// <param name="addressOrZip"></param>
        /// <returns></returns>
        public static string ConvertToStandAddressOrZip(this string addressOrZip)
        {
            return !string.IsNullOrEmpty(addressOrZip)
                ? Regex.Replace(addressOrZip.ToUpper(), " ", "").Replace("-", "").Replace("'", "").Replace("‘", "")
                : string.Empty;
        }
    }
}

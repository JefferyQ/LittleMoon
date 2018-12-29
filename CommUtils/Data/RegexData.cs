namespace CommUtils.Data
{
    /// <summary>
    /// 常用正则表达式判断
    /// </summary>
    public static class RegexData
    {
        /// <summary>
        /// 主键重复
        /// </summary>
        public static string DuplicateKey = @"duplicatekey";

        /// <summary>
        /// 判断带小数表达式
        /// </summary>
        public static string IsNumeric = @"^[-+]?[0-9]+(\.[0-9]+)?$";

        /// <summary>
        /// 判断电子邮件地址EMAIL表达式
        /// </summary>
        public static string IsEmail = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// 电话号码 传真号码表达式
        /// </summary>
        public static string IsFaxOrPhone = @"([0-9]{3}-[0-9]{8})|([0-9]{4}-[0-9]{7})";

        /// <summary>
        /// 6-20位包含字母和数字表达式
        /// </summary>
        public static string NumAndEnglish = @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,20}$";

        /// <summary>
        /// 只包含英文字母表达式
        /// </summary>
        public static string CharacterEnglish = @"^[A-Za-z]+$";

        /// <summary>
        /// 匹配中文字符表达式
        /// </summary>
        public static string ChararcterChinese = @"^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// 全匹配0-9表达式
        /// </summary>
        public static string NumberRegex = @"^[0-9]*$";
    }
}

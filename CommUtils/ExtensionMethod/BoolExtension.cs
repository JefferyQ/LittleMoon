namespace CommUtils.ExtensionMethod
{
    public static class BoolExtension
    {
        /// <summary>
        /// 取反
        /// </summary>
        /// <param name="source"></param>
        /// <param name="isInvert"></param>
        /// <returns></returns>
        public static bool Invert(this bool source, bool isInvert)
        {
            return isInvert ? !source : source;
        }
    }
}

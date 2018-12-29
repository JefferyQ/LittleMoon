
namespace CommUtils.ExtensionMethod
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获取最底层异常，如果InnerException为空则返回自身
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static System.Exception GetInnestException(this System.Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
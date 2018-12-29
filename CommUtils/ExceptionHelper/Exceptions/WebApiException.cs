namespace CommUtils.Exceptions
{
    /// <summary>
    /// 自定义架构WebApi异常
    /// </summary>
    public class WebApiException : BaseException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="errCode">错误代码</param>
        public WebApiException(string message = null, string errCode = "")
            : base(ResultStatus.Failure, errCode, message)
        {
        }
    }
}
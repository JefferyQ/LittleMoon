namespace CommUtils.Exceptions
{
    /// <summary>
    /// 自定义权限异常
    /// </summary>
    public class AuthorizationException : BaseException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="errCode">错误代码</param>
        public AuthorizationException(string message = null, string errCode = "")
            : base(ResultStatus.Failure, errCode,message)
        {
        }
    }
}

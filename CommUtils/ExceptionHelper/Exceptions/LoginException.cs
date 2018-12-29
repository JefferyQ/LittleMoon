namespace CommUtils.Exceptions
{
    /// <summary>
    /// 自定义登陆异常
    /// </summary>
    public class LoginException : BaseException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="errCode">错误代码</param>
        public LoginException(string message = null, string errCode = "") : base(ResultStatus.Failure, errCode, message)
        {
        }
    }
}
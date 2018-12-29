namespace CommUtils.Exceptions
{
    /// <summary>
    /// api异常信息基类
    /// </summary>
    public class BaseException : System.Exception
    {
        /// <summary>
        /// 状态
        /// </summary>
        public ResultStatus Status { get; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; }

        /// <summary>
        /// api异常信息基类构造函数
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="errcode">错误码</param>
        /// <param name="errmsg">错误信息</param>
        public BaseException(ResultStatus status, string errcode, string errmsg):base(errmsg)
        {
            Status = status;
            ErrCode = errcode;
            ErrMsg = errmsg;
        }
    }
}

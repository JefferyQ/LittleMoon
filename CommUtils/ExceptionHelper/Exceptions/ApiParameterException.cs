namespace CommUtils.Exceptions
{
    /// <summary>
    /// 参数异常
    /// </summary>
    public class ApiParameterException: BaseException
    {
        public ApiParameterException(string errmsg)
            : base(ResultStatus.Failure, "parameter_error", errmsg)
        {
        }
    }
}

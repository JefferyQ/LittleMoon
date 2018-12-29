using System.ComponentModel;

namespace CommUtils
{
    /// <summary>
    /// api状态
    /// </summary>
    public enum ResultStatus
    {
        [Description("失败")]
        Failure = 0,
        [Description("成功")]
        Success = 1,
        [Description("警告")]
        Warning = 2,
        [Description("提示")]
        Info = 3,
        [Description("异常")]
        Exception = 4,
    }
}

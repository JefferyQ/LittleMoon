using CommUtils.Attributes;
using System.Collections.Generic;

namespace CommUtils.Data
{
    [Json(false)]
    public class PageJsonExt<T> where T : class
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public ErrCode ErrCode { set; get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { set; get; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Draw { set; get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordsTotal { set; get; }

        /// <summary>
        /// 筛选后记录数
        /// </summary>
        public int RecordsFiltered { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Data { set; get; }
    }

    public enum Status
    {
        Failure = 0,
        Success = 1,
        Warning = 2,
        Info = 3
    }

    public enum ErrCode
    {
        None = 0,
        NotFound = 1,
        General = 2,
        NotLoggedIn = 3,
        Authorization = 10,
    }
}

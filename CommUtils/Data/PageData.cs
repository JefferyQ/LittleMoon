using CommUtils.Attributes;
using System.Collections.Generic;

namespace CommUtils.Data
{
    [Json(false)]
    public class PageData<T>
    {
        public IList<T> Items { get; set; }

        public PageInfo PageInfo { get; set; }
    }

    [Json(false)]
    public class PageInfo
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPage { get; set; }
    }
}

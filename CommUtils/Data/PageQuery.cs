namespace CommUtils.Data
{
    public class PageQuery
    {

        private int _pageIndex = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value > 0 ? value : _pageIndex;
        }

        private int _pageSize = 15;

        /// <summary>
        /// 页面容量
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : _pageSize;
        }

        /// <summary>
        /// 分页使用参数
        /// </summary>
        public int Draw { set; get; }
    }
}

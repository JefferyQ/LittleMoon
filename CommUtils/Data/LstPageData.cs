using System.Collections.Generic;

namespace CommUtils.Data
{
    /// <summary>
    /// 列表页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LstPageData<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        private List<T> _items;

        /// <summary>
        /// 列表页数据
        /// </summary>
        public List<T> Items
        {
            get
            {
                return _items ?? (_items = new List<T>());
            }
            set
            {
                _items = value;
            }
        }

        private SearchPageInfo _searchPageInfo;

        public SearchPageInfo SearchPageInfo
        {
            get
            {
                return _searchPageInfo ?? (_searchPageInfo = new SearchPageInfo());
            }
            set
            {
                _searchPageInfo = value;
            }
        }
    }
    
    /// <summary>
         /// 搜索列表页设置（默认第一页，每页10条数据）
         /// </summary>
    public class SearchPageInfo
    {
        private int _pageSize = 10, _pageIndex = 1;

        public Dictionary<string, OrderBy> Sort { get; set; }

        public SearchPageInfo()
        {
        }

        public SearchPageInfo(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > 0 ? value : _pageSize; }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value > 0 ? value : _pageIndex; }
        }
    }
    public enum OrderBy
    {
        Asc = 0,
        Desc = 1
    }
}
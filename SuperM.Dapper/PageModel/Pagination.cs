namespace SuperM.Dapper.PageModel
{
    public class Pagination : IPagination
    {
        /// <summary>
        /// 初始化分页参数
        /// </summary>
        public Pagination()
            : this(1)
        {
        }

        /// <summary>
        /// 初始化分页参数
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数,默认20</param> 
        public Pagination(int page, int pageSize)
            : this(page, pageSize, 0)
        {
        }

        /// <summary>
        /// 初始化分页参数
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数,默认20</param> 
        /// <param name="totalCount">总行数</param>
        public Pagination(int page, int pageSize = 20, int totalCount = 0)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        private int _pageIndex;
        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int Page
        {
            get
            {
                if (_pageIndex <= 0)
                    _pageIndex = 1;
                return _pageIndex;
            }
            set => _pageIndex = value;
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 获取总页数
        /// </summary>
        public int GetPageCount()
        {
            if ((TotalCount % PageSize) == 0)
                return TotalCount / PageSize;
            return (TotalCount / PageSize) + 1;
        }

        /// <summary>
        /// 获取跳过的行数
        /// </summary>
        public int GetSkipCount()
        {
            if (Page > GetPageCount())
                Page = GetPageCount();
            return PageSize * (Page - 1);
        }


        /// <summary>
        /// 起始行数
        /// </summary>
        public int GetStartNumber()
        {
            return (Page - 1) * PageSize + 1;
        }
        /// <summary>
        /// 结束行数
        /// </summary>
        public int GetEndNumber()
        {
            return Page * PageSize;
        }
    }
}

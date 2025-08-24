namespace Application.Common.Pagination
{
    public class PageParam
    {
        private int _pageSize = 10;
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
        public string SearchKey { get; set; }
        public int Order { get; set; } = -1;
    }
}

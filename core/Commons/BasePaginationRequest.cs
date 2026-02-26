namespace Core.Commons
{
    public class BasePaginationRequest
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int Skip => PageIndex * PageSize;
        public int Take => PageSize;
        public string Keyword { get; set; } = null;
        public string SortBy { get; set; } = null;
        public bool SortDescending { get; set; } = false;
    }
}

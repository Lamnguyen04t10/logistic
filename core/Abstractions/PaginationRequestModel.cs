namespace Core.Abstractions
{
    public class PaginationRequestModel
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public int Skip => PageIndex * PageSize;
        public int Take => PageSize;
    }
}

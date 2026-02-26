namespace Core.Commons
{
    public class BasePaginationResponse<T>(int pageIndex, int pageSize, int total, List<T> items)
        where T : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int Total { get; set; } = total;
        public IReadOnlyCollection<T> Items { get; set; } = items;
    }
}

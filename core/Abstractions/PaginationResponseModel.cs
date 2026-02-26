namespace Core.Abstractions
{
    public record PaginationResponseModel<T>(
        int PageIndex,
        int PageSize,
        int Total,
        IEnumerable<T> Items
    )
        where T : IResponseModel;
}

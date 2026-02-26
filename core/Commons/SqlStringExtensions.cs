using System.Text;

namespace Core.Commons
{
    public static class SqlStringExtensions
    {
        public static StringBuilder AddOrderBy(string column, bool isSortDescending)
        {
            var orderBy = new StringBuilder();
            if (!string.IsNullOrEmpty(column))
            {
                orderBy.Append(" ORDER BY ");
                orderBy.Append(column);
                if (isSortDescending)
                {
                    orderBy.Append(" DESC");
                }
                else
                {
                    orderBy.Append(" ASC");
                }
            }
            return orderBy;
        }

        public static StringBuilder AddPagination(int skip, int take)
        {
            var pagination = new StringBuilder();
            if (skip >= 0 && take > 0)
            {
                pagination.Append(" OFFSET ");
                pagination.Append(skip);
                pagination.Append(" ROWS FETCH NEXT ");
                pagination.Append(take);
                pagination.Append(" ROWS ONLY");
            }
            return pagination;
        }
    }
}

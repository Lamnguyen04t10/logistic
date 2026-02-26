using System.Text;
using Dapper;

namespace Core.Abstractions
{
    public static class SqlQueryExtensions
    {
        public static StringBuilder QueryFetch(
            this StringBuilder sql,
            string oderBy,
            DynamicParameters parameters,
            int pageIndex,
            int pageSize
        )
        {
            parameters.Add("@Offset", pageIndex * pageSize);
            parameters.Add("@PageSize", pageSize);
            return sql.Append(
                @$"ORDER BY {oderBy} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;"
            );
        }

        public static StringBuilder AppendCondition(this StringBuilder sql, params string[] values)
        {
            if (!sql.ToString().Contains("WHERE"))
                sql.Append(" WHERE ");
            for (int i = 0; i < values.Length; i++)
            {
                sql.Append(values[i]);
                if (i < values.Length - 1)
                {
                    sql.Append(" AND ");
                }
            }
            return sql;
        }
    }
}

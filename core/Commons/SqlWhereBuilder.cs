using System.Collections;
using System.Text;
using Dapper;

public class SqlQueryBuilder
{
    private readonly StringBuilder _whereBuilder = new();
    private readonly StringBuilder _joinBuilder = new();
    private readonly DynamicParameters _parameters = new();
    private string? _orderBy;
    private string? _pagination;
    private int _paramIndex = 0;

    public SqlQueryBuilder AddCondition(string field, string op, object? value = null)
    {
        if (string.IsNullOrWhiteSpace(field))
            return this;

        string clause = BuildClause(field, op.ToUpperInvariant(), value);
        if (!string.IsNullOrEmpty(clause))
        {
            if (_whereBuilder.Length > 0)
                _whereBuilder.Append(" AND ");
            else
                _whereBuilder.Append(" WHERE ");

            _whereBuilder.Append(clause);
        }

        return this;
    }

    private string BuildClause(string field, string op, object? value)
    {
        string baseParam = $"p{_paramIndex++}";

        return op switch
        {
            "IN" when value is IEnumerable enumerable => HandleIn(field, enumerable, baseParam),
            "NOT IN" when value is IEnumerable enumerable => HandleIn(
                field,
                enumerable,
                baseParam,
                notIn: true
            ),
            "BETWEEN" when value is Tuple<object, object> tuple => HandleBetween(
                field,
                tuple,
                baseParam
            ),
            "IS NULL" => $"{field} IS NULL",
            "IS NOT NULL" => $"{field} IS NOT NULL",
            "LIKE" => AddParamAndReturn(field, "LIKE", value, baseParam),
            "NOT LIKE" => AddParamAndReturn(field, "NOT LIKE", value, baseParam),
            "=" or ">" or "<" or ">=" or "<=" or "<>" or "!=" => AddParamAndReturn(
                field,
                op,
                value,
                baseParam
            ),
            _ => string.Empty,
        };
    }

    private string AddParamAndReturn(string field, string op, object? value, string param)
    {
        if (value is null)
            return string.Empty;
        _parameters.Add(param, value);
        return $"{field} {op} @{param}";
    }

    private string HandleIn(string field, IEnumerable values, string baseParam, bool notIn = false)
    {
        var inParams = new List<string>();
        int count = 0;

        foreach (var val in values)
        {
            var name = $"{baseParam}_{count++}";
            _parameters.Add(name, val);
            inParams.Add($"@{name}");
        }

        if (inParams.Count == 0)
            return string.Empty;

        string keyword = notIn ? "NOT IN" : "IN";
        return $"{field} {keyword} ({string.Join(",", inParams)})";
    }

    private string HandleBetween(string field, Tuple<object, object> range, string baseParam)
    {
        var startParam = $"{baseParam}_start";
        var endParam = $"{baseParam}_end";
        _parameters.Add(startParam, range.Item1);
        _parameters.Add(endParam, range.Item2);

        return $"{field} BETWEEN @{startParam} AND @{endParam}";
    }

    public SqlQueryBuilder AddJoin(string joinType, string tableName, string onCondition)
    {
        if (
            string.IsNullOrWhiteSpace(joinType)
            || string.IsNullOrWhiteSpace(tableName)
            || string.IsNullOrWhiteSpace(onCondition)
        )
            return this;

        _joinBuilder.AppendLine($"{joinType.ToUpperInvariant()} JOIN {tableName} ON {onCondition}");
        return this;
    }

    public SqlQueryBuilder InnerJoin(string table, string onCondition) =>
        AddJoin("INNER", table, onCondition);

    public SqlQueryBuilder LeftJoin(string table, string onCondition) =>
        AddJoin("LEFT", table, onCondition);

    public SqlQueryBuilder RightJoin(string table, string onCondition) =>
        AddJoin("RIGHT", table, onCondition);

    public SqlQueryBuilder FullJoin(string table, string onCondition) =>
        AddJoin("FULL", table, onCondition);

    public SqlQueryBuilder AddOrderBy(string column, bool isDescending = false)
    {
        if (!string.IsNullOrWhiteSpace(column))
        {
            _orderBy = $" ORDER BY {column} {(isDescending ? "DESC" : "ASC")}";
        }
        else
        {
            _orderBy = @$" ORDER BY ""Id"" {(isDescending ? "DESC" : "ASC")}";
        }
        return this;
    }

    public SqlQueryBuilder AddPagination(int skip, int take)
    {
        if (skip >= 0 && take > 0)
        {
            _pagination = $" OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
        }
        return this;
    }

    public string BuildQuery(string baseSql)
    {
        return $"{baseSql} {_joinBuilder}{_whereBuilder}{_orderBy}{_pagination}";
    }

    public string BuildCountQuery(string tableOrSubQuery)
    {
        return $"SELECT COUNT(*) FROM {tableOrSubQuery} {_joinBuilder}{_whereBuilder}";
    }

    public DynamicParameters Parameters => _parameters;
}

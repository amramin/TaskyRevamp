namespace TaskyRevamp.Dto.GeneralDto;

public class PagingParameterModel
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}

public class FilteringParameterModel
{
    public string? SearchTerm { get; set; }
}

public class QueryModel
{
    public PagingParameterModel? Paging { get; set; } = null;
    public FilteringParameterModel? Filter { get; set; } = null;

    public List<Guid>? CheckedFilter { get; set; } = null;
}

public class PaginatedList<T>
{
    public List<T> List { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}
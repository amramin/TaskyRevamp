namespace TaskyRevamp.Dto;

public class SearchableBackendDto<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
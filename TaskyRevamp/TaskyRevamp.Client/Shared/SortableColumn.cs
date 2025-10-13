namespace TaskyRevamp.Client.Shared;

public class SortableColumn
{
    public string Field { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool IsSortable { get; set; } = true; // Default: sortable
}

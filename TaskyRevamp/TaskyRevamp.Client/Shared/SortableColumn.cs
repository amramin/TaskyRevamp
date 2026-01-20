namespace TaskyRevamp.Client.Shared;

public class SortableColumn
{
    public string Field { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool IsSortable { get; set; } = true; // Default: sortable 
    public int Order { get; set; }           // Grid order
    public bool IsVisible { get; set; }      // Show / Hide


}

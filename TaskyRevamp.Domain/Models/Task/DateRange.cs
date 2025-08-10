namespace TaskyRevamp.Domain.Models.Task;

public record DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }
    public DateRange(DateTime start, DateTime end)
    {
        if (end < start) throw new ArgumentException("End date must be on or after start date");
        Start = start;
        End = end;
    }
}
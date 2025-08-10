namespace TaskyRevamp.Domain.Models.Task;

public record Reminder
{
    public DateTime Date { get; }
    public Reminder(DateTime date)
    {
        Date = date;
    }
}
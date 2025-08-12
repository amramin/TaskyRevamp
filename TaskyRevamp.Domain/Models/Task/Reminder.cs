using Microsoft.EntityFrameworkCore;

namespace TaskyRevamp.Domain.Models.Task;

[Owned]
public record Reminder
{
    public DateTime Date { get; }
    public Reminder(DateTime date)
    {
        Date = date;
    }

    public Reminder()
    {
    }
}
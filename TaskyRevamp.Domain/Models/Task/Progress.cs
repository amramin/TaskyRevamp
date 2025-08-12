using Microsoft.EntityFrameworkCore;

namespace TaskyRevamp.Domain.Models.Task;

[Owned]
public record Progress
{
    public int Percentage { get; }
    public Progress(int percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentOutOfRangeException(nameof(percentage), "Progress must be between 0 and 100");
        Percentage = percentage;
    }

    public Progress()
    {
    }
}
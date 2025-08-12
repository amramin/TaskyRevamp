using Microsoft.EntityFrameworkCore;

namespace TaskyRevamp.Domain.Models.Task;

[Owned]
public record Weight
{
    public int Value { get; }
    public Weight(int value)
    {
        if (value < 0 || value > 100) throw new ArgumentOutOfRangeException(nameof(value), "Weight must be between 0 and 100");
        Value = value;
    }

    public Weight()
    {
    }
}
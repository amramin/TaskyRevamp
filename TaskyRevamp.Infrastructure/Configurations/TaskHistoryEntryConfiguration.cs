using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;

namespace TaskyRevamp.Infrastructure.Configurations;

internal class TaskHistoryEntryConfiguration : IEntityTypeConfiguration<TaskHistoryEntry>
{
    public void Configure(EntityTypeBuilder<TaskHistoryEntry> builder)
    {
        builder
    .HasOne(e => e.By)
    .WithMany()
    .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction in EF Core 5+

        builder
             .HasOne(e => e.Task)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade); // keep cascade only where needed

    }
}

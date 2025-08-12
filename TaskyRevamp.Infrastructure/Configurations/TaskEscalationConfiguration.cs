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

internal class TaskEscalationConfiguration : IEntityTypeConfiguration<TaskEscalation>
{
    public void Configure(EntityTypeBuilder<TaskEscalation> builder)
    {
        builder
   .HasOne(te => te.EscalatedTo)
   .WithMany()
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

        builder
            .HasOne(te => te.RequestedBy)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(te => te.Task)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
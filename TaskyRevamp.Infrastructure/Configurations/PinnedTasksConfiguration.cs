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

internal class PinnedTasksConfiguration : IEntityTypeConfiguration<PinnedTasks>
{
    public void Configure(EntityTypeBuilder<PinnedTasks> builder)
    {
        builder
   .HasOne(te => te.Task)
   .WithMany().HasForeignKey(k=>k.TaskId)
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

     

    }
}
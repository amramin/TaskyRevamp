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

internal class TaskAssigneesConfiguration : IEntityTypeConfiguration<TaskAssignees>
{
    public void Configure(EntityTypeBuilder<TaskAssignees> builder)
    {
        builder
   .HasOne(te => te.task)
   .WithMany(tt => tt.Assignees).HasForeignKey(t => t.taskId)
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

        builder
            .HasOne(te => te.User)
            .WithMany().HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);



    }
}
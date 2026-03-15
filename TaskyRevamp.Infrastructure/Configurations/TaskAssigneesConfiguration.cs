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

internal class TaskAssigneesConfiguration : IEntityTypeConfiguration<TaskAssignee>
{
    public void Configure(EntityTypeBuilder<TaskAssignee> builder)
    {
        builder
   .HasOne(te => te.TaskItem)
   .WithMany(tt => tt.TaskAssignees).HasForeignKey(t => t.TaskItemId)
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

        builder
            .HasOne(te => te.User)
            .WithMany().HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);



    }
}
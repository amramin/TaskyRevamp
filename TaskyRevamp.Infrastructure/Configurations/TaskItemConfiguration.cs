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

internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder
   .HasOne(te => te.Source)
   .WithMany().HasForeignKey(te => te.TaskSourceId)
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

        builder
            .HasOne(te => te.Type)
            .WithMany().HasForeignKey(k => k.TaskTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
        .HasOne(te => te.status)
        .WithMany().HasForeignKey(k => k.StatusId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
         .HasOne(te => te.Priority)
         .WithMany().HasForeignKey(k => k.PriorityId)
         .OnDelete(DeleteBehavior.Restrict);
        builder
             .HasOne(te => te.CreatedBy)
             .WithMany().HasForeignKey(k => k.CreatedById)
             .OnDelete(DeleteBehavior.NoAction);
        builder

     .HasOne(te => te.UpdatedBy)
     .WithMany()
     .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(te => te.Comments)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
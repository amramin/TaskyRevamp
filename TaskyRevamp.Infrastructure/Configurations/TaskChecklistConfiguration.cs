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

internal class TaskChecklistConfiguration : IEntityTypeConfiguration<TaskChecklist>
{
    public void Configure(EntityTypeBuilder<TaskChecklist> builder)
    {
        builder
   .HasOne(te => te.taskItem)
   .WithMany(tc=>tc.taskChecklists).HasForeignKey(p=>p.TaskItemId)
   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+


        

    }
}
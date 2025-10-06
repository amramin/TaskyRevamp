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

internal class ChecklistItemConfiguration : IEntityTypeConfiguration<ChecklistItem>
{
    public void Configure(EntityTypeBuilder<ChecklistItem> builder)
    {
        builder
   .HasOne(te => te.TaskChecklist)
   .WithMany(tc=>tc.items)
   .HasForeignKey(ci => ci.TaskChecklistId)  // Use this FK

   .OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+

        builder
.HasOne(te => te.AssignedUser)
.WithMany()
.HasForeignKey(ci => ci.AssignedUserId)  // Use this FK

.OnDelete(DeleteBehavior.Restrict); // or NoAction in EF Core 5+




    }
}
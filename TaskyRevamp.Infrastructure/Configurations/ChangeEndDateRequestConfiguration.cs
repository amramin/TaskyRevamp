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

internal class ChangeEndDateRequestConfiguration : IEntityTypeConfiguration<ChangeEndDateRequest>
{
    public void Configure(EntityTypeBuilder<ChangeEndDateRequest> builder)
    {
        builder
    .HasOne(r => r.Task)
    .WithMany().HasForeignKey(r => r.TaskId)
    .OnDelete(DeleteBehavior.NoAction);
        builder
.HasOne(r => r.CreatedBy)
.WithMany().HasForeignKey(r => r.CreatedById)
.OnDelete(DeleteBehavior.NoAction);
        builder
.HasOne(r => r.Requester)
.WithMany().HasForeignKey(r => r.RequesterId)
.OnDelete(DeleteBehavior.NoAction);

    }
}

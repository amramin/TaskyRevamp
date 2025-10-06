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

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
    
.HasOne(r => r.CreatedBy)
.WithMany().HasForeignKey(r => r.CreatedById)
.OnDelete(DeleteBehavior.NoAction);

        builder
       .HasOne(r => r.Parentdepartment)
.WithMany().HasForeignKey(r => r.ParentdepartmentId)
.OnDelete(DeleteBehavior.NoAction);
        
        builder

.HasOne(r => r.UpdateddBy)
.WithMany().HasForeignKey(r => r.UpdatedById)
.OnDelete(DeleteBehavior.NoAction);

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
    
.HasOne(r => r.Department)
.WithMany(k=>k.AssignedUser).HasForeignKey(r => r.DepartmentId)
.OnDelete(DeleteBehavior.NoAction);

   
    }
}

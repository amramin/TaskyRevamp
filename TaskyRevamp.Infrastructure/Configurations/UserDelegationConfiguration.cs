using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users.UserDelegations;

namespace TaskyRevamp.Infrastructure.Configurations;

internal class UserDelegationConfiguration : IEntityTypeConfiguration<UserDelegation>
{
    public void Configure(EntityTypeBuilder<UserDelegation> builder)
    {
        builder
    
.HasOne(r => r.FromUser)
.WithMany().HasForeignKey(r => r.FromUserId)
.OnDelete(DeleteBehavior.NoAction);
     
        builder
           .HasOne(r => r.Touser)
.WithMany().HasForeignKey(r => r.ToUserId)
.OnDelete(DeleteBehavior.NoAction);

    }
}

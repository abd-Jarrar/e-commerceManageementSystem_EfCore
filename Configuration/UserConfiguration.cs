using E_CommerceManagementSystemEfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.FullName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.IsDeleted).IsRequired();
            
        }
    }
}

using ECommerce.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();
            builder.Property(x=>x.FullName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.HasDiscriminator<string>("UserType")
                .HasValue<Employee>("Employee")
                .HasValue<Customer>("Customer")
                .HasValue<Admin>("Admin");
            builder.ToTable("Users");
            
        }
    }
}

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
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.HasOne(x => x.Customer)
            .WithOne(x => x.CustomerProfile)
            .HasForeignKey<CustomerProfile>(x => x.Id);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.ToTable("CustomerProfiles");
        }
    }
}

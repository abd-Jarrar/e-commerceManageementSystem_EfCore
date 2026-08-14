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
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(
                 new CustomerProfile { Id = 1, PhoneNumber = "050234444", BirthDate = new DateTime(2026, 8, 12) },
                 new CustomerProfile { Id = 2, PhoneNumber = "050234444", BirthDate = new DateTime(2026, 8, 12) }

                );
            builder.Property(x=>x.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.HasOne(x => x.Customer)
            .WithOne(x => x.CustomerProfile)
            .HasForeignKey<CustomerProfile>(x => x.Id);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.ToTable("CustomerProfiles");
        }
    }
}

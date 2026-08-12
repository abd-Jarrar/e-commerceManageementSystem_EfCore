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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            
            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.City).IsRequired().HasMaxLength(100);
                address.Property(x => x.Street).IsRequired().HasMaxLength(200);
                address.Property(x => x.BuildingNumber).IsRequired().HasMaxLength(20);
            });
            
            builder.HasMany(x=>x.Orders).WithOne(x=>x.Customer).HasForeignKey(x=>x.CustomerId);
            builder.ToTable("Customers");
        } 
    }
}

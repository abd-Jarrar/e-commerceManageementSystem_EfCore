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

                address.HasData(
        new
        {
            CustomerId = 1,
            City = "nablus",
            Street = "Main Street",
            BuildingNumber = "10"
        },
        new
        {
            CustomerId = 2,
            City = "nablus",
            Street = "Main Street",
            BuildingNumber = "20"
        }
    );
            });
            builder.HasData(
                new Customer
                {
                     Id = 1,
                     FullName = "abood",
                     Email = "abood@example.com",
                     
                },
                new Customer
                {
                    Id = 2,
                    FullName = "rami",
                    Email = "rami@example.com",
                    
                }
                );
            builder.HasMany(x=>x.Orders).WithOne(x=>x.Customer).HasForeignKey(x=>x.CustomerId);
            
        } 
    }
}

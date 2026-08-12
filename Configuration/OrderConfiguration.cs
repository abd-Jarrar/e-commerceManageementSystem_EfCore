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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(
            new Order
            {
                Id = 1,
                CreatedDate = new DateTime(2026, 8, 1),
                Status = OrderStatus.Pending,
                CustomerId = 1
            },
            new Order
            {
                Id = 2,
                CreatedDate = new DateTime(2026, 8, 5),
                Status = OrderStatus.Shipped,
                CustomerId = 2
            }
        );
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.Ignore(x => x.TotalPrice);
            builder.HasIndex(x => new { x.CustomerId, x.CreatedDate });
            
            builder.ToTable("Orders");

        }
    }
}

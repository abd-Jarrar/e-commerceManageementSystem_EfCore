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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasData(
            new OrderItem
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 100m
            },
            new OrderItem
            {
                OrderId = 1,
                ProductId = 2,
                Quantity = 1,
                Price = 50m
            },
            new OrderItem
            {
                OrderId = 2,
                ProductId = 1,
                Quantity = 1,
                Price = 100m
            }
        );

            builder.HasOne(x => x.Order)
              .WithMany(x => x.Items)
              .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId);
        }
    }
}

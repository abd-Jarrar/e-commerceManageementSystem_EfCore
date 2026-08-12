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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasData(
        new Product
        {
            Id = 1,
            Name = "Gaming Laptop",
            Price = 1200m,
            StockQuantity = 10,
            CategoryId = 1
        },
        new Product
        {
            Id = 2,
            Name = "t-shirt",
            Price = 50m,
            StockQuantity = 25,
            CategoryId = 2
        },
        new Product
        {
            Id = 3,
            Name = "c# for beginners",
            Price = 80m,
            StockQuantity = 15,
            CategoryId = 2
        }
    );
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2); 
            builder.Property(x => x.StockQuantity).IsRequired();
            builder.OwnsOne(x => x.Details, x =>
            {
                x.Property(x => x.Weight).IsRequired().HasPrecision(18, 2); ;
                x.Property(x => x.Color).IsRequired().HasMaxLength(25);
                x.Property(x => x.Description).IsRequired().HasMaxLength(300);
                x.HasData(
            new
            {
                ProductId = 1,
                Weight = 1.5m,
                Color = "Black",
                Description = "High performance gaming laptop"
            },
            new
            {
                ProductId = 2,
                Weight = 0.45m,
                Color = "White",
                Description = "Professional football"
            },
            new
            {
                ProductId = 3,
                Weight = 0.8m,
                Color = "Blue",
                Description = "Comfortable running shoes"
            }
        );
            });
            builder.ToTable("Products");
        }
    }
}

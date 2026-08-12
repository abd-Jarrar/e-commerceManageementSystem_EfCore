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

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2); 
            builder.Property(x => x.StockQuantity).IsRequired();
            builder.OwnsOne(x => x.Details, x =>
            {
                x.Property(x => x.Weight).IsRequired().HasPrecision(18, 2); ;
                x.Property(x => x.Color).IsRequired().HasMaxLength(25);
                x.Property(x => x.Description).IsRequired().HasMaxLength(300);

            });
            builder.ToTable("Products");
        }
    }
}

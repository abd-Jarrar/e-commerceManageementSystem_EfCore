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
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.Ignore(x => x.TotalPrice);
            builder.HasIndex(x => new { x.CustomerId, x.CreatedDate });
            builder.ToTable("Orders");

        }
    }
}

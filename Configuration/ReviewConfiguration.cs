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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(300);
            builder.HasOne<Customer>(x=>x.Customer).WithMany(x=>x.Reviews).HasForeignKey(x => x.CustomerId);
            builder.HasOne<Product>(x => x.Product).WithMany(x=>x.Reviews).HasForeignKey(x => x.ProductId);
        }
    }
}

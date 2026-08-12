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
    public class ProductSalesSummaryConfiguration : IEntityTypeConfiguration<ProductSalesSummary>
    {
        public void Configure(EntityTypeBuilder<ProductSalesSummary> builder)
        {
            builder.HasNoKey();
            builder.ToView("ProductSalesSummary");
            builder.Property(x=>x.ProductName).HasMaxLength(100);
            builder.Property(x => x.TotalRevenue).HasPrecision(18, 2);
        }
    }
}

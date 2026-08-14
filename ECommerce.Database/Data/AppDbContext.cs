using ECommerce.Database.Configuration;
using ECommerce.Business.Entities;
using ECommerce.Database.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductSalesSummary> ProductSalesSummaries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}

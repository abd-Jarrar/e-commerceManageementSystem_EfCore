using ECommerce.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = 3,
                    FullName = "abood",
                    Email = "abood23@example.com",
                    Salary = 2000,
                    HireDate = new DateTime(2016, 8, 2)
                });
            builder.Property(x => x.Salary).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.HireDate).IsRequired();
        }
    }
}

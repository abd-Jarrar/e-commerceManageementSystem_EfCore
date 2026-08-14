using ECommerce.Business.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce.Database.Data;
using ECommerce.Database.Interceptors;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            var connectionString = configuration
                .GetSection("constr")
                .Value;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .AddInterceptors(new SoftDeleteInterceptor())
                .Options;

            using var context = new AppDbContext(options);

            Console.WriteLine("E-Commerce Management System");
        }
    }
}

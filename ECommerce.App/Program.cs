using ECommerce.Business.Entities;
using ECommerce.Business.Services;
using ECommerce.Database.Data;
using ECommerce.Database.Interceptors;
using ECommerce.Database.Repositories;
using Microsoft.EntityFrameworkCore;
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

            var productRepository = new ProductRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);


            var productService = new ProductService(
                productRepository,
                categoryRepository,
                unitOfWork);

            // Business operation
            var result = productService.CreateProduct(
                "Keyboard",
                50,
                1,"blue",null,"mechanical keyboard");

            if (result.IsSuccess)
            {
                Console.WriteLine("Product created successfully.");
                Console.WriteLine($"Id: {result.Data!.Id}");
                Console.WriteLine($"Name: {result.Data.Name}");
                Console.WriteLine($"Price: {result.Data.Price}");
                Console.WriteLine($"Price: {result.Data.Details.Color}");
                Console.WriteLine($"Price: {result.Data.Details.Weight}");
                Console.WriteLine($"Price: {result.Data.Details.Description}");
            }
            else
            {
                Console.WriteLine($"Failed: {result.Error}");
            }
            Console.WriteLine("E-Commerce Management System");
        }
    }
}

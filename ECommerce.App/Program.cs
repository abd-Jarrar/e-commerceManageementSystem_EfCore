using ECommerce.Business.Entities;
using ECommerce.Business.Results;
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
            var result = productService.GetAllProducts();
            if (result.IsSuccess)
            {
                foreach (var product in result.Data!)
                {
                    Console.WriteLine($"Id: {product.Id}");
                    Console.WriteLine($"Name: {product.Name}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Stock: {product.StockQuantity}");
                    Console.WriteLine($"Color: {product.Details.Color}");
                    Console.WriteLine($"Weight: {product.Details.Weight}");
                    Console.WriteLine($"Description: {product.Details.Description}");
                    Console.WriteLine("--------------------");
                }
            }
            else
            {
                Console.WriteLine($"Failed: {result.Error}");
            }


        }
    }
}

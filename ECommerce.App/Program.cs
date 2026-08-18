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

            var updateResult = productService.UpdateProduct(
    1,
    "Keyboard",
    100,
    1,
    "blue",
    null,
    "mechanical keyboard");

            if (updateResult.IsSuccess)
            {
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine($"Failed: {updateResult.Error}");
            }


            // Get the same product again
            var getResult = productService.GetProductById(1);

            if (getResult.IsSuccess)
            {
                var product = getResult.Data!;

                Console.WriteLine($"Id: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price}");
            }
            else
            {
                Console.WriteLine($"Failed: {getResult.Error}");
            }

        }

    }
}

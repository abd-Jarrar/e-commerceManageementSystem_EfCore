using ECommerce.Business.Entities;
using ECommerce.Business.Requests;
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
            var customerRepository = new CustomerRepository(context);
            var orderRepository = new OrderRepository(context);
            var reviewRepository = new ReviewRepository(context);
            var unitOfWork = new UnitOfWork(context);


            var productService = new ProductService(productRepository, categoryRepository, unitOfWork);
            var categoryService = new CategoryService(categoryRepository, unitOfWork);
            var customerService = new CustomerService(customerRepository, unitOfWork);
            var orderService = new OrderService(productRepository, orderRepository, customerRepository, unitOfWork);
            var reviewService = new ReviewService(reviewRepository, customerRepository, productRepository, unitOfWork);

            var result1 = orderService.MarkOrderAsDelivered(1001);
             var result2 = reviewService.CreateReview(
    customerId: 1,
    productId: 3,
    rating: 5,
    comment: "Excellent product!!!!!!");

            Console.WriteLine($"Success: {result2.IsSuccess}");

            if (result2.IsSuccess)
            {
                Console.WriteLine($"Review ID: {result2.Data!.Id}");
            }
            else
            {
                Console.WriteLine(result2.Error);
            }

        }
    }
}

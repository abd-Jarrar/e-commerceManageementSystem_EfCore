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
            var customerRepository =new CustomerRepository(context);
            var orderRepository=new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context);


            var productService = new ProductService(
                productRepository,
                categoryRepository,
                unitOfWork);

            var categoryService=new CategoryService(categoryRepository, unitOfWork);
            var customerService = new CustomerService(customerRepository, unitOfWork);
            var orderService = new OrderService(productRepository, orderRepository, customerRepository, unitOfWork);


            

        }

    }
}

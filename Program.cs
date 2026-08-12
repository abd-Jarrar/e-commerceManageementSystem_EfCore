using E_CommerceManagementSystemEfCore.Data;
using E_CommerceManagementSystemEfCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_CommerceManagementSystemEfCore
{
    public class Program
    {
        static bool IsExpensive(Product product)
        {
            return product.Price > 100;
        }
        static void Main(string[] args)
        {
            using(var context=new AppDbContext())
            {
                var products = context.Products.AsNoTracking();
                
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name}, {product.Price}");

                }
                var productWithoutPrice = context.Products.Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToList();//top level projection

                foreach (var product in productWithoutPrice)
                {
                    Console.WriteLine($"{product.Id}, {product.Name}");

                }

                var Categories = context.Categories.Include(x => x.Products).AsNoTracking();//eager loading
                context.Reviews.ExecuteUpdate(x => x.SetProperty(x => x.Rating , 8));//effecient update

                var name = "abood";
                var users = context.Users.
                    FromSqlInterpolated($"select * from users where FullName={name}")
                    .ToList();//pass sql parameter 


            }
        }
    }
}

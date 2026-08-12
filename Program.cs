using E_CommerceManagementSystemEfCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_CommerceManagementSystemEfCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(var context=new AppDbContext())
            {
                var products = context.Products.AsNoTracking();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name}, {product.Price}");

                }
            }
        }
    }
}

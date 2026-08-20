using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Database.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public int ApplyDiscount(Expression<Func<Product, bool>> condition,decimal discountPercentage)
        {
            return _context.Products
       .Where(condition)
       .ExecuteUpdate(setters => setters
           .SetProperty(
               p => p.Price,
               p => p.Price - p.Price * discountPercentage / 100));
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public List<Product> GetAll()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(p=>p.Id == id);
        }

        public Product? GetByName(string name)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(p => p.Name == name);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public bool TryDecreaseStock(int productId, int quantity)
        {
            var affectedRows = _context.Products
                .Where(p => p.Id == productId &&
                            p.StockQuantity >= quantity)
                .ExecuteUpdate(setters =>
                    setters.SetProperty(
                        p => p.StockQuantity,
                        p => p.StockQuantity - quantity));

            return affectedRows == 1;
        }
    }
}

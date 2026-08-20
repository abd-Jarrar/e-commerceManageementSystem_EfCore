using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Database.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) {
            _context = context;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.AsNoTracking().FirstOrDefault(c=>c.Id==id);
        }

        public Category? GetByName(string name)
        {
            return _context.Categories.AsNoTracking().FirstOrDefault(c => c.Name == name);

        }

        
        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
        public bool HasProducts(int categoryId)
        {
            return _context.Products
       .Any(p => p.CategoryId == categoryId);

        }

        public List<Category> GetCategories(Expression<Func<Category, bool>> condition)
        {
            return _context.Categories
                .AsNoTracking()
                .Where(condition)
                .ToList();
        }

        public List<Category> GetCategoriesByRevenue(decimal minimumRevenue)
        {
            return _context.Categories
                .Where(c =>
                    _context.OrderItems
                        .Where(oi =>
                            oi.Product.CategoryId == c.Id &&
                            (oi.Order.Status == OrderStatus.Paid ||
                             oi.Order.Status == OrderStatus.Shipped))
                        .Sum(oi => oi.Price * oi.Quantity) > minimumRevenue)
                .ToList();
        }
    }
}

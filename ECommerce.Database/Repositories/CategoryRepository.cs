using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c=>c.Id==id);
        }

        public Category? GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == name);

        }

        public void RemoveCategory(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}

using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface ICategoryRepository
    {
        Category? GetById(int id);
        Category? GetByName(string name);
        void Add(Category category);
        void Remove(Category category);

        List<Category> GetAll();

        void UpdateCategory(Category category);
        bool HasProducts(int categoryId);

    }
}

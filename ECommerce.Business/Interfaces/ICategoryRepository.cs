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
        void AddCategory(Category category);
        void RemoveCategory(Category category);

        List<Category> GetAll();



    }
}

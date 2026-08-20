using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface IProductRepository
    {
        Product? GetById(int id);

        Product? GetByName(string name);

        List<Product> GetAll();


        void Add(Product product);

        void Update(Product product);

        void Delete(Product product);

        int ApplyDiscount(Expression<Func<Product, bool>> condition,decimal discountPercentage);

        bool TryDecreaseStock(int productId, int quantity);
    }
}

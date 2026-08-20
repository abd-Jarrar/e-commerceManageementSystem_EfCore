using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface IOrderRepository
    {

        void Add(Order order);

        void Update(Order order);

        void Delete(Order order);

        Order? GetById(int id);

        List<Order> GetAll();

        List<Order> GetOrders(
            Expression<Func<Order, bool>> condition);

    }
}

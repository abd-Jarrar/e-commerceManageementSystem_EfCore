using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Delete(Customer customer);
        Customer? GetById(int id);
        List<Customer> GetAll();
        List<Customer> GetByName(string name);
        void Update(Customer customer);

        List<Order> GetOrders(int customerId, Expression<Func<Order, bool>> condition);

        public List<Customer> GetCustomers(Expression<Func<Customer, bool>> condition);

        public bool EmailExists(string email);
    }
}

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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context=context;
        }
        public void Add(Customer customer)
        {
            _context.Users.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Users.Remove(customer);
        }

        public List<Customer> GetAll()
        {
            return _context.Users.OfType<Customer>().ToList();
        }

        public Customer? GetById(int id)
        {
            return _context.Users.AsNoTracking().OfType<Customer>().FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetByName(string name)
        {
            return _context.Users
                .AsNoTracking()
                .OfType<Customer>()
                .Where(c => c.FullName == name)
                .ToList();
        }

        public List<Order> GetOrders(int customerId,Expression<Func<Order, bool>> condition)
        {
            return _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.CustomerId == customerId)
                .Where(condition)
                .ToList();
        }

        public List<Customer> GetCustomers(Expression<Func<Customer, bool>> condition)
        {
            return _context.Users
                .OfType<Customer>()
                .AsNoTracking()
                .Where(condition)
                .ToList();
        }
        public void Update(Customer customer)
        {
            _context.Users.Update(customer);
        }

        public bool EmailExists(string email)
        {
            return _context.Users
                .Any(u => u.Email == email);
        }

        public Customer? GetByIdForUpdate(int id)
        {
            return _context.Users
                .OfType<Customer>()
                .Include(c => c.CustomerProfile)
                .FirstOrDefault(c => c.Id == id);
        }

    }
}

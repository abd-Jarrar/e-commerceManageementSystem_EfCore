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
    public class OrderRepository : IOrderRepository
    {

        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) {
            _context=context;
        }
        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public List<Order> GetAll()
        {
            return _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetOrders(Expression<Func<Order, bool>> condition)
        {
            return _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .Where(condition)
            .ToList();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}

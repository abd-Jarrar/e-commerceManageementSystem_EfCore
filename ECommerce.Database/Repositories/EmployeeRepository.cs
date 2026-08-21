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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) {
            _context = context;

        }
        public void Add(Employee employee) {

            _context.Users.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _context.Users.Remove(employee);
        }

        public List<Employee> GetAll()
        {
            return _context.Users.AsNoTracking().OfType<Employee>().ToList();
        }

        public Employee? GetByEmail(string email)
        {
            return _context.Users.AsNoTracking().OfType<Employee>().FirstOrDefault(e => e.Email == email);
        }

        public Employee? GetById(int id)
        {
            return _context.Users.AsNoTracking().OfType<Employee>().FirstOrDefault(e=>e.Id==id);
        }

        public Employee? GetByPhoneNumber(string phoneNumber) {

            return _context.Users.AsNoTracking().OfType<Employee>().FirstOrDefault(e=>e.PhoneNumber==phoneNumber);
        }

        public List<Employee> GetEmployees(Expression<Func<Employee, bool>> condition)
        {
            return _context.Users.AsNoTracking().OfType<Employee>().Where(condition).ToList();
        }

        public void Update(Employee employee)
        {
            _context.Users.Update(employee);
        }

        
    }
}

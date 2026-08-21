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
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Admin admin)
        {
            _context.Users.Add(admin);
        }

        public void Delete(Admin admin)
        {
            _context.Users.Remove(admin);
        }

        public List<Admin> GetAll()
        {
            return _context.Users
                .AsNoTracking()
                .OfType<Admin>()
                .ToList();
        }

        public Admin? GetByEmail(string email)
        {
            return _context.Users
                .AsNoTracking()
                .OfType<Admin>()
                .FirstOrDefault(a => a.Email == email);
        }

        public Admin? GetById(int id)
        {
            return _context.Users
                .AsNoTracking()
                .OfType<Admin>()
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Admin> GetAdmins(
            Expression<Func<Admin, bool>> condition)
        {
            return _context.Users
                .AsNoTracking()
                .OfType<Admin>()
                .Where(condition)
                .ToList();
        }

        public void Update(Admin admin)
        {
            _context.Users.Update(admin);
        }
        public bool PromoteToAdmin(int employeeId, string role)
        {
            return _context.Database.ExecuteSqlInterpolated($@"
        UPDATE Users
        SET UserType = 'Admin',
            Role = {role}
        WHERE Id = {employeeId}") > 0;
        }
    }
}

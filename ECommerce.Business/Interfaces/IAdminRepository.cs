using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface IAdminRepository
    {
        void Add(Admin admin);

        Admin? GetById(int id);

        Admin? GetByEmail(string email);

        List<Admin> GetAll();

        List<Admin> GetAdmins(
            Expression<Func<Admin, bool>> condition);

        void Update(Admin admin);

        void Delete(Admin admin);

        bool PromoteToAdmin(int employeeId, string role);
    }
}

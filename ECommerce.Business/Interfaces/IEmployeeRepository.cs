using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        Employee? GetById(int id);

        List<Employee> GetAll();

        Employee? GetByEmail(string email);

        List<Employee> GetEmployees(
            Expression<Func<Employee, bool>> condition);

        void Update(Employee employee);

        void Delete(Employee employee);

        Employee? GetByPhoneNumber(string phoneNumber);
    }
}

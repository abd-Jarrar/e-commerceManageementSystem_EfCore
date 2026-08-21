using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository,IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }
        public Result<Employee> CreateEmployee(string fullName,string email,string phoneNumber,decimal salary)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Result<Employee>.Failure(
                    "Employee name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Employee>.Failure(
                    "Employee email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result<Employee>.Failure(
                    "Employee phone number cannot be empty.");
            }

            if (salary <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee salary must be greater than zero.");
            }

            email = email.Trim();
            phoneNumber = phoneNumber.Trim();

            var existingEmployee = _employeeRepository.GetByEmail(email);

            if (existingEmployee is not null)
            {
                return Result<Employee>.Failure(
                    "An employee with this email already exists.");
            }

            existingEmployee = _employeeRepository.GetByPhoneNumber(phoneNumber);

            if (existingEmployee is not null)
            {
                return Result<Employee>.Failure(
                    "An employee with this phone number already exists.");
            }

            var employee = new Employee
            {
                FullName = fullName.Trim(),
                Email = email,
                PhoneNumber = phoneNumber,
                Salary = salary,
                HireDate = DateTime.Now
            };

            _employeeRepository.Add(employee);
            _unitOfWork.SaveChanges();

            return Result<Employee>.Success(employee);
        }
        public Result<Employee> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee ID must be greater than zero.");
            }

            var employee = _employeeRepository.GetById(id);

            if (employee is null)
            {
                return Result<Employee>.Failure(
                    "Employee not found.");
            }

            return Result<Employee>.Success(employee);
        }

        public Result<Employee> GetEmployeeByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Employee>.Failure(
                    "Employee email cannot be empty.");
            }

            var employee = _employeeRepository.GetByEmail(email.Trim());

            if (employee is null)
            {
                return Result<Employee>.Failure(
                    "Employee not found.");
            }

            return Result<Employee>.Success(employee);
        }


        public Result<List<Employee>> GetEmployees(Expression<Func<Employee, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Employee>>.Failure(
                    "Employee condition cannot be null.");
            }

            var employees = _employeeRepository.GetEmployees(condition);

            return Result<List<Employee>>.Success(employees);
        }

        public Result<List<Employee>> GetAllEmployees()
        {
            return Result<List<Employee>>.Success(
                _employeeRepository.GetAll());
        }

        public Result<Employee> DeleteEmployeeById(int id)
        {
            if (id <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee ID must be greater than zero.");
            }

            var employee = _employeeRepository.GetById(id);

            if (employee is null)
            {
                return Result<Employee>.Failure(
                    "Employee not found.");
            }

            _employeeRepository.Delete(employee);
            _unitOfWork.SaveChanges();

            return Result<Employee>.Success(employee);
        }

        public Result<Employee> UpdateSalary(int id, decimal salary)
        {
            if (id <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee ID must be greater than zero.");
            }

            if (salary <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee salary must be greater than zero.");
            }

            var employee = _employeeRepository.GetById(id);

            if (employee is null)
            {
                return Result<Employee>.Failure(
                    "Employee not found.");
            }

            employee.Salary = salary;

            _employeeRepository.Update(employee);
            _unitOfWork.SaveChanges();

            return Result<Employee>.Success(employee);
        }

        public Result<Employee> UpdateEmployee(int id,string fullName,string email,string phoneNumber,decimal salary,DateTime hireDate)
        {
            if (id <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Result<Employee>.Failure(
                    "Employee name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Employee>.Failure(
                    "Employee email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result<Employee>.Failure(
                    "Employee phone number cannot be empty.");
            }

            if (salary <= 0)
            {
                return Result<Employee>.Failure(
                    "Employee salary must be greater than zero.");
            }

            if (hireDate > DateTime.Now)
            {
                return Result<Employee>.Failure(
                    "Hire date cannot be in the future.");
            }

            var employee = _employeeRepository.GetById(id);

            if (employee is null)
            {
                return Result<Employee>.Failure(
                    "Employee not found.");
            }

            email = email.Trim();
            phoneNumber = phoneNumber.Trim();

            var existingEmployee = _employeeRepository.GetByEmail(email);

            if (existingEmployee is not null && existingEmployee.Id != id)
            {
                return Result<Employee>.Failure(
                    "An employee with this email already exists.");
            }
            existingEmployee = _employeeRepository.GetByPhoneNumber(phoneNumber);
            if (existingEmployee is not null)
            {
                return Result<Employee>.Failure(
                    "An employee with this phone number already exists.");
            }

            employee.FullName = fullName.Trim();
            employee.Email = email;
            employee.PhoneNumber = phoneNumber;
            employee.Salary = salary;
            employee.HireDate = hireDate;

            _employeeRepository.Update(employee);
            _unitOfWork.SaveChanges();

            return Result<Employee>.Success(employee);
        }
    }
}

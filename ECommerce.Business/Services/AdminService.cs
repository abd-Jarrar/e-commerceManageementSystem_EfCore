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
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(
            IAdminRepository adminRepository,IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public Result<bool> PromoteToAdmin(int employeeId, string role)
        {
            if (employeeId <= 0)
            {
                return Result<bool>.Failure(
                    "Employee ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                return Result<bool>.Failure(
                    "Admin role cannot be empty.");
            }

            var employee = _employeeRepository.GetById(employeeId);

            if (employee is null)
            {
                return Result<bool>.Failure(
                    "Employee not found.");
            }

            var promoted = _adminRepository.PromoteToAdmin(
                employeeId,
                role.Trim());

            if (!promoted)
            {
                return Result<bool>.Failure(
                    "Failed to promote employee.");
            }

            return Result<bool>.Success(true);
        }

        public Result<Admin> GetAdminById(int id)
        {
            if (id <= 0)
            {
                return Result<Admin>.Failure(
                    "Admin ID must be greater than zero.");
            }

            var admin = _adminRepository.GetById(id);

            if (admin is null)
            {
                return Result<Admin>.Failure(
                    "Admin not found.");
            }

            return Result<Admin>.Success(admin);
        }

        public Result<Admin> GetAdminByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Admin>.Failure(
                    "Admin email cannot be empty.");
            }

            var admin = _adminRepository.GetByEmail(email.Trim());

            if (admin is null)
            {
                return Result<Admin>.Failure(
                    "Admin not found.");
            }

            return Result<Admin>.Success(admin);
        }

        public Result<List<Admin>> GetAllAdmins()
        {
            return Result<List<Admin>>.Success(
                _adminRepository.GetAll());
        }

        public Result<List<Admin>> GetAdmins(Expression<Func<Admin, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Admin>>.Failure(
                    "Admin condition cannot be null.");
            }

            var admins = _adminRepository.GetAdmins(condition);

            return Result<List<Admin>>.Success(admins);
        }

        public Result<Admin> UpdateAdminRole(int id, string role)
        {
            if (id <= 0)
            {
                return Result<Admin>.Failure(
                    "Admin ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                return Result<Admin>.Failure(
                    "Admin role cannot be empty.");
            }

            var admin = _adminRepository.GetById(id);

            if (admin is null)
            {
                return Result<Admin>.Failure(
                    "Admin not found.");
            }

            admin.Role = role.Trim();

            _adminRepository.Update(admin);
            _unitOfWork.SaveChanges();

            return Result<Admin>.Success(admin);
        }

        public Result<Admin> DeleteAdminById(int id)
        {
            if (id <= 0)
            {
                return Result<Admin>.Failure(
                    "Admin ID must be greater than zero.");
            }

            var admin = _adminRepository.GetById(id);

            if (admin is null)
            {
                return Result<Admin>.Failure(
                    "Admin not found.");
            }

            _adminRepository.Delete(admin);
            _unitOfWork.SaveChanges();

            return Result<Admin>.Success(admin);
        }
    }
}

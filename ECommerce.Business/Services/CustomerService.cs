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
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository customerRepository,IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public Result<Customer> CreateCustomer(string fullName,string email,string city,string street,string buildingNumber,string? postalCode,string phoneNumber,DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Result<Customer>.Failure(
                    "Customer name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Customer>.Failure(
                    "Customer email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                return Result<Customer>.Failure(
                    "Customer city cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                return Result<Customer>.Failure(
                    "Customer street cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(buildingNumber))
            {
                return Result<Customer>.Failure(
                    "Customer building number cannot be empty.");
            }


            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result<Customer>.Failure(
                    "Customer phone number cannot be empty.");
            }

            if (birthDate > DateTime.Today)
            {
                return Result<Customer>.Failure(
                    "Birth date cannot be in the future.");
            }

            if (_customerRepository.EmailExists(email.Trim()))
            {
                return Result<Customer>.Failure(
                    "Email is already registered.");
            }
            var customer = new Customer
            {
                FullName = fullName.Trim(),
                Email = email.Trim(),

                Address = new Address
                {
                    City = city.Trim(),
                    Street = street.Trim(),
                    BuildingNumber = buildingNumber.Trim(),
                    PostalCode = postalCode?.Trim()
                },

                CustomerProfile = new CustomerProfile
                {
                    PhoneNumber = phoneNumber.Trim(),
                    BirthDate = birthDate
                }
            };

            _customerRepository.Add(customer);
            _unitOfWork.SaveChanges();

            return Result<Customer>.Success(customer);
        }

        public Result<Customer> GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return Result<Customer>.Failure(
                    "Customer ID must be greater than zero.");
            }

            var customer = _customerRepository.GetById(id);

            if (customer is null)
            {
                return Result<Customer>.Failure(
                    "Customer not found.");
            }

            return Result<Customer>.Success(customer);
        }

        public Result<List<Customer>> GetCustomersByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<List<Customer>>.Failure(
                    "Customer name cannot be empty.");
            }

            var customers = _customerRepository.GetByName(name.Trim());

            return Result<List<Customer>>.Success(customers);
        }

        public Result<List<Customer>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAll();

            return Result<List<Customer>>.Success(customers);
        }

        public Result<List<Customer>> GetCustomers(Expression<Func<Customer, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Customer>>.Failure(
                    "Customer condition cannot be null.");
            }

            var customers = _customerRepository.GetCustomers(condition);

            return Result<List<Customer>>.Success(customers);
        }

        public Result<List<Order>> GetCustomerOrders(int customerId,Expression<Func<Order, bool>> condition)
        {
            if (customerId <= 0)
            {
                return Result<List<Order>>.Failure(
                    "Customer ID must be greater than zero.");
            }

            if (condition is null)
            {
                return Result<List<Order>>.Failure(
                    "Order condition cannot be null.");
            }

            var customer = _customerRepository.GetById(customerId);

            if (customer is null)
            {
                return Result<List<Order>>.Failure(
                    "Customer not found.");
            }

            var orders = _customerRepository.GetOrders(
                customerId,
                condition);

            return Result<List<Order>>.Success(orders);
        }

        public Result<Customer> UpdateCustomer(int id, string fullName, string email, string city, string street, string buildingNumber, string? postalCode, string phoneNumber, DateTime birthDate) {
            if (id <= 0) {
                return Result<Customer>.Failure("Customer ID must be greater than zero."); 
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Result<Customer>.Failure("Customer name cannot be empty."); 
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<Customer>.Failure("Customer email cannot be empty."); 
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                return Result<Customer>.Failure("Customer city cannot be empty."); 
            } 

            if (string.IsNullOrWhiteSpace(street))
            {
                return Result<Customer>.Failure("Customer street cannot be empty."); 
            }

            if (string.IsNullOrWhiteSpace(buildingNumber))
            {
                return Result<Customer>.Failure("Customer building number cannot be empty."); 
            } 

            if (string.IsNullOrWhiteSpace(phoneNumber)) 
            {
                return Result<Customer>.Failure("Customer phone number cannot be empty."); 
            } 

            if (birthDate > DateTime.Today)
            {
                return Result<Customer>.Failure("Birth date cannot be in the future.");
            }

            var customer = _customerRepository.GetByIdForUpdate(id);

            if (customer is null) {
                return Result<Customer>.Failure("Customer not found."); 
            } 

            email = email.Trim();
            if (_customerRepository.EmailExists(email)) {
                return Result<Customer>.Failure("Email is already registered."); 
            } 
            customer.FullName = fullName.Trim();
            customer.Email = email;
            customer.Address.City = city.Trim();
            customer.Address.Street = street.Trim();
            customer.Address.BuildingNumber = buildingNumber.Trim();
            customer.Address.PostalCode = postalCode?.Trim();
            customer.CustomerProfile.PhoneNumber = phoneNumber.Trim();
            customer.CustomerProfile.BirthDate = birthDate;
            _customerRepository.Update(customer);
            _unitOfWork.SaveChanges();
            return Result<Customer>.Success(customer); }

        public Result<Customer> DeleteCustomerById(int id)
        {
            if (id <= 0)
            {
                return Result<Customer>.Failure(
                    "Customer ID must be greater than zero.");
            }

            var customer = _customerRepository.GetById(id);

            if (customer is null)
            {
                return Result<Customer>.Failure(
                    "Customer not found.");
            }

            _customerRepository.Delete(customer);
            _unitOfWork.SaveChanges();

            return Result<Customer>.Success(customer);
        }
    }
}

using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public Result<Product> CreateProduct(string name,decimal price,int categoryId)
        {
            
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Product>.Failure(
                    "Product name cannot be empty.");
            }
            if (price <= 0)
            {
                return Result<Product>.Failure(
                    "Product price must be greater than zero.");

            }
            var category = _categoryRepository.GetById(categoryId);
            if (category is null)
            {
                return Result<Product>.Failure(
                    "The specified category does not exist.");
            }
            var product = new Product
            {
                Name = name.Trim(),
                Price = price,
                StockQuantity = 0,
                CategoryId = categoryId
            };
            _productRepository.Add(product);
             return Result<Product>.Success(product);
        }
    }
}

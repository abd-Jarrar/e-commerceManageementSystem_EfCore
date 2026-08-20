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
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        private Result<Product> GetProductForOperation(int productId)
        {
            if (productId <= 0)
            {
                return Result<Product>.Failure(
                    "Product ID must be greater than zero.");
            }

            var product = _productRepository.GetById(productId);

            if (product is null)
            {
                return Result<Product>.Failure(
                    "Product not found.");
            }

            return Result<Product>.Success(product);
        }
        public Result<Product> CreateProduct(string name,decimal price,int categoryId, string color,decimal? weight,string description)
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
            if (string.IsNullOrWhiteSpace(color))
            {
                return Result<Product>.Failure(
                    "Product color cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Product>.Failure(
                    "Product description cannot be empty.");
            }
            
            var product = new Product
            {
                Name = name.Trim(),
                Price = price,
                StockQuantity = 0,
                CategoryId = categoryId,
                Details = new ProductDetails
                {
                    Color = color.Trim(),
                    Weight = weight,
                    Description = description.Trim()
                }

            };
            _productRepository.Add(product);
            _unitOfWork.SaveChanges();
            return Result<Product>.Success(product);
        }

        public Result<Product> GetProductById(int id)
        {
            if (id <= 0)
            {
                return Result<Product>.Failure(
                    "Product ID must be greater than zero.");
            }

            var product = _productRepository.GetById(id);
            if (product is null)
            {
                return Result<Product>.Failure(
                    "Product not found.");
            }
            return Result<Product>.Success(product);
        }

        public Result<Product> GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Product>.Failure(
                    "Product name cannot be empty.");
            }

            var product = _productRepository.GetByName(name);
            if (product is null)
            {
                return Result<Product>.Failure(
                    "Product not found.");
            }
            return Result<Product>.Success(product);
        }
        public Result<List<Product>> GetAllProducts()
        {
            return  Result<List<Product>>.Success( _productRepository.GetAll());
        }
        public Result<Product> UpdateProduct(int id,string name,decimal price,int categoryId,string color,decimal? weight,string description)
        {
            if (id <= 0)
            {
                return Result<Product>.Failure(
                    "Product ID must be greater than zero.");
            }

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

            if (string.IsNullOrWhiteSpace(color))
            {
                return Result<Product>.Failure(
                    "Product color cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Product>.Failure(
                    "Product description cannot be empty.");
            }

            var product = _productRepository.GetById(id);

            if (product is null)
            {
                return Result<Product>.Failure(
                    "Product not found.");
            }

            var category = _categoryRepository.GetById(categoryId);

            if (category is null)
            {
                return Result<Product>.Failure(
                    "The specified category does not exist.");
            }

            product.Name = name.Trim();
            product.Price = price;
            product.CategoryId = categoryId;

            product.Details.Color = color.Trim();
            product.Details.Weight = weight;
            product.Details.Description = description.Trim();

            _productRepository.Update(product);
            _unitOfWork.SaveChanges();

            return Result<Product>.Success(product);
        }

        public Result<Product> DeactivateProductById(int id)
        {
            if (id <= 0)
            {
                return Result<Product>.Failure(
                    "Product ID must be greater than zero.");
            }

            var result = GetProductForOperation(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var product = result.Data!;
            if (product.IsDeleted)
            {
                return Result<Product>.Failure(
                    "Product is already inactive.");
            }
            _productRepository.Delete(product);
            _unitOfWork.SaveChanges();

            return Result<Product>.Success(product);
        }

        public Result<List<Product>> GetProducts( Expression<Func<Product, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Product>>.Failure(
                    "Product condition cannot be null.");
            }

            var products = _productRepository.GetProducts(condition);

            return Result<List<Product>>.Success(products);
        }
        public Result<Product> AddStock(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return Result<Product>.Failure(
                    "Stock quantity must be greater than zero.");
            }

            var result = GetProductForOperation(productId);

            if (!result.IsSuccess)
            {
                return result;
            }

            var product = result.Data!;

            if (product.IsDeleted)
            {
                return Result<Product>.Failure(
                    "Cannot add stock to an inactive product.");
            }

            product.StockQuantity += quantity;

            _productRepository.Update(product);
            _unitOfWork.SaveChanges();

            return Result<Product>.Success(product);
        }
        public Result<int> ApplyDiscount(Expression<Func<Product, bool>> condition,decimal discountPercentage)
        {
            if (discountPercentage <= 0 || discountPercentage >= 100)
            {
                return Result<int>.Failure(
                    "Discount must be between 0 and 100.");
            }
            int affectedProducts = _productRepository.ApplyDiscount(condition,discountPercentage);
            _unitOfWork.SaveChanges();
            return Result<int>.Success(affectedProducts);
        }
    }
}

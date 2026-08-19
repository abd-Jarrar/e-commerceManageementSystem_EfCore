using ECommerce.Business.Entities;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public Result<Category> CreateCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Category>.Failure(
                    "Category name cannot be empty.");
            }
            var category=_categoryRepository.GetByName(name);
            if(category != null)
            {
                return Result<Category>.Failure(
                    "Category already exist.");
            }
            category = new Category { Name = name };
            _categoryRepository.Add(category);
            _unitOfWork.SaveChanges();
            return Result<Category>.Success(category);
        }

        public Result<Category> GetCategoryById(int id)
        {
            if (id <= 0)
            {
                return Result<Category>.Failure(
                    "Category ID must be greater than zero.");
            }
            var category= _categoryRepository.GetById(id);
            if (category is null)
            {
                return Result<Category>.Failure(
                    "Category not found.");
            }
            return Result<Category>.Success(category);
        }

        public Result<Category> GetCategoryByName(string name)
        {
            name = name.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Category>.Failure(
                    "Category name cannot be empty.");
            }
            var category = _categoryRepository.GetByName(name);
            if (category is null)
            {
                return Result<Category>.Failure(
                    "Category not found.");
            }
            return Result<Category>.Success(category);
        }

        public Result<List<Category>> GetAllCategories()
        {
            return Result<List<Category>>.Success(_categoryRepository.GetAll());
        }

        public Result<Category> UpdateCategory(int id, string name)
        {
            if (id <= 0)
            {
                return Result<Category>.Failure(
                    "Category ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return Result<Category>.Failure(
                    "Category name cannot be empty.");
            }
            var category = _categoryRepository.GetById(id);

            if (category is null)
            {
                return Result<Category>.Failure(
                    "category not found.");
            }

            var existingCategory = _categoryRepository.GetByName(name);
            if (existingCategory != null && existingCategory.Id != id)
            {
                return Result<Category>.Failure(
                    "Category already exists.");
            }
            category.Name = name.Trim();
            _categoryRepository.UpdateCategory(category);
            _unitOfWork.SaveChanges();

            return Result<Category>.Success(category);
        }

        public Result<Category> DeleteCategoryById(int id) {
            if (id <= 0)
            {
                return Result<Category>.Failure(
                    "Category ID must be greater than zero.");
            }
            var category = _categoryRepository.GetById(id);

            if (category is null)
            {
                return Result<Category>.Failure(
                    "category not found.");
            }
            if (_categoryRepository.HasProducts(id))
            {
                return Result<Category>.Failure(
                   "category has products!!.");
            }
            _categoryRepository.Remove(category);
            _unitOfWork.SaveChanges();
            return Result<Category>.Success(category);
        }

    }
}

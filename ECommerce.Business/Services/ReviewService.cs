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
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(
            IReviewRepository reviewRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Result<Review> CreateReview(int customerId,int productId,int rating,string comment)
        {
            if (customerId <= 0)
            {
                return Result<Review>.Failure(
                    "Customer ID must be greater than zero.");
            }

            if (productId <= 0)
            {
                return Result<Review>.Failure(
                    "Product ID must be greater than zero.");
            }

            if (rating < 1 || rating > 5)
            {
                return Result<Review>.Failure(
                    "Rating must be between 1 and 5.");
            }


            var customer = _customerRepository.GetById(customerId);

            if (customer is null)
            {
                return Result<Review>.Failure(
                    "Customer not found.");
            }

            var product = _productRepository.GetById(productId);

            if (product is null)
            {
                return Result<Review>.Failure(
                    "Product not found.");
            }

            var purchased = _reviewRepository
                .CustomerPurchasedProduct(customerId, productId);

            if (!purchased)
            {
                return Result<Review>.Failure("Customer can only review products they have purchased and received.");
            }

            var review = new Review
            {
                CustomerId = customerId,
                ProductId = productId,
                Rating = rating,
                Comment = comment?.Trim() ?? "",
                CreatedDate = DateTime.Now
            };

            _reviewRepository.Add(review);
            _unitOfWork.SaveChanges();

            return Result<Review>.Success(review);
        }

        public Result<Review> GetReviewById(int id)
        {
            if (id <= 0)
            {
                return Result<Review>.Failure(
                    "Review ID must be greater than zero.");
            }

            var review = _reviewRepository.GetById(id);

            if (review is null)
            {
                return Result<Review>.Failure(
                    "Review not found.");
            }

            return Result<Review>.Success(review);
        }

        public Result<List<Review>> GetAllReviews()
        {
            var reviews = _reviewRepository.GetAll();

            return Result<List<Review>>.Success(reviews);
        }

        public Result<List<Review>> GetProductReviews(int productId)
        {
            if (productId <= 0)
            {
                return Result<List<Review>>.Failure(
                    "Product ID must be greater than zero.");
            }

            var product = _productRepository.GetById(productId);

            if (product is null)
            {
                return Result<List<Review>>.Failure(
                    "Product not found.");
            }

            var reviews = _reviewRepository.GetByProductId(productId);

            return Result<List<Review>>.Success(reviews);
        }

        public Result<List<Review>> GetCustomerReviews(int customerId)
        {
            if (customerId <= 0)
            {
                return Result<List<Review>>.Failure(
                    "Customer ID must be greater than zero.");
            }

            var customer = _customerRepository.GetById(customerId);

            if (customer is null)
            {
                return Result<List<Review>>.Failure(
                    "Customer not found.");
            }

            var reviews = _reviewRepository.GetByCustomerId(customerId);

            return Result<List<Review>>.Success(reviews);
        }
        public Result<List<Review>> GetReviews(Expression<Func<Review, bool>> condition)
        {
            if (condition is null)
            {
                return Result<List<Review>>.Failure(
                    "Review condition cannot be null.");
            }

            var reviews = _reviewRepository.GetReviews(condition);

            return Result<List<Review>>.Success(reviews);
        }

        public Result<Review> UpdateReview(int id,int rating,string comment)
        {
            if (id <= 0)
            {
                return Result<Review>.Failure(
                    "Review ID must be greater than zero.");
            }

            if (rating < 1 || rating > 5)
            {
                return Result<Review>.Failure(
                    "Rating must be between 1 and 5.");
            }

            var review = _reviewRepository.GetById(id);

            if (review is null)
            {
                return Result<Review>.Failure(
                    "Review not found.");
            }

            review.Rating = rating;
            review.Comment = comment?.Trim() ?? "";

            _reviewRepository.Update(review);
            _unitOfWork.SaveChanges();

            return Result<Review>.Success(review);
        }

        public Result<Review> DeleteReviewById(int id)
        {
            if (id <= 0)
            {
                return Result<Review>.Failure(
                    "Review ID must be greater than zero.");
            }

            var review = _reviewRepository.GetById(id);

            if (review is null)
            {
                return Result<Review>.Failure(
                    "Review not found.");
            }

            _reviewRepository.Delete(review);
            _unitOfWork.SaveChanges();

            return Result<Review>.Success(review);
        }
    }
}

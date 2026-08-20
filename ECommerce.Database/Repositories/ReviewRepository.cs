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
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }

        public Review? GetById(int id)
        {
            return _context.Reviews.AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Review> GetAll()
        {
            return _context.Reviews.AsNoTracking()
                .ToList();
        }

        public List<Review> GetByProductId(int productId)
        {
            return _context.Reviews.AsNoTracking()
                .Where(x => x.ProductId == productId)
                .ToList();
        }

        public List<Review> GetByCustomerId(int customerId)
        {
            return _context.Reviews.AsNoTracking()
                .Where(x => x.CustomerId == customerId)
                .ToList();
        }

        public bool CustomerPurchasedProduct(int customerId, int productId)
        {
            return _context.Orders
                .Any(o =>
                    o.CustomerId == customerId &&
                    o.Status == OrderStatus.Shipped &&
                    o.Items.Any(i => i.ProductId == productId));
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }

        public void Delete(Review review)
        {
            _context.Reviews.Remove(review);
        }
        public List<Review> GetReviews(Expression<Func<Review, bool>> condition)
        {
            return _context.Reviews
                .AsNoTracking()
                .Where(condition)
                .ToList();
        }
    }
}

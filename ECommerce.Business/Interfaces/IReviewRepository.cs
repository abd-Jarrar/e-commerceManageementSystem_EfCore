using ECommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Interfaces
{
    public interface IReviewRepository
    {
        void Add(Review review);

        Review? GetById(int id);

        List<Review> GetAll();

        List<Review> GetByProductId(int productId);

        List<Review> GetByCustomerId(int customerId);

        bool CustomerPurchasedProduct(int customerId, int productId);

        void Update(Review review);

        void Delete(Review review);
        List<Review> GetReviews(Expression<Func<Review, bool>> condition);
    }
}

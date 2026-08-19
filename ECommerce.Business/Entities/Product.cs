using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ProductDetails Details { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}

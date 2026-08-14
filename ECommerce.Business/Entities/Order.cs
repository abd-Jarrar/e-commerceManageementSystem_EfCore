using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderStatus Status { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    }

    
}

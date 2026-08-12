using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderStatus Status { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = new();

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } = "";

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

    }
}

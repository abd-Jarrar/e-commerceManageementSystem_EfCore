using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class Customer:User
    {
        public Address Address { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public CustomerProfile CustomerProfile { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}

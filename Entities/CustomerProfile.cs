using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class CustomerProfile
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; } = "";

        public DateTime BirthDate { get; set; }

        public Customer Customer { get; set; } = null!;
    }
}

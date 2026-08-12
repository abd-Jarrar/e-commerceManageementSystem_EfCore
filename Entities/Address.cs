using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class Address
    {
        public string City { get; set; } = "";
        public string Street { get; set; } = "";
        public string BuildingNumber { get; set; } = "";
        public string? PostalCode { get; set; } = "";
    }
}

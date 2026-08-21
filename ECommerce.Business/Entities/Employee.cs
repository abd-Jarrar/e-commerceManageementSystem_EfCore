using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Entities
{
    public class Employee:User
    {
        public string PhoneNumber { get; set; } = "";
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }
    }
}

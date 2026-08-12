using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceManagementSystemEfCore.Entities
{
    public class Employee:User
    {
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }
    }
}

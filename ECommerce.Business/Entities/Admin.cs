using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Entities
{
    public class Admin:Employee
    {
        public string Role { get; set; } = "";
    }
}

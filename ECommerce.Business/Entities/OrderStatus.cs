using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Entities
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Cancelled,
    }
}

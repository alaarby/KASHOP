using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Canceled,
        Approved,
        Shipped,
        Delivered
    }
}

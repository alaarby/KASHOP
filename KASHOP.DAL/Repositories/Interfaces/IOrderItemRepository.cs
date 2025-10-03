using KASHOP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeAsync(List<OrderItem> items);
    }
}

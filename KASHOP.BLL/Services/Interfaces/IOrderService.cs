using KASHOP.DAL.Entities;
using KASHOP.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderIdAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus);
        Task<List<Order>> GetBySatatusAsync(OrderStatus status);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}

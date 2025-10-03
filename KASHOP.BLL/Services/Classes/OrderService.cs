using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Enums;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> AddAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, newStatus);
        }

        public Task<List<Order>> GetBySatatusAsync(OrderStatus status)
        {
            return _orderRepository.GetBySatatusAsync(status);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<Order?> GetUserByOrderIdAsync(int orderId)
        {
            return await _orderRepository.GetUserByOrderIdAsync(orderId);
        }
    }
}

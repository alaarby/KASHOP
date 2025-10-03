using KASHOP.DAL.Data;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Enums;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrderIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }  
        public async Task<List<Order>> GetBySatatusAsync(OrderStatus status)
        {
            return await _context.Orders.Where(o => o.Status == status).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.Status = newStatus;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders.Include(o => o.Items)
                .AnyAsync(e => e.UserId == userId && e.Status == OrderStatus.Approved && 
                e.Items.Any(i => i.ProductId == productId));
        }
    }
}

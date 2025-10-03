using KASHOP.DAL.Data;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            var items = await _context.Carts.Where(c => c.UserId == UserId).ToListAsync();

            _context.Carts.RemoveRange(items);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> GetUserCartAsync(string UserId)
        {
            return await _context.Carts.Include(c => c.Product)
                    .Where(c => c.UserId == UserId).ToListAsync();
        }
    }
}

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
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewedProduct(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public async Task<bool> AddReviewAsync(Review request, string userId)
        {
            request.UserId = userId;
            request.ReviewDate = DateTime.Now;
            await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequest request, string userId)
        {
            var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(userId, request.ProductId);
            if (!hasOrder) return false;
            var alreadyReviews = await _reviewRepository.HasUserReviewedProduct(userId, request.ProductId);
            if(alreadyReviews) return false;

            var review = request.Adapt<Review>();
            await _reviewRepository.AddReviewAsync(review, userId);

            return true;
        }
    }
}

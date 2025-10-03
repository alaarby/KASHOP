using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCartAsync(CartRequest request, string UserId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = UserId,
                Count = 1
            };

            return await _cartRepository.AddAsync(newItem) > 0;
        }

        public async Task<CartSummeryResponse> CartSummeryResponseAsync(string UserId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            var response = new CartSummeryResponse
            {
                Items = cartItems.Select(c => new CartResponse
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Count = c.Count,
                    Price = c.Product.Price,
                }).ToList()
            };

            return response;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.ClearCartAsync(userId);
        }
    }
}

using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartRequest cartRequest, string UserId);
        Task<CartSummeryResponse> CartSummeryResponseAsync (string UserId);
        Task<bool> ClearCartAsync(string userId);
    }
}

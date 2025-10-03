using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KASHOP.API.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartRequest request)
        {
            var userId = User.FindFirstValue("Id");
            var result = await _cartService.AddToCartAsync(request, userId);

            return result ? Ok() : BadRequest(); 
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = User.FindFirstValue("Id");
            var result = await _cartService.CartSummeryResponseAsync(userId);

            return Ok(result); 
        }
    }
}

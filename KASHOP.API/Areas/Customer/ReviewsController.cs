using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.API.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest request)
        {
            var userId = User.FindFirstValue("Id");
            var result = await _reviewService.AddReviewAsync(request, userId);
            return Ok(result);
        }
    }
}

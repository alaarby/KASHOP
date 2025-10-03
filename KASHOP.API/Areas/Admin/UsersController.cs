using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.API.Areas.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,SuperAdmin")]
    [Area("Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUSerById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }
        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            return Ok(result);
        }
        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsync(userId);
            return Ok(result);
        }
        [HttpPatch("isBlocked/{userId}")]
        public async Task<IActionResult> IsBlocked([FromRoute] string userId)
        {
            var result = await _userService.IsBlockedAsync(userId);
            return Ok(result);
        }
        [HttpPatch("change-role/{userId}")]
        public async Task<IActionResult> ChangeRole([FromRoute] string userId, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, request);
            return Ok(result);
        }
    }
}

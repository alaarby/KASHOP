using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KASHOP.API.Areas.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles =("Admin,SuperAdmin"))]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrderByStatus([FromRoute]OrderStatus status)
        { 
            var orders = await _orderService.GetBySatatusAsync(status);
            return Ok(orders);
        }
        [HttpPatch("change-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody]OrderStatus status)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, status);
            return Ok(result);
        }

    }
}

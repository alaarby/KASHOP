using KASHOP.BLL.Interfaces;
using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.API.Areas.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles =("Admin, SuperAdmin"))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll(true));

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _service.GetById(id);
            if (category == null) return NotFound();

            return Ok(category);
        }
    }
}

using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.API.Areas.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    //[Authorize(Roles ="Customer")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;
        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll(true));
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _service.GetById(id);
            if (brand == null) return NotFound();

            return Ok(brand);
        }
    }
}

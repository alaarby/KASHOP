using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.API.Areas.Admin
{
    [Route("api/[area]/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize(Roles =("Admin, SuperAdmin"))]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;
        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _service.GetById(id);
            if (brand == null) return NotFound();

            return Ok(brand);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] BrandRequest request)
        {
            var id = _service.CreateFile(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { data = request });
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest request)
        {
            var updated = _service.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }

        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = _service.Toggle(id);

            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Deleted = _service.Delete(id);

            return Deleted > 0 ? Ok() : NotFound();
        }
    }
}

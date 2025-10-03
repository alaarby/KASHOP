using KASHOP.BLL.Interfaces;
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
    [Authorize(Roles = ("Admin, SuperAdmin"))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _service.GetById(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { data = request });
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
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

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _context;

        public CategoriesController(ICategoryService context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await Task.FromResult(_context.GetCategories());
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(short id)
        {
            var category = await Task.FromResult(_context.GetCategoryByID(id));
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(short id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await Task.FromResult(_context.GetCategoryByID(id));
            if (existingCategory == null)
            {
                return NotFound();
            }

            try
            {
                _context.UpdateCategory(id, category);
                // If your service is async, await it here
                return Ok(new { message = "update successful" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            try
            {
                var existingCategory = await Task.FromResult(_context.GetCategoryByID(category.CategoryId));
                if (existingCategory != null)
                {
                    return Conflict("Category already exists.");
                }

                _context.AddCategory(category);
                // If your service is async, await it here

                return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(short id)
        {
            var category = await Task.FromResult(_context.GetCategoryByID(id));
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                _context.DeleteCategory(id);
                // If your service is async, await it here

                return Ok(new { message = "delete successful" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
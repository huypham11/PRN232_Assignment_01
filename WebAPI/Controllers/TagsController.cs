using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Microsoft.Data.SqlClient;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            // Assuming GetTags() is synchronous, wrap in Task.Run for async controller
            var tags = await Task.Run(() => _tagService.GetTags());
            return Ok(tags);
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await Task.Run(() => _tagService.GetTagByID(id));
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(() => _tagService.UpdateTag(id, tag));
            }
            catch
            {
                if (_tagService.GetTagByID(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            try
            {
                await Task.Run(() => _tagService.AddTag(tag));
            }
            catch
            {
                if (_tagService.GetTagByID(tag.TagId) != null)
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtAction(nameof(GetTag), new { id = tag.TagId }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await Task.Run(() => _tagService.GetTagByID(id));
            if (tag == null)
            {
                return NotFound();
            }

            try
            {
                await Task.Run(() => _tagService.DeleteTag(id));
                return Ok(new { message = "delete successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "delete failed", details = ex.Message});
            }
        }

    }
}

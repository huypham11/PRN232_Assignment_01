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
    public class NewsArticlesController : ControllerBase
    {
        private readonly INewsArticleService _context;

        public NewsArticlesController(INewsArticleService context)
        {
            _context = context;
        }

        // GET: api/NewsArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> GetNewsArticles()
        {
            try
            {
                var articles = await Task.FromResult(_context.GetNewsArticles());
                return Ok(articles);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/NewsArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticle>> GetNewsArticle(string id)
        {
            try
            {
                var newsArticle = await Task.FromResult(_context.GetNewsArticleByID(id));
                if (newsArticle == null)
                {
                    return NotFound();
                }
                return Ok(newsArticle);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/NewsArticles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsArticle(string id, NewsArticle newsArticle)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return BadRequest("NewsArticle ID mismatch.");
            }

            var existingArticle = await Task.FromResult(_context.GetNewsArticleByID(id));
            if (existingArticle == null)
            {
                return NotFound();
            }

            try
            {
                _context.UpdateNewsArticle(id, newsArticle);
                // If your service is async, await it here
                return Ok(new { message = "update successful" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/NewsArticles
        [HttpPost]
        public async Task<ActionResult<NewsArticle>> PostNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                var existingArticle = await Task.FromResult(_context.GetNewsArticleByID(newsArticle.NewsArticleId));
                if (existingArticle != null)
                {
                    return Conflict("NewsArticle already exists.");
                }

                _context.AddNewsArticle(newsArticle);
                // If your service is async, await it here

                return CreatedAtAction(nameof(GetNewsArticle), new { id = newsArticle.NewsArticleId }, newsArticle);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/NewsArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsArticle(string id)
        {
            var newsArticle = await Task.FromResult(_context.GetNewsArticleByID(id));
            if (newsArticle == null)
            {
                return NotFound();
            }

            try
            {
                _context.DeleteNewsArticle(id);
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


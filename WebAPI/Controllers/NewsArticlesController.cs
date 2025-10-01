using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsArticlesController : ControllerBase
    {
        private readonly INewsArticleService _context;
        private readonly IActivityLogService _activityLogService;

        public NewsArticlesController(INewsArticleService context, IActivityLogService activityLogService)
        {
            _context = context;
            _activityLogService = activityLogService;
        }

        // GET: api/NewsArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> GetNewsArticles()
        {
            try
            {
                var articles = await Task.FromResult(_context.GetNewsArticles());

                // Log activity
                var currentUserId = ActivityLogHelper.GetCurrentUserId(HttpContext);
                await ActivityLogHelper.LogActivityAsync(
                    _activityLogService,
                    HttpContext,
                    currentUserId,
                    "VIEW",
                    "NewsArticle",
                    "ALL",
                    $"Viewed all news articles (Count: {articles.Count})"
                );

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

                // Log activity
                var currentUserId = ActivityLogHelper.GetCurrentUserId(HttpContext);
                await ActivityLogHelper.LogActivityAsync(
                    _activityLogService,
                    HttpContext,
                    currentUserId,
                    "VIEW",
                    "NewsArticle",
                    id,
                    $"Viewed news article: {newsArticle.NewsTitle}"
                );

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

                // Log activity
                var currentUserId = ActivityLogHelper.GetCurrentUserId(HttpContext);
                await ActivityLogHelper.LogActivityAsync(
                    _activityLogService,
                    HttpContext,
                    currentUserId,
                    "UPDATE",
                    "NewsArticle",
                    id,
                    $"Updated news article: {newsArticle.NewsTitle}",
                    existingArticle,
                    newsArticle
                );

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

                // Log activity
                var currentUserId = ActivityLogHelper.GetCurrentUserId(HttpContext);
                await ActivityLogHelper.LogActivityAsync(
                    _activityLogService,
                    HttpContext,
                    currentUserId,
                    "CREATE",
                    "NewsArticle",
                    newsArticle.NewsArticleId,
                    $"Created news article: {newsArticle.NewsTitle}",
                    newValues: newsArticle
                );

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

                // Log activity
                var currentUserId = ActivityLogHelper.GetCurrentUserId(HttpContext);
                await ActivityLogHelper.LogActivityAsync(
                    _activityLogService,
                    HttpContext,
                    currentUserId,
                    "DELETE",
                    "NewsArticle",
                    id,
                    $"Deleted news article: {newsArticle.NewsTitle}",
                    oldValues: newsArticle
                );

                return Ok(new { message = "delete successful" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


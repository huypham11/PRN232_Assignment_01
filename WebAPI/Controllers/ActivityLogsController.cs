using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityLogsController : ControllerBase
{
    private readonly IActivityLogService _activityLogService;

    public ActivityLogsController(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    // GET: api/ActivityLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLogs()
    {
        try
        {
            var activities = await _activityLogService.GetActivityLogsAsync();
            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/ActivityLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityLog>> GetActivityLog(int id)
    {
        try
        {
            var activityLog = await _activityLogService.GetActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return Ok(activityLog);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/ActivityLogs/user/5
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ActivityLog>>> GetUserActivityLogs(short userId)
    {
        try
        {
            var activities = await _activityLogService.GetActivityLogsByUserAsync(userId);
            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/ActivityLogs/entity/{entityType}
    [HttpGet("entity/{entityType}")]
    public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLogsByEntityType(string entityType)
    {
        try
        {
            var activities = await _activityLogService.GetActivityLogsByEntityTypeAsync(entityType);
            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/ActivityLogs/daterange?fromDate=2024-01-01&toDate=2024-12-31
    [HttpGet("daterange")]
    public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLogsByDateRange(
        [FromQuery] DateTime fromDate, 
        [FromQuery] DateTime toDate)
    {
        try
        {
            var activities = await _activityLogService.GetActivityLogsByDateRangeAsync(fromDate, toDate);
            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // POST: api/ActivityLogs
    [HttpPost]
    public async Task<ActionResult<ActivityLog>> PostActivityLog(ActivityLog activityLog)
    {
        try
        {
            await _activityLogService.AddActivityLogAsync(activityLog);
            return CreatedAtAction(nameof(GetActivityLog), new { id = activityLog.ActivityId }, activityLog);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // POST: api/ActivityLogs/log
    [HttpPost("log")]
    public async Task<IActionResult> LogActivity([FromBody] LogActivityRequest request)
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                request.UserId,
                request.Action,
                request.EntityType,
                request.EntityId,
                request.Description,
                request.OldValues,
                request.NewValues,
                request.IpAddress,
                request.UserAgent
            );
            return Ok(new { message = "Activity logged successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

public class LogActivityRequest
{
    public short UserId { get; set; }
    public string Action { get; set; } = null!;
    public string EntityType { get; set; } = null!;
    public string EntityId { get; set; } = null!;
    public string Description { get; set; } = null!;
    public object? OldValues { get; set; }
    public object? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
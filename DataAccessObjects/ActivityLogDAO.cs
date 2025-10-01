using System.Text.Json;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public class ActivityLogDAO
{
    public static async Task<List<ActivityLog>> GetActivityLogsAsync()
    {
        var list = new List<ActivityLog>();
        try
        {
            using var context = new FunewsManagementContext();
            list = await context.ActivityLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static async Task<List<ActivityLog>> GetActivityLogsByUserAsync(short userId)
    {
        var list = new List<ActivityLog>();
        try
        {
            using var context = new FunewsManagementContext();
            list = await context.ActivityLogs
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static async Task<ActivityLog?> GetActivityLogByIdAsync(int id)
    {
        ActivityLog? activityLog = null;
        try
        {
            using var context = new FunewsManagementContext();
            activityLog = await context.ActivityLogs
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ActivityId == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return activityLog;
    }

    public static async Task AddActivityLogAsync(ActivityLog activityLog)
    {
        try
        {
            using var context = new FunewsManagementContext();
            activityLog.Timestamp = DateTime.Now;
            context.ActivityLogs.Add(activityLog);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task LogActivityAsync(short userId, string action, string entityType, string entityId, 
        string description, object? oldValues = null, object? newValues = null, string? ipAddress = null, string? userAgent = null)
    {
        try
        {
            var activityLog = new ActivityLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                Description = description,
                Timestamp = DateTime.Now,
                OldValues = oldValues != null ? JsonSerializer.Serialize(oldValues) : null,
                NewValues = newValues != null ? JsonSerializer.Serialize(newValues) : null,
                IpAddress = ipAddress,
                UserAgent = userAgent
            };

            await AddActivityLogAsync(activityLog);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static async Task<List<ActivityLog>> GetActivityLogsByDateRangeAsync(DateTime fromDate, DateTime toDate)
    {
        var list = new List<ActivityLog>();
        try
        {
            using var context = new FunewsManagementContext();
            list = await context.ActivityLogs
                .Include(a => a.User)
                .Where(a => a.Timestamp >= fromDate && a.Timestamp <= toDate)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static async Task<List<ActivityLog>> GetActivityLogsByEntityTypeAsync(string entityType)
    {
        var list = new List<ActivityLog>();
        try
        {
            using var context = new FunewsManagementContext();
            list = await context.ActivityLogs
                .Include(a => a.User)
                .Where(a => a.EntityType == entityType)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }
}
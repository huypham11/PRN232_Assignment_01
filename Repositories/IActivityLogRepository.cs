using BusinessObjects;

namespace Repositories;

public interface IActivityLogRepository
{
    Task<List<ActivityLog>> GetActivityLogsAsync();
    Task<List<ActivityLog>> GetActivityLogsByUserAsync(short userId);
    Task<ActivityLog?> GetActivityLogByIdAsync(int id);
    Task AddActivityLogAsync(ActivityLog activityLog);
    Task LogActivityAsync(short userId, string action, string entityType, string entityId, 
        string description, object? oldValues = null, object? newValues = null, string? ipAddress = null, string? userAgent = null);
    Task<List<ActivityLog>> GetActivityLogsByDateRangeAsync(DateTime fromDate, DateTime toDate);
    Task<List<ActivityLog>> GetActivityLogsByEntityTypeAsync(string entityType);
}
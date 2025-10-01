using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public class ActivityLogRepository : IActivityLogRepository
{
    public async Task<List<ActivityLog>> GetActivityLogsAsync()
        => await ActivityLogDAO.GetActivityLogsAsync();

    public async Task<List<ActivityLog>> GetActivityLogsByUserAsync(short userId)
        => await ActivityLogDAO.GetActivityLogsByUserAsync(userId);

    public async Task<ActivityLog?> GetActivityLogByIdAsync(int id)
        => await ActivityLogDAO.GetActivityLogByIdAsync(id);

    public async Task AddActivityLogAsync(ActivityLog activityLog)
        => await ActivityLogDAO.AddActivityLogAsync(activityLog);

    public async Task LogActivityAsync(short userId, string action, string entityType, string entityId, 
        string description, object? oldValues = null, object? newValues = null, string? ipAddress = null, string? userAgent = null)
        => await ActivityLogDAO.LogActivityAsync(userId, action, entityType, entityId, description, oldValues, newValues, ipAddress, userAgent);

    public async Task<List<ActivityLog>> GetActivityLogsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        => await ActivityLogDAO.GetActivityLogsByDateRangeAsync(fromDate, toDate);

    public async Task<List<ActivityLog>> GetActivityLogsByEntityTypeAsync(string entityType)
        => await ActivityLogDAO.GetActivityLogsByEntityTypeAsync(entityType);
}
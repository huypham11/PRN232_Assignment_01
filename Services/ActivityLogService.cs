using BusinessObjects;
using Repositories;

namespace Services;

public class ActivityLogService : IActivityLogService
{
    private readonly IActivityLogRepository _activityLogRepository = new ActivityLogRepository();

    public async Task<List<ActivityLog>> GetActivityLogsAsync()
        => await _activityLogRepository.GetActivityLogsAsync();

    public async Task<List<ActivityLog>> GetActivityLogsByUserAsync(short userId)
        => await _activityLogRepository.GetActivityLogsByUserAsync(userId);

    public async Task<ActivityLog?> GetActivityLogByIdAsync(int id)
        => await _activityLogRepository.GetActivityLogByIdAsync(id);

    public async Task AddActivityLogAsync(ActivityLog activityLog)
        => await _activityLogRepository.AddActivityLogAsync(activityLog);

    public async Task LogActivityAsync(short userId, string action, string entityType, string entityId, 
        string description, object? oldValues = null, object? newValues = null, string? ipAddress = null, string? userAgent = null)
        => await _activityLogRepository.LogActivityAsync(userId, action, entityType, entityId, description, oldValues, newValues, ipAddress, userAgent);

    public async Task<List<ActivityLog>> GetActivityLogsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        => await _activityLogRepository.GetActivityLogsByDateRangeAsync(fromDate, toDate);

    public async Task<List<ActivityLog>> GetActivityLogsByEntityTypeAsync(string entityType)
        => await _activityLogRepository.GetActivityLogsByEntityTypeAsync(entityType);
}
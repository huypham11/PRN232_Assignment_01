using System.Net.Http.Json;
using System.Text.Json;
using BusinessObjects;

namespace WebApp.Services;

public class ActivityLogService
{
    private readonly AuthenticatedHttpClient _httpClient;

    public ActivityLogService(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ActivityLog>?> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityLog>>("api/ActivityLogs");
        }
        catch (Exception)
        {
            return new List<ActivityLog>();
        }
    }

    public async Task<ActivityLog?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ActivityLog>($"api/ActivityLogs/{id}");
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<ActivityLog>?> GetByUserIdAsync(short userId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityLog>>($"api/ActivityLogs/user/{userId}");
        }
        catch (Exception)
        {
            return new List<ActivityLog>();
        }
    }

    public async Task<List<ActivityLog>?> GetByEntityTypeAsync(string entityType)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityLog>>($"api/ActivityLogs/entity/{entityType}");
        }
        catch (Exception)
        {
            return new List<ActivityLog>();
        }
    }

    public async Task<List<ActivityLog>?> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityLog>>($"api/ActivityLogs/daterange?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}");
        }
        catch (Exception)
        {
            return new List<ActivityLog>();
        }
    }

    public async Task<bool> LogActivityAsync(short userId, string action, string entityType, string entityId, 
        string description, object? oldValues = null, object? newValues = null, string? ipAddress = null, string? userAgent = null)
    {
        try
        {
            var request = new
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                Description = description,
                OldValues = oldValues,
                NewValues = newValues,
                IpAddress = ipAddress,
                UserAgent = userAgent
            };

            var response = await _httpClient.PostAsJsonAsync("api/ActivityLogs/log", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> AddAsync(ActivityLog activityLog)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/ActivityLogs", activityLog);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
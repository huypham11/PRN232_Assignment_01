using BusinessObjects;
using Services;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WebAPI.Helpers;

public static class ActivityLogHelper
{
    public static async Task LogActivityAsync(
        IActivityLogService activityLogService,
        HttpContext httpContext,
        short userId,
        string action,
        string entityType,
        string entityId,
        string description,
        object? oldValues = null,
        object? newValues = null)
    {
        try
        {
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();

            await activityLogService.LogActivityAsync(
                userId,
                action,
                entityType,
                entityId,
                description,
                oldValues,
                newValues,
                ipAddress,
                userAgent
            );
        }
        catch (Exception)
        {
            // Log activity failure should not break the main operation
            // You might want to log this error to your logging system
        }
    }

    public static short GetCurrentUserId(HttpContext httpContext)
    {
        try
        {
            // Try to get user ID from custom header (set by frontend)
            if (httpContext?.Request?.Headers.ContainsKey("X-User-Id") == true)
            {
                var userIdHeader = httpContext.Request.Headers["X-User-Id"].FirstOrDefault();
                if (short.TryParse(userIdHeader, out var userId))
                {
                    return userId;
                }
            }

            // Try to get from JWT claims if using JWT authentication
            var userIdClaim = httpContext?.User?.FindFirst("UserId")?.Value;
            if (short.TryParse(userIdClaim, out var claimUserId))
            {
                return claimUserId;
            }

            // Try to get from other common claim names
            var idClaim = httpContext?.User?.FindFirst("id")?.Value;
            if (short.TryParse(idClaim, out var idClaimUserId))
            {
                return idClaimUserId;
            }

            // Default fallback - should not happen in production
            Console.WriteLine("Warning: Unable to determine current user ID, using default value 1");
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting current user ID: {ex.Message}");
            return 1;
        }
    }

    public static string GetEntityDescription(string action, string entityType, object? entity = null)
    {
        return action switch
        {
            "CREATE" => $"Created new {entityType.ToLower()}",
            "UPDATE" => $"Updated {entityType.ToLower()}",
            "DELETE" => $"Deleted {entityType.ToLower()}",
            "VIEW" => $"Viewed {entityType.ToLower()}",
            _ => $"Performed {action.ToLower()} on {entityType.ToLower()}"
        };
    }
}
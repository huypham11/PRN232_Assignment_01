using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WebApp.Services;

public class AuthenticatedHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public AuthenticatedHttpClient(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    private void AddUserIdHeader()
    {
        if (_authService.CurrentUser != null)
        {
            _httpClient.DefaultRequestHeaders.Remove("X-User-Id");
            _httpClient.DefaultRequestHeaders.Add("X-User-Id", _authService.CurrentUser.AccountId.ToString());
        }
    }

    public async Task<T?> GetFromJsonAsync<T>(string requestUri)
    {
        AddUserIdHeader();
        return await _httpClient.GetFromJsonAsync<T>(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        AddUserIdHeader();
        return await _httpClient.PostAsJsonAsync(requestUri, value);
    }

    public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        AddUserIdHeader();
        return await _httpClient.PutAsJsonAsync(requestUri, value);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        AddUserIdHeader();
        return await _httpClient.DeleteAsync(requestUri);
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        AddUserIdHeader();
        return await _httpClient.GetAsync(requestUri);
    }
}
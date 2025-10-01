using System.Net.Http.Json;
using BusinessObjects;

namespace WebApp.Services
{
    public class SystemAccountService
    {
        private readonly AuthenticatedHttpClient _httpClient;
        
        public SystemAccountService(AuthenticatedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<SystemAccount>?> GetSystemAccountsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SystemAccount>>("api/systemaccounts");
        }
        
        public async Task<SystemAccount?> GetSystemAccountByIdAsync(short id)
        {
            return await _httpClient.GetFromJsonAsync<SystemAccount>($"api/systemaccounts/{id}");
        }
        
        public async Task CreateSystemAccountAsync(SystemAccount account)
        {
            var response = await _httpClient.PostAsJsonAsync("api/systemaccounts", account);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task UpdateSystemAccountAsync(short id, SystemAccount account)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/systemaccounts/{id}", account);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<Boolean> DeleteSystemAccountAsync(short id)
        {
            var response = await _httpClient.DeleteAsync($"api/systemaccounts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

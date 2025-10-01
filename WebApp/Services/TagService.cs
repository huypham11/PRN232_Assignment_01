using System.Net.Http.Json;
using BusinessObjects;

namespace WebApp.Services
{
    public class TagService
    {
        private readonly AuthenticatedHttpClient _httpClient;
        
        public TagService(AuthenticatedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<Tag>?> GetTagsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tag>>("api/tags");
        }
        
        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tag>($"api/tags/{id}");
        }
        
        public async Task CreateTagAsync(Tag tag)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tags", tag);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task UpdateTagAsync(int id, Tag tag)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tags/{id}", tag);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<Boolean> DeleteTagAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tags/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

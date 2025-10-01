using System.Net.Http.Json;
using BusinessObjects;

namespace WebApp.Services
{
    public class NewsArticleService
    {
        private readonly AuthenticatedHttpClient _httpClient;
        
        public NewsArticleService(AuthenticatedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<NewsArticle>?> GetNewsArticlesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NewsArticle>>("api/newsarticles");
        }
        
        public async Task<NewsArticle?> GetNewsArticleByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<NewsArticle>($"api/newsarticles/{id}");
        }
        
        public async Task CreateNewsArticleAsync(NewsArticle article)
        {
            var response = await _httpClient.PostAsJsonAsync("api/newsarticles", article);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task UpdateNewsArticleAsync(string id, NewsArticle article)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/newsarticles/{id}", article);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<Boolean> DeleteNewsArticleAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/newsarticles/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

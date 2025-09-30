using System.Net.Http.Json;
using BusinessObjects;

namespace WebBlazor.Client.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<Category>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<Category>>("api/Categories");
        
    }
}

using System.Net.Http.Json;
using BusinessObjects;

namespace WebApp.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;
    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Category>($"api/categories/{id}");
    }
    public async Task CreateCategoryAsync(Category category)
    {
        var response = await _httpClient.PostAsJsonAsync("api/categories", category);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateCategoryAsync(int id, Category category)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", category);
        response.EnsureSuccessStatusCode();
    }
    public async Task<Boolean> DeleteCategoryAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/categories/{id}");
        return response.IsSuccessStatusCode;
    }
}
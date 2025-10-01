using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            
            // Configure HttpClient
            var apiBaseUrl = builder.HostEnvironment.IsDevelopment() 
                ? "https://localhost:7242/" 
                : "https://localhost:7242/";
                
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
            // hehe hihi
            // Add services to the container
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<AuthenticatedHttpClient>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<SystemAccountService>();
            builder.Services.AddScoped<NewsArticleService>();
            builder.Services.AddScoped<ActivityLogService>();
            
            await builder.Build().RunAsync();
        }
    }
}

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebBlazor.Client;
using WebBlazor.Client.Services;

namespace WebBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // HttpClient gọi API backend (ví dụ ASP.NET Core API bạn có sẵn)
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/")
            });

            // Đăng ký service
            builder.Services.AddScoped<CategoryService>();

            await builder.Build().RunAsync();
        }
    }
}
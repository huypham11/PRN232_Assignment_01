using System.Text.Json.Serialization;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//system account
builder.Services.AddSingleton<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddSingleton<ISystemAccountService, SystemAccountService>();
//tag
builder.Services.AddSingleton<ITagRepository, TagRepository>();
builder.Services.AddSingleton<ITagService, TagService>();
//news article
builder.Services.AddSingleton<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddSingleton<INewsArticleService, NewsArticleService>();
//category
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
//activity log
builder.Services.AddSingleton<IActivityLogRepository, ActivityLogRepository>();
builder.Services.AddSingleton<IActivityLogService, ActivityLogService>();

builder.Services.AddSwaggerGen();

// Enhanced CORS configuration for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7215", "http://localhost:5157") // WebApp ports
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Use HTTPS redirection in development
    app.UseHttpsRedirection();
}

app.UseCors("AllowBlazorApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
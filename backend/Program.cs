using AspNetCoreRateLimit;
using backend.Config;
using backend.Infraestructure;
using backend.Middleware;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<RestExceptionHandler>(); // Agregar RestExceptionHandler globalmente
});
builder.Services.AddTransient<FirmaService>();
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISupplierService, SupplierService>();

RateLimitConfig.AddRateLimiting(builder.Services);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
SwaggerConfig.AddSwaggerDocumentation(builder.Services);
AuthenticationConfig.AddCustomAuthentication(builder.Services);

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ValidationMiddleware>();
app.UseAuthentication();
app.UseIpRateLimiting();
app.UseAuthorization();
app.MapControllers();
app.Run();
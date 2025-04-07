using AspNetCoreRateLimit;
using backend.Config;
using backend.Infraestructure;
using backend.Middleware;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<RestExceptionHandler>(); // Agregar RestExceptionHandler globalmente
})
.AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Permite cualquier origen
              .AllowAnyHeader()    // Permite cualquier cabecera
              .AllowAnyMethod();   // Permite cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
    });
});
var app = builder.Build();

// Usar CORS
app.UseCors("AllowAll");

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ValidationMiddleware>();
app.UseAuthentication();
app.UseIpRateLimiting();
app.UseAuthorization();
app.MapControllers();
app.Run();
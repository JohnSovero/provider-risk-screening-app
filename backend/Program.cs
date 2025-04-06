using AspNetCoreRateLimit;
using backend.Infraestructure;
using backend.MIddleware;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddTransient<FirmaService>();
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
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
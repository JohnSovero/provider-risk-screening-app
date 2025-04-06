
using Microsoft.AspNetCore.Authentication;
using backend.Handlers;

namespace backend.Infraestructure
{
    public static class AuthenticationConfig
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, CustomBasicAuthenticationHandler>("BasicAuthentication", null);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Security.Jwks;

namespace Common.Security.IdentityServer
{
    public static class IdentityServerConfig
    {
        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Authentication");
            services.Configure<JwtBearerSettings>(appSettingsSection);
            var settings = appSettingsSection.Get<JwtBearerSettings>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                  {
                      options.Authority = settings.Authority;
                      options.Audience = settings.Audience;
                      options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                  });

            return services;
        }
    }

}

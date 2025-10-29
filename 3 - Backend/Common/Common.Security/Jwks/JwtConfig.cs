using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Security.Jwks
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Identity");
            services.Configure<JwksSettings>(appSettingsSection);

            var settings = appSettingsSection.Get<JwksSettings>();
            var signingConfigurations = new SigningConfigurations(settings.JwtSecret);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;

                x.TokenValidationParameters.IssuerSigningKey = signingConfigurations.Key;

                x.TokenValidationParameters.ValidateIssuerSigningKey = true;
                x.TokenValidationParameters.ValidateAudience = false;
                x.TokenValidationParameters.ValidateIssuer = false;
                x.TokenValidationParameters.ValidateLifetime = true;

                x.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });
        }

        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
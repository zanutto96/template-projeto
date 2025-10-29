using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Security.Identity
{
    public static class CurrentUserConfiguration
    {
        public static IServiceCollection AddCurrentUserConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }

}

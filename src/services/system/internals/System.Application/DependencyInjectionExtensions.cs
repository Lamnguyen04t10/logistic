using Core.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace System.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationDependencyInjection(
            this IServiceCollection services
        )
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}

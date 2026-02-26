using System.Domain.Abstractions;
using System.Infrastructure.Implementations;
using Core.Services.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace System.Infrastructure
{
    public static class ConfigurationSettingExtensions
    {
        public static IServiceCollection AddCustomDbContext(
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseNpgsql(
                    connectionString,
                    npgsqlOptions =>
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorCodesToAdd: null
                        )
                )
            );
            return services;
        }

        public static IServiceCollection AddCustomDependencyInjection(
            this IServiceCollection services
        )
        {
            //services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddRedis(
            this IServiceCollection services,
            string connectionString
        )
        {
            var multiplexer = ConnectionMultiplexer.Connect(connectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            return services;
        }
    }
}

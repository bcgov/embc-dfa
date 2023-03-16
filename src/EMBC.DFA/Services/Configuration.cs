using System.IO;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Medallion.Threading.WaitHandles;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using StackExchange.Redis;

namespace EMBC.DFA.Services
{
    public static class Configuration
    {
        public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetValue("REDIS_CONNECTIONSTRING", string.Empty);
            var appName = configuration.GetValue("APP_NAME", string.Empty);
            if (!string.IsNullOrEmpty(redisConnectionString))
            {
                Log.Information("Configuring Redis cache");
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                });
                services.AddDataProtection()
                    .SetApplicationName(appName)
                    .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisConnectionString), $"{appName}-data-protection-keys");

                services.AddSingleton<IDistributedSemaphoreProvider>(new RedisDistributedSynchronizationProvider(ConnectionMultiplexer.Connect(redisConnectionString).GetDatabase()));
            }
            else
            {
                Log.Information("Configuring in-memory cache");
                var dataProtectionPath = configuration.GetValue("KEY_RING_PATH", string.Empty);
                services.AddDistributedMemoryCache();
                var dpBuilder = services.AddDataProtection()
                    .SetApplicationName(appName);

                if (!string.IsNullOrEmpty(dataProtectionPath)) dpBuilder.PersistKeysToFileSystem(new DirectoryInfo(dataProtectionPath));

                services.AddSingleton<IDistributedSemaphoreProvider>(new WaitHandleDistributedSynchronizationProvider());
            }

            services.AddSingleton<INotificationManager, NotificationManager>();

            services.TryAddTransient<SmbBackgroundTask>();
            services.AddHostedService<BackgroundTask<SmbBackgroundTask>>();

            services.TryAddTransient<IndBackgroundTask>();
            services.AddHostedService<BackgroundTask<IndBackgroundTask>>();

            services.TryAddTransient<GovBackgroundTask>();
            services.AddHostedService<BackgroundTask<GovBackgroundTask>>();

            return services;
        }
    }
}

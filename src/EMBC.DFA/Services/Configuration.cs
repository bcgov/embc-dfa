using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EMBC.DFA.Services
{
    public static class Configuration
    {
        public static IServiceCollection AddBackgroundTasks(this IServiceCollection services)
        {
            services.TryAddTransient<SmbBackgroundTask>();
            services.AddHostedService<BackgroundTask<SmbBackgroundTask>>();
            return services;
        }
    }
}

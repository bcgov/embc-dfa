using Microsoft.Extensions.DependencyInjection;

namespace EMBC.DFA.Services
{
    public static class Configuration
    {
        public static IServiceCollection AddBackgroundTasks(this IServiceCollection services)
        {
            services.AddTransient<SmbBackgroundTask>();
            services.AddHostedService<BackgroundTask<SmbBackgroundTask>>();
            return services;
        }
    }
}

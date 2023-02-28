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

            services.TryAddTransient<IndBackgroundTask>();
            services.AddHostedService<BackgroundTask<IndBackgroundTask>>();

            //services.TryAddTransient<GovBackgroundTask>();
            //services.AddHostedService<BackgroundTask<GovBackgroundTask>>();

            return services;
        }
    }
}

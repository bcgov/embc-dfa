using Microsoft.Extensions.DependencyInjection;

namespace EMBC.DFA.Resources.Submissions
{
    public static class Configuration
    {
        public static IServiceCollection AddSubmissionsRepository(this IServiceCollection services)
        {
            services.AddTransient<ISubmissionsRepository, SubmissionsRepository>();
            return services;
        }
    }
}

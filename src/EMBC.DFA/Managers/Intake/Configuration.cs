using Microsoft.Extensions.DependencyInjection;

namespace EMBC.DFA.Managers.Intake
{
    public static class Configuration
    {
        public static IServiceCollection AddIntakeManager(this IServiceCollection services)
        {
            services.AddTransient<IIntakeManager, IntakeManager>();
            return services;
        }
    }
}

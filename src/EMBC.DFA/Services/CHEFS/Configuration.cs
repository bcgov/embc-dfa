using Microsoft.Extensions.DependencyInjection;

namespace EMBC.DFA.Services.CHEFS
{
    public static class Configuration
    {
        public static IServiceCollection AddCHEFSAPIService(this IServiceCollection services)
        {
            services.AddHttpClient<ICHEFSAPIService, CHEFSAPIService>();
            return services;
        }
    }
}

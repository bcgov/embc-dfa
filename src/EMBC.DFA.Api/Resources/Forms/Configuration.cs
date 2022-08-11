namespace EMBC.DFA.Api.Resources.Forms
{
    public static class Configuration
    {
        public static IServiceCollection AddFormsRepository(this IServiceCollection services)
        {
            services.AddTransient<IFormsRepository, FormsRepository>();
            return services;
        }
    }
}

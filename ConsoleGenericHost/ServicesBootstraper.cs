using Microsoft.Extensions.DependencyInjection;

namespace ConsoleGenericHost
{
    public static class ServicesBootstraper
    {
        public static IServiceCollection AddConsoleGenericHostServices(this IServiceCollection services) 
        {
            services.AddHostedService<Worker>();
            return services;
        }
            

    }
}

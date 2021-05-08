using ConsoleGenericHost.CountDown;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleGenericHost
{
    public static class ServicesBootstraper
    {
        public static IServiceCollection AddConsoleGenericHostServices(this IServiceCollection services, IConfiguration configuration) 
        {

            services.AddOptions<CountdownService>().Bind(configuration.GetSection(nameof(CountdownSettings)));

            services.AddScoped<ICountdownService, CountdownService>();
            services.AddHostedService<Worker>();
            return services;
        }
            

    }
}

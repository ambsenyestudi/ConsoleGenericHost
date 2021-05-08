using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGenericHost
{
    public static class ServicesBootstraper
    {
        public static IServiceCollection AddConsoleGenericHostServices(this IServiceCollection services) 
        {
            //services.AddTransient<IWeatherService>();
            return services;
        }
            

    }
}

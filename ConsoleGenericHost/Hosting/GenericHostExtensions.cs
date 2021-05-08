
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Hosting
{
    //A little tunning on https://github.com/aspnet/Hosting/blob/master/src/Microsoft.AspNetCore.Hosting/WebHostBuilderExtensions.cs
    public static class GenericHostExtensions
    {
        public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder) where TStartup: IStartup=>
            hostBuilder.UseStartup(typeof(TStartup));

        public static IHostBuilder UseStartup(this IHostBuilder hostBuilder, Type startupType) 
        {
            
            return hostBuilder
                .ConfigureServices(services =>
                {
                    if (typeof(IStartup).IsAssignableFrom(startupType))
                    {
                        var configuration = new ConfigurationBuilder().UseDefaultConfiguration().Build();
                        var startup = new Startup(configuration);

                        hostBuilder.ConfigureServices((host, services) =>
                        {

                            startup.ConfigureServices(services);
                            services.AddHostedService<Worker>();
                        });
                    }
                });
        }

        public static IConfigurationBuilder UseDefaultConfiguration(this IConfigurationBuilder hostConfigBuilder)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            hostConfigBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();
            return hostConfigBuilder;
        }


    }
}

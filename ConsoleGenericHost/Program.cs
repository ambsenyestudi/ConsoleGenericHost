using ConsoleGenericHost.Application;
using ConsoleGenericHost.CountDown;
using ConsoleGenericHost.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ConsoleGenericHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging(logging =>
            {
                // Add any 3rd party loggers like NLog or Serilog
            })
            .ConfigureServices((hostContext, services) =>
            {
                services
                .AddHostedService<Worker>()
                .AddSingleton<ICountdownService, CountdownService>()
                .AddScoped<IPostingRepository, PostingRepository>(); ;

                services.AddOptions<CountdownSettings>().Bind(hostContext.Configuration.GetSection(nameof(CountdownSettings)));
            });

    }
}

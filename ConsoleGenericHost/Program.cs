using ConsoleGenericHost.Application.Posting;
using ConsoleGenericHost.Infrastructure.Posting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                .AddScoped<IPostingRepository, PostingRepository>(); 

                services.AddOptions<PostingSettings>().Bind(hostContext.Configuration.GetSection(nameof(PostingSettings)));
                services
                .AddHttpClient<IPostingGateway, PostingGateway>(c =>
                    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
            });

    }
}

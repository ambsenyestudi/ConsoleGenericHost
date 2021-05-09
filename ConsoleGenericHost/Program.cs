using ConsoleGenericHost.Application.Posting;
using ConsoleGenericHost.Extensions;
using ConsoleGenericHost.Infrastructure;
using ConsoleGenericHost.Infrastructure.Posting;
using ConsoleGenericHost.MessageHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
                services.AddOptions<BlogClientSettings>().Bind(hostContext.Configuration.GetSection(nameof(BlogClientSettings)));
                services.AddTransient<AcceptMessageHandler>();
                services
                .AddHttpClient<IPostingGateway, PostingGateway>()
                .ConfigureHttpClientWithBlogClientSettings()
                .AddHttpMessageHandler<AcceptMessageHandler>();
            });

    }
}

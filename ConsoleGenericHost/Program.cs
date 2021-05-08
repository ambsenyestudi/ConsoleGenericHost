using ConsoleGenericHost.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            .ConfigureHostConfiguration(configHost => configHost.UseDefaultConfiguration())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddConsoleGenericHostServices();
            });

    }
}

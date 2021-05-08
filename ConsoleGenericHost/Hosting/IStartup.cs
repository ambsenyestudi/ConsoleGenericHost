using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleGenericHost.Hosting
{
    public interface IStartup
    {
        IServiceProvider ConfigureServices(IServiceCollection services);
    }
}

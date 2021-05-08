using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGenericHost
{
    public class Worker : IHostedService
    {
        private readonly ILogger logger;
        public Worker(ILogger<Worker> logger,
            IHostApplicationLifetime appLifetime)
        {
            this.logger = logger;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Warming up");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Shutting down");
            return Task.CompletedTask;

        }
        private void OnStarted()
        {
            logger.LogInformation("2. OnStarted has been called.");
        }

        private void OnStopping()
        {
            logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            logger.LogInformation("5. OnStopped has been called.");
        }
    }
}

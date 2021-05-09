using ConsoleGenericHost.Application;
using ConsoleGenericHost.CountDown;
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
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IPostingRepository postingRepository;
        private readonly ICountdownService countdownService;

        private int? _exitCode;

        public Worker(
            ILogger<Worker> logger,
            IHostApplicationLifetime appLifetime,
            IPostingRepository postingRepository,
            ICountdownService countdownService)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.postingRepository = postingRepository;
            this.countdownService = countdownService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var posts = await postingRepository.GetAllPosts();
                        IReadOnlyList<int> countdownList = await countdownService.GenerateCountdownAsync();
                        for (int i = 0; i < countdownList.Count; i++)
                        {
                            var currentNumber = countdownList[i];

                            logger.LogInformation($"{currentNumber} seconds reminder");
                            await Task.Delay(1000);
                        }

                        _exitCode = 0;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unhandled exception!");
                        _exitCode = 1;
                    }
                    finally
                    {
                        // Stop the application once the work is done
                        appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"Exiting with return code: {_exitCode}");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}

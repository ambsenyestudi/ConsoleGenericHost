using ConsoleGenericHost.Application.Posting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGenericHost
{
    public class Worker : IHostedService
    {
        private readonly ILogger logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IPostingRepository postingRepository;

        private int? exitCode;

        public Worker(
            ILogger<Worker> logger,
            IHostApplicationLifetime appLifetime,
            IPostingRepository postingRepository)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.postingRepository = postingRepository;
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
                        var posts = await postingRepository.GetAllPostsAsync();
                        foreach (var item in posts)
                        {
                            var printalbePost = $"id: {item.id} title:{item.title} userId:{item.userId}";
                            logger.LogInformation(printalbePost);
                            await Task.Delay(20);
                        }
                        exitCode = 0;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unhandled exception!");
                        exitCode = 1;
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
            logger.LogDebug($"Exiting with return code: {exitCode}");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}

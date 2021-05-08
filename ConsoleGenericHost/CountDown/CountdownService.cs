using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.CountDown
{
    public class CountdownService : ICountdownService
    {
        public const int MILLISECONDS_WAIT = 1000;
        private CountdownSettings settings;
        private ILogger<CountdownService> logger;

        public CountdownService(IOptions<CountdownSettings> options, ILogger<CountdownService> logger)
        {
            this.settings = options.Value;
            this.logger = logger;
        }
        public Task<IReadOnlyList<int>> GenerateCountdownAsync() =>
            Task.Factory.StartNew<IReadOnlyList<int>>(() => 
            {
                var countdownList = new List<int>();
                for (int i = 0; i < settings.CountdownFrom; i++)
                {
                    var currentNumber = settings.CountdownFrom - i;
                    countdownList.Add(currentNumber);
                }
                return countdownList;
            });
    }
}

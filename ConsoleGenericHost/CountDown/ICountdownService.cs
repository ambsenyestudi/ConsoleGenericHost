using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.CountDown
{
    public interface ICountdownService
    {
        Task<IReadOnlyList<int>> GenerateCountdownAsync();
    }
}

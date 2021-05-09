using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGenericHost.MessageHandlers
{
    public class AcceptMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jsonheader = new MediaTypeWithQualityHeaderValue("application/json");
            request.Headers.Accept.Add(jsonheader);
            return base.SendAsync(request, cancellationToken);
        }
    }
}

using ConsoleGenericHost.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ConsoleGenericHost.Extensions
{
    public static class HttpClientExtensions
    {
        public static IHttpClientBuilder ConfigureHttpClientWithBlogClientSettings(this IHttpClientBuilder clientBuilder) =>
            clientBuilder
            .ConfigureHttpClient((serviceProvider, httpClient) =>
             {
                 var httpClientOptions = serviceProvider
                     .GetRequiredService<IOptions<BlogClientSettings>>()
                     .Value;
                 httpClient.BaseAddress = new Uri(httpClientOptions.BaseUrl);
             });
    }
}

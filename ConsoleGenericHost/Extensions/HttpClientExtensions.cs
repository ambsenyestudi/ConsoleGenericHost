using ConsoleGenericHost.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleGenericHost.Extensions
{
    public static class HttpClientExtensions
    {

        public static IHttpClientBuilder ConfigureHttpclientWithBaseUrl<TSettings>(this IHttpClientBuilder clientBuilder) where TSettings : class, IBaseUrl =>
            clientBuilder
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<TSettings>>();
                var url = options.Value.BaseUrl;
                if(!string.IsNullOrWhiteSpace(url))
                {
                    var uri = url.ToUri();
                    httpClient.BaseAddress = uri;
                }
            });

        public static IHttpClientBuilder ConfigureHttpclientWithClientSettings<TSettings>(this IHttpClientBuilder clientBuilder) where TSettings: class =>
            clientBuilder
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<TSettings>>();
                if (options.TryGetClientUri(out Uri uri))
                {
                    httpClient.BaseAddress = uri;
                }
            });

        public static IOptions<TSettings> GetOptions<TSettings> (this IServiceProvider serviceProvider, string urlPropetyName = "BaseUrl") where TSettings : class
        {
            return serviceProvider.GetRequiredService<IOptions<TSettings>>();
        }

        public static Uri ToUri(this string url) =>
            new Uri(url);

        public static bool TryGetClientUri<TSettings> (this IOptions<TSettings> options,  out Uri uri, string urlPropetyName = "BaseUrl") where TSettings : class
        {
            var settings = options.Value;
            if(!TryGetUrlPropertyInfo(typeof(TSettings), out PropertyInfo pInfo, urlPropetyName))
            {
                uri = null;
                return false;
                
            }
            var url = pInfo.GetValue(settings) as string;
            if (string.IsNullOrWhiteSpace(url))
            {
                uri = null;
                return false;
            }

            uri = url.ToUri();
            return true;

        }

        public static bool TryGetUrlPropertyInfo(this Type settingsType, out PropertyInfo pInfo, string urlPropetyName = "BaseUrl")
        {
            if (settingsType == null || string.IsNullOrWhiteSpace(urlPropetyName))
            {
                pInfo = null;
                return false;
            }
            var pInfoList = settingsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            pInfo = pInfoList.FirstOrDefault(pi => pi.Name.ToLower() == urlPropetyName.ToLower());
            return pInfo != null;
        }


    }
}

// Copyright (c) 2019 Lykke Corp.
//  See the LICENSE file in the project root for more information.

using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Snow.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder ConfigureProxy(this IHttpClientBuilder builder, 
            string proxyAddress,
            string userName, 
            string password)
        {
            if (string.IsNullOrEmpty(proxyAddress))
            {
                return builder;
            }

            return builder.ConfigurePrimaryHttpMessageHandler(sp =>
            {            
                var proxy = new WebProxy(proxyAddress);
                if (!string.IsNullOrEmpty(userName))
                {
                    proxy.Credentials = new NetworkCredential
                    {
                        Password = password,
                        UserName = userName
                    };
                }
                
                return new HttpClientHandler {Proxy = proxy};
            });
        }
    }
}

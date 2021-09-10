// Copyright (c) 2019 Lykke Corp.
//  See the LICENSE file in the project root for more information.

using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Snow.Extensions
{
    /// <summary>
    /// Http client builder extensions
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// Configure proxy usage for http client
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="proxyAddress">The proxy address</param>
        /// <param name="userName">The user name if required</param>
        /// <param name="password">The password if required</param>
        /// <returns></returns>
        public static IHttpClientBuilder ConfigureProxy(this IHttpClientBuilder builder, 
            string proxyAddress,
            string userName, 
            string password)
        {
            if (string.IsNullOrEmpty(proxyAddress))
            {
                return builder;
            }

            return builder.ConfigurePrimaryHttpMessageHandler(_ =>
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

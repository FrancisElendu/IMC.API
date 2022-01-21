using IMC.API.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IMC.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithBearer(this HttpClient client, string apiKey)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
            return client;
        }
    }
}

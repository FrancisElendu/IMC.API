using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMC.API.TaxJarHttpClient
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ILogger logger;
        public HttpClient Client { get; }
        public HttpClientService(ILoggerFactory loggerFactory, HttpClient client)
        {
            Client = client;
            logger = loggerFactory.CreateLogger<HttpClientService>();
        }

        public async Task<T> Get<T>(string url)
        {
            var httpResponse = await GetResponse(url);
            if (!httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                var responseDetail = await httpResponse.Content.ReadAsStringAsync();
                logger.LogError(httpResponse.ReasonPhrase + ": " + responseDetail);
            }
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        }

        public HttpClient GetClient()
        {
            return Client;
        }

        public async Task<T> Post<T>(string url, string content)
        {
            var response = await PostStringContent(url, content);
            if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                var responseDetail = await response.Content.ReadAsStringAsync();
                logger.LogError(response.ReasonPhrase + ": " + responseDetail);
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        private async Task<HttpResponseMessage> PostStringContent(string url, string content)
        {
            return await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
        }

        private async Task<HttpResponseMessage> GetResponse(string url)
        {
            return await Client.GetAsync(url);
        }
    }
}

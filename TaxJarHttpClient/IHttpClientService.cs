using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IMC.API.TaxJarHttpClient
{
    public interface IHttpClientService
    {
        HttpClient GetClient();
        //Task<T> Get<T>(string location, string url);
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string content, string url);
    }
}

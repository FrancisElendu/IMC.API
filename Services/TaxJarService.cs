using IMC.API.Extensions;
using IMC.API.Models;
using IMC.API.Models.Response;
using IMC.API.Settings;
using IMC.API.TaxJarHttpClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMC.API.Services
{
    public class TaxJarService : ITaxJarService
    {
        private readonly IHttpClientService _client;
        private readonly ILogger<TaxJarService> _logger;
        private readonly TaxJarSettings taxJarSettings;
        public TaxJarService(IHttpClientService client, ILogger<TaxJarService> logger, IOptions<TaxJarSettings> taxJarSettingsOption)
        {
            _client = client;
            _logger = logger;
            taxJarSettings = taxJarSettingsOption.Value;
            _client.GetClient().WithBearer(taxJarSettings.ApiKey);
        }


        public async Task<TaxRate> GetTaxRates(string location)
        {
            var url = $"{taxJarSettings.BaseUrl}rates/{ location}";
            return await _client.Get<TaxRate>(url);
        }

        public async Task<SalesTaxResponse> CalculateTax(SalesTax salesTax)
        {
            var url = $"{taxJarSettings.BaseUrl}taxes";
            var order = JsonConvert.SerializeObject(salesTax);
            return await _client.Post<SalesTaxResponse>(url, order);
        }



        //First implemented it this way and later refactored to the code above for better functionality and reusability.

        //public async Task<TaxRate> GetTaxRates(string location)
        //{
        //    var url = $"{taxJarSettings.BaseUrl}rates/{ location}";
        //    _client.DefaultRequestHeaders.Add("Accept", "application/json");
        //    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + taxJarSettings.ApiKey);
        //    var response = await _client.GetStringAsync(url);

        //    return JsonConvert.DeserializeObject<TaxRate>(response);
        //}

        //public async Task<SalesTaxResponse> CalculateTax(SalesTax salesTax)
        //{
        //    var url = $"{taxJarSettings.BaseUrl}taxes";
        //    _client.DefaultRequestHeaders.Add("Accept", "application/json");
        //    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + taxJarSettings.ApiKey);

        //    var order = JsonConvert.SerializeObject(salesTax);
        //    var requestContent = new StringContent(order, Encoding.UTF8, "application/json");
        //    var responseContent = await _client.PostAsync(url, requestContent);
        //    responseContent.EnsureSuccessStatusCode();
        //    var content = await responseContent.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<SalesTaxResponse>(content);
        //}
    }
}

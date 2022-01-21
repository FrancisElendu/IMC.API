using IMC.API.Models;
using IMC.API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMC.API.Services
{
    public interface ITaxJarService
    {
        Task<TaxRate> GetTaxRates(string location);
        Task<SalesTaxResponse> CalculateTax(SalesTax salesTax);
    }
}

using System.Threading.Tasks;
using IMC.API.Models;
using IMC.API.Models.Response;
using IMC.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxJarController : ControllerBase
    {
        private readonly ITaxJarService _taxJarService;

        public TaxJarController(ITaxJarService taxJarService)
        {
            _taxJarService = taxJarService;
        }

        [HttpGet("{location}")]

        public async Task<ActionResult<TaxRate>> GetRateByLocation(string location)
        {
            var result = await _taxJarService.GetTaxRates(location);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SalesTaxResponse>> CalculateTax([FromBody] SalesTax salesTax)
        {
            var result = await _taxJarService.CalculateTax(salesTax);
            return Ok(result);
        }
    }
}

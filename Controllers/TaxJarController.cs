using System.Threading.Tasks;
using IMC.API.Models;
using IMC.API.Models.Response;
using IMC.API.Services;
using Microsoft.AspNetCore.Http;
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

        [ProducesResponseType(typeof(TaxRate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{location}")]
        public async Task<ActionResult<TaxRate>> GetRateByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return StatusCode(StatusCodes.Status400BadRequest);

            var result = await _taxJarService.GetTaxRates(location);
            if(result == null)
                return StatusCode(StatusCodes.Status404NotFound);
            return Ok(result);
        }

        [ProducesResponseType(typeof(SalesTaxResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<SalesTaxResponse>> CalculateTax([FromBody] SalesTax salesTax)
        {
            if (salesTax == null)
                return StatusCode(StatusCodes.Status400BadRequest);
            var result = await _taxJarService.CalculateTax(salesTax);
            return Ok(result);
        }
    }
}

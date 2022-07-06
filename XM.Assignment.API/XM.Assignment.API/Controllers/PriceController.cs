using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XM.Assignment.Domain.Abstractions;

namespace XM.Assignment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceFetcherService _priceFetcherService;

        public PriceController(IPriceFetcherService priceFetcherService)
        {
            _priceFetcherService = priceFetcherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentPriceAsync(string sourceName, string currency)
        {
            var result = await _priceFetcherService.GetCurrentPriceAsync(sourceName, currency);

            return result is null ? NotFound() : Ok(result);
        }
    }
}

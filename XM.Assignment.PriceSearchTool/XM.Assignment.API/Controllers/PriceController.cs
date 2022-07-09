using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PriceController : ControllerBase
{
    private readonly IPriceFetcherService _priceFetcherService;

    public PriceController(IPriceFetcherService priceFetcherService)
    {
        _priceFetcherService = priceFetcherService;
    }

    /// <summary>
    /// Fetches the latest price of the curency for the source name
    /// </summary>
    /// <param name="sourceName"> The name of the price source that is registered in appsettings.json file</param>
    /// <param name="currency"> The name of the currency (e.g. btcusd, btceur) </param>
    /// <response code="200"> Price retrieved </response>
    /// <response code="404"> The source in not registered in appsettigs.json </response>
    /// <response code="500"> API error. Possible reasons: currency name is not supported by the source </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PriceLogEntry>>> GetCurrentPriceAsync(string sourceName, string currency)
    {
        var result = await _priceFetcherService.GetCurrentPriceAsync(sourceName, currency);

        return result is null ? NotFound() : Ok(result);
    }
}

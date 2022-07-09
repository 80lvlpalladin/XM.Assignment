using Microsoft.AspNetCore.Mvc;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PriceHistoryController : ControllerBase
{
    private readonly IPriceHistoryService _priceHistoryService;

    public PriceHistoryController(IPriceHistoryService priceHistoryService)
    {
        _priceHistoryService = priceHistoryService;
    }

    /// <summary>
    /// Fetches the history of prices that were loaded from different sources during the lifetime of the application
    /// </summary>
    /// <param name="currency"> The name of the currency (e.g. btcusd, btceur) </param>
    /// <param name="requestBody">Optional request body that contains filtering and ordering options</param>
    /// <response code="200"> History retrieved </response>
    /// <response code="404"> No history was found for the given currency and/or filter </response>
    /// <response code="500"> API error </response>
    [HttpPost("{currency}")]
    public ActionResult<IEnumerable<PriceLogEntryDto>> GetPriceHistory(string currency, [FromBody] PriceHistoryRequestModel? requestBody = null)
    {
        var response = _priceHistoryService.GetHistoryForCurrency(currency, requestBody?.OrderBy, requestBody?.Filtering);
        return response.Count() > 0 ? Ok(response) : NotFound();
    }
}

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

    [HttpPost("{currency}")]
    public IActionResult GetPriceHistory([FromBody] PriceHistoryRequestModel request, string currency)
    {
        var response = _priceHistoryService.GetAllHistoryForCurrency(currency, request.OrderingOptions);
        return response.Count() > 0 ? Ok(response) : NotFound();
    }
}

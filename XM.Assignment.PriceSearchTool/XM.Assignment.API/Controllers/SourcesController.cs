using Microsoft.AspNetCore.Mvc;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SourcesController : ControllerBase
{
    private readonly IDomainConfigurationProvider _domainConfigurationProvider;

    public SourcesController(IDomainConfigurationProvider domainConfigurationProvider)
    {
        _domainConfigurationProvider = domainConfigurationProvider;
    }

    /// <summary>
    /// Fetches all sources that are registered in appsettings.json file
    /// </summary>
    /// <response code="200"> Sources retrieved </response>
    /// <response code="404"> No sources were found in the appsettings.json </response>
    /// <response code="500"> API error </response>
    [HttpGet]
    public ActionResult<IEnumerable<Source>> GetAllSources()
    {
        return _domainConfigurationProvider.GetAllSources().Count() > 0
            ? Ok(_domainConfigurationProvider.GetAllSources())
            : NotFound();
    }
}

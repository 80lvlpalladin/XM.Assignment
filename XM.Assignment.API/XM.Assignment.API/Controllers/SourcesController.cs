using Microsoft.AspNetCore.Mvc;
using XM.Assignment.Domain.Abstractions;

namespace XM.Assignment.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SourcesController : ControllerBase
{
    private readonly ISourcesProvider _sourcesProvider;

    public SourcesController(ISourcesProvider sourcesProvider)
    {
        _sourcesProvider = sourcesProvider;
    }

    [HttpGet]
    public IActionResult GetAllSources() => Ok(_sourcesProvider.GetAll());
}

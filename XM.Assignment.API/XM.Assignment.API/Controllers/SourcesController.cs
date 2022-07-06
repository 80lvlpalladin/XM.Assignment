using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.API.Controllers
{
    [Route("api/[controller]")]
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
}

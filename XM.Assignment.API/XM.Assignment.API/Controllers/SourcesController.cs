using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly IEnumerable<Source> _sources;

        public SourcesController(IOptions<AppSettings> appSettings)
        {
            _sources = appSettings.Value.Sources;
        }

        [HttpGet]
        public IActionResult GetAllSources()
        {
            return Ok(_sources);
        }
    }
}

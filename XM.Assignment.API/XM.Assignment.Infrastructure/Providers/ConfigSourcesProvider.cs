using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.Infrastructure.Providers
{
    internal class ConfigSourcesProvider : ISourcesProvider
    {
        private readonly IEnumerable<Source> _sources;

        public ConfigSourcesProvider(IOptions<AppSettings> appSettings)
        {
            _sources = appSettings.Value.Sources;
        }

        public IEnumerable<Source> GetAll() => _sources;
    }
}

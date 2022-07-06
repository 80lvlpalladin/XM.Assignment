using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using XM.Assignment.Domain.Abstractions;

namespace XM.Assignment.Infrastructure.Datastore
{
    public class PriceLogDatastore : IPriceLogDatastore
    {
        private readonly IDictionary<string, IList<PriceLogEntry>> _priceLog;

        public PriceLogDatastore(ISourcesProvider sourcesProvider)
        {
            _priceLog = new Dictionary<string, IList<PriceLogEntry>>();
            foreach (var source in sourcesProvider.GetAll().Select(s => s.Name.ToLower()))
            {
                _priceLog.TryAdd(source, new List<PriceLogEntry>());
            }
        }

        public IEnumerable<PriceLogEntry>? GetAll(string sourceName)
        {
            return _priceLog.TryGetValue(sourceName.ToLower(), out var priceLogEntries) ?
                priceLogEntries :
                null;
        }

        public bool Save(string sourceName, PriceLogEntry logEntry)
        {
            var result = false;
            var lowercaseSourceName = sourceName.ToLower();

            if (_priceLog.TryGetValue(lowercaseSourceName, out var priceLogEntries))
            {
                priceLogEntries.Add(logEntry);
                result = true;
            }

            return result;
        }

    }
}

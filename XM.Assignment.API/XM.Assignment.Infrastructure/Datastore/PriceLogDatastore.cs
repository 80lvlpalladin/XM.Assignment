using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using XM.Assignment.Domain.Abstractions;

namespace XM.Assignment.Infrastructure.Datastore
{
    public class BitcoinPriceLogDatastore : IBitcoinPriceLogDatastore
    {
        private readonly IDictionary<string, IList<BitcoinPriceLogEntry>> _priceLog;

        public BitcoinPriceLogDatastore(IOptions<AppSettings> appSettings)
        {
            _priceLog = new Dictionary<string, IList<BitcoinPriceLogEntry>>();
            foreach (var source in appSettings.Value.Sources.Select(s => s.Name.ToLower()))
            {
                _priceLog.TryAdd(source, new List<BitcoinPriceLogEntry>());
            }
        }

        public IEnumerable<BitcoinPriceLogEntry>? GetAll(string sourceName)
        {
            return _priceLog.TryGetValue(sourceName.ToLower(), out var priceLogEntries) ?
                priceLogEntries :
                null;
        }

        public bool Save(string sourceName, BitcoinPriceLogEntry logEntry)
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

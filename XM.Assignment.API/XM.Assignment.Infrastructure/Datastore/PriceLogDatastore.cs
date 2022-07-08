using Microsoft.Extensions.Caching.Memory;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Infrastructure.Datastore;

public class PriceLogDatastore : IPriceLogDatastore
{
    //TODO inevitable out-of memory error in the future. replace with MemoryCache
    //private readonly IDictionary<string, IList<PriceLogEntry>> _priceLog;
    private readonly IMemoryCache _priceLog;
    private readonly IEnumerable<string> _sourceNames;

    public PriceLogDatastore(ISourcesProvider sourcesProvider, IMemoryCache memoryCache)
    {
        _priceLog = memoryCache;
        _sourceNames = sourcesProvider.GetAll().Select(s => s.Name.ToLower());
    }

    /// <returns>Empty array, if source-currency pair exists in the datastore, null otherwise</returns>
    public IEnumerable<PriceLogEntry>? GetForSourceAndCurrency(string sourceName, string currency)
    {
        var key = GenerateKey(sourceName.ToLower(), currency.ToLower());
        return _priceLog.TryGetValue(key, out IEnumerable<PriceLogEntry> priceLogEntries) ?
            priceLogEntries :
            null;
    }

    public void Save(string sourceName, string currency, PriceLogEntry logEntry)
    {
        var key = GenerateKey(sourceName.ToLower(), currency.ToLower());

        var valueInCache = _priceLog.GetOrCreate(key, c => new List<PriceLogEntry>());

        if (valueInCache is not null)
            valueInCache.Add(logEntry);


        //if (_priceLog.TryGetValue(key, out List<PriceLogEntry> priceLogEntries))
        //{
        //    priceLogEntries.Add(logEntry);
        //}
        //else
        //{
        //    _priceLog.
        //}
    }

    private string GenerateKey(string sourceName, string currency) => $"{sourceName}_{currency}_v1";
}

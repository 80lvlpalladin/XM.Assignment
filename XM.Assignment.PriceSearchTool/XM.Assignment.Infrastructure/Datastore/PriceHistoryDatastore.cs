using Microsoft.Extensions.Caching.Memory;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Infrastructure.Datastore;

public class PriceHistoryDatastore : IPriceHistoryDatastore
{
    private readonly IMemoryCache _priceLog;
    private readonly IEqualityComparer<PriceLogEntry> _priceLogEntryEqualityComparer;
    private readonly TimeSpan _expirationTime;

    private readonly object _lockObject = new();
    private readonly int _defaultExpirationTimeHours = 24;

    public PriceHistoryDatastore(
        IMemoryCache memoryCache, 
        IEqualityComparer<PriceLogEntry> priceLogEntryEqualityComparer, 
        IDomainConfigurationProvider domainConfigurationProvider)
    {
        _priceLog = memoryCache;
        _priceLogEntryEqualityComparer = priceLogEntryEqualityComparer;
        _expirationTime = TimeSpan.FromHours(domainConfigurationProvider.GetDatastoreExpirationTime() ?? _defaultExpirationTimeHours);
    }

    public IEnumerable<PriceLogEntry>? GetForSourceAndCurrency(string sourceName, string currency, uint? filterTimestamp = null)
    {
        var key = GenerateKey(sourceName.ToLower(), currency.ToLower());
        if (_priceLog.TryGetValue(key, out IEnumerable<PriceLogEntry> priceLogEntries))
        {
            return filterTimestamp is not null
                ? priceLogEntries.Where(entry => entry.TimeStamp > filterTimestamp)
                : priceLogEntries;
        }
        else
            return null;
    }

    public void Save(string sourceName, string currency, PriceLogEntry logEntry)
    {
        lock (_lockObject)
        {
            var key = GenerateKey(sourceName.ToLower(), currency.ToLower());

            var valueInCache = _priceLog.GetOrCreate(key, c => 
                { 
                    c.AbsoluteExpirationRelativeToNow = _expirationTime;
                    return new HashSet<PriceLogEntry>(_priceLogEntryEqualityComparer);
                });

            valueInCache.Add(logEntry);
        }
    }

    private string GenerateKey(string sourceName, string currency) => $"{sourceName}_{currency}_v1";
}

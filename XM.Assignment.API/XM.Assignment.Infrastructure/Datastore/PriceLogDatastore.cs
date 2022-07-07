using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Infrastructure.Datastore;

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

    public IEnumerable<PriceLogEntry>? GetAll(string sourceName, string currency)
    {
        var key = GenerateKey(sourceName.ToLower(), currency.ToLower());
        return _priceLog.TryGetValue(key, out var priceLogEntries) ?
            priceLogEntries :
            null;
    }

    public bool Save(string sourceName, string currency, PriceLogEntry logEntry)
    {
        var result = false;
        var key = GenerateKey(sourceName.ToLower(), currency.ToLower());

        if (_priceLog.TryGetValue(key, out var priceLogEntries))
        {
            priceLogEntries.Add(logEntry);
            result = true;
        }

        return result;
    }

    private string GenerateKey(string sourceName, string currency) => $"{sourceName}_{currency}_v1";
}

using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions;

public interface IPriceHistoryDatastore
{
    IEnumerable<PriceLogEntry>? GetForSourceAndCurrency(string sourceName, string currency, uint? filterDate = null);
    void Save(string sourceName, string currency, PriceLogEntry logEntry);
}
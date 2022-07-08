using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface IPriceLogDatastore
    {
        IEnumerable<PriceLogEntry>? GetForSourceAndCurrency(string sourceName, string currency);
        void Save(string sourceName, string currency, PriceLogEntry logEntry);
    }
}
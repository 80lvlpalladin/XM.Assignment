using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface IPriceLogDatastore
    {
        IEnumerable<PriceLogEntry>? GetAll(string sourceName, string currency);
        bool Save(string sourceName, string currency, PriceLogEntry logEntry);
    }
}
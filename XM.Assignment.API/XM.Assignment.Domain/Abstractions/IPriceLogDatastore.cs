using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface IPriceLogDatastore
    {
        IEnumerable<PriceLogEntry>? GetAll(string sourceName);
        bool Save(string sourceName, PriceLogEntry logEntry);
    }
}
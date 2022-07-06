using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface IBitcoinPriceLogDatastore
    {
        IEnumerable<BitcoinPriceLogEntry>? GetAll(string sourceName);
        bool Save(string sourceName, BitcoinPriceLogEntry logEntry);
    }
}